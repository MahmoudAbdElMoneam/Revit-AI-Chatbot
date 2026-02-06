using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DocumentFormat.OpenXml.Spreadsheet;
using Markdig;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using Revit.Async;

//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Document = Autodesk.Revit.DB.Document;

namespace AIChat.AI
{
    public class RevitCodeExecutor
    {
        public string LastCompilationErrors { get; private set; }
        public string aiDataFile = "";
        public async Task HandleUserMessageAsync(UIApplication uiApp, string userQuestion, Chatbox chatBox, AiChatForm aiForm)
        {
            //await RevitTask.RunAsync(
            //async app =>
            //{
            // 1. Ask the model (include instructions so it returns csharp_tool block when needed)
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            if (uiDoc == null)
            {
                aiForm.cbInteractRevit.Checked = false;
                aiForm.cbRetrieveAndReAsk.Checked = false;
                await Ollama.ShowAssistantText("No opened Revit Models found, unchecked the setting for you, enable it when u open a file. Please repeat your message.", chatBox);
                aiForm.UpdateStatus("No Revit File Opened...", 0);
                return;
            }

            Document doc = uiDoc.Document;
            // Example: selected elements
            var selIds = uiDoc.Selection.GetElementIds();
            string selectionInfo = "";
            foreach (var id in selIds)
            {
                var e = doc.GetElement(id);
                selectionInfo += $"- {e.Id} {e.Category?.Name} {e.Name}\n";
            }
            // Example: simple context from active document
            string docContext = $"Revit Version: {uiApp.Application.VersionNumber}\n" +
                $"Project: {doc.Title}\n" +
                                $"Active view: {doc.ActiveView.Name}\n";
            string systemPrompt = "";
            if (!aiForm.cbRetrieveAndReAsk.Checked)
            {
                
                systemPrompt = aiForm.retrieveAndReAskPrompt;
                aiDataFile = "";
            }
            if (aiForm.cbRetrieveAndReAsk.Checked)
            {
                aiDataFile = aiForm.settingsFolder + "AiData.txt";
               
                    systemPrompt = aiForm.interactRevitPrompt;
            }
            //@"You are a Revit assistant running inside a Revit add-in.
            //        You can ask to run tools using JSON, like:
            //        ```tool
            //        { ""tool"": ""get_selected_elements"", ""args"": {} }";
            string fullPrompt =
                systemPrompt + "\n\n" +
                "Current document context:\n" + docContext + "\n" +
                "Current selection:\n" + selectionInfo + "\n" +
                "User question:\n" + userQuestion;

            string response = await Ollama.GetAIResponse(fullPrompt, chatBox, true);

            // 2. Try to extract a code block
            string code = TryExtractCSharpTool(response);
            if (code == null)
            {
                // Normal text response: show it in your UI
                //await ShowAssistantText(response, chatBox);
                return;
            }
            aiForm.WriteSettings();
            aiForm.ReadSettings();
            // 3. Compile & run the code
            var executor = new RevitCodeExecutor();
            code = WrapUserCode(code);
            //bool ok = executor.CompileAndRun(code, uiApp);

            //if (!ok)
            //{
            //    await ShowAssistantText("Code failed:\n" + executor.LastCompilationErrors, chatBox);
            //    return;
            //}
            // Store code for the handler
            aiForm.UpdateStatus("Running Code...", 0);
            AiCommand.CurrentJob.CodeToRun = code;
            AiCommand.CurrentJob.LastError = null;
            AiCommand.AiHandler.aiForm = aiForm;
            AiCommand.AiHandler.chatBox = chatBox;
            AiCommand.AiHandler.userQuery = userQuestion;
            // Ask Revit to execute it in API context
            AiCommand.AiExternalEvent.Raise();
            aiForm.UpdateStatus("Status", 0);
            // 4. Optionally, run a follow‑up query asking the model to explain what it did,
            // based on any results your script wrote back (e.g. via a log you pass into the prompt).
            //});
        }
        private string WrapUserCode(string body)
        {
            // body = raw code coming from the model, e.g. inside ```csharp_tool``` block.
            // It should contain only statements, NOT class or method declarations.            
            return
          @"using System;
            using System.Linq;
            using System.Collections.Generic;
            using Autodesk.Revit.DB;
            using Autodesk.Revit.DB.Architecture;
            using Autodesk.Revit.UI;
            using Autodesk.Revit.Attributes;
            using Markdig;
            using System.Drawing.Drawing2D;
            using System.IO;
            using System.Reflection;
            using System.Text;
            public static class AiScript
            {
                public static string Run(UIApplication app)
                {
                    // Common Revit variables available to the snippet:
                    UIDocument uidoc = app.ActiveUIDocument;
                    Document doc = uidoc.Document;

                    // ---- BEGIN MODEL CODE ----
                    " + body + @"
                    // ---- END MODEL CODE ----
                    return ""succeeded"";
                }
            }";
        }
        private string TryExtractCSharpTool(string response)
        {
            const string startTag = "```csharp";
            const string endTag = "```";

            int start = response.IndexOf(startTag, StringComparison.OrdinalIgnoreCase);
            if (start < 0) return null;

            int codeStart = response.IndexOf('\n', start);
            if (codeStart < 0) return null;
            codeStart++;

            int end = response.IndexOf(endTag, codeStart, StringComparison.OrdinalIgnoreCase);
            if (end < 0) return null;

            return response.Substring(codeStart, end - codeStart).Trim();
        }
        public object CompileAndRun(string code, UIApplication uiApp, AiChatForm aiForm)
        {
            LastCompilationErrors = null;

            // 1. Parse code into syntax tree
            var syntaxTree = CSharpSyntaxTree.ParseText(code);

            // 2. Collect references: mscorlib, System, and Revit assemblies
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                .ToList();

            var references = new List<MetadataReference>();
            foreach (var asm in assemblies)
            {
                try
                {
                    references.Add(MetadataReference.CreateFromFile(asm.Location));
                }
                catch { /* ignore bad locations */ }
            }

            // Explicit Revit API references (if needed)
            // Adjust paths for your Revit version.[web:32][web:40]
            var revitApiPath = @$"C:\Program Files\Autodesk\Revit {uiApp.Application.VersionNumber}\RevitAPI.dll";
            var revitUiPath = @$"C:\Program Files\Autodesk\Revit {uiApp.Application.VersionNumber}\RevitAPIUI.dll";
            if (File.Exists(revitApiPath))
                references.Add(MetadataReference.CreateFromFile(revitApiPath));
            if (File.Exists(revitUiPath))
                references.Add(MetadataReference.CreateFromFile(revitUiPath));

            // 3. Create compilation
            var compilation = CSharpCompilation.Create(
                assemblyName: "AiDynamicScript_" + Guid.NewGuid().ToString("N"),
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                var emitResult = compilation.Emit(ms);

                if (!emitResult.Success)
                {
                    LastCompilationErrors = string.Join(
                        Environment.NewLine,
                        emitResult.Diagnostics
                            .Where(d => d.Severity == DiagnosticSeverity.Error)
                            .Select(d => d.ToString()));

                    return null;
                }

                // 4. Load assembly and execute AiScript.Run(UIApplication)
                ms.Seek(0, SeekOrigin.Begin);
                var asm = Assembly.Load(ms.ToArray());

                var scriptType = asm.GetType("AiScript");
                if (scriptType == null)
                {
                    LastCompilationErrors = "AiScript class not found. Ensure 'public static class AiScript' is defined.";
                    return null;
                }

                var runMethod = scriptType.GetMethod(
                    "Run",
                    BindingFlags.Public | BindingFlags.Static,
                    null,
                    new[] { typeof(UIApplication) },
                    null);

                if (runMethod == null)
                {
                    LastCompilationErrors = "Run(UIApplication) method not found on AiScript.";
                    return null;
                }

                try
                {
                    var codeResult = runMethod.Invoke(null, new object[] { uiApp });
                    return codeResult;
                }
                catch (Exception ex)
                {
                    LastCompilationErrors = ex.ToString();
                    return null;
                }
            }
        }
    }

    public class AiScriptJob
    {
        public string CodeToRun { get; set; }  // full C# code after WrapUserCode
        public string LastError { get; set; }
    }

    [Transaction(TransactionMode.Manual)]
    public class AiExternalEventHandler : IExternalEventHandler
    {
        public RevitCodeExecutor Executor { get; }
        public AiChatForm aiForm { get; set; }
        public Chatbox chatBox { get; set; }
        public string userQuery { get; set; }
        public AiExternalEventHandler(RevitCodeExecutor executor)
        {
            Executor = executor;
        }

        public void Execute(UIApplication app)
        {

            var job = AiCommand.CurrentJob; // wherever you store it
            if (string.IsNullOrWhiteSpace(job.CodeToRun))
                return;

            object result = Executor.CompileAndRun(job.CodeToRun, app, aiForm);

            if (result == null)
            {
                job.LastError = Executor.LastCompilationErrors;
                // optionally show a TaskDialog here
                //TaskDialog.Show("AI Script", "Code failed:\n" + job.LastError);
                RevitTask.RunAsync(
                async (uiApp) =>
                {
                    await Ollama.ShowAssistantText($"Running C# Code failed, adjust system prompt or your prompt to handle/solve it:\n{job.LastError}", chatBox);
                });
                job.CodeToRun = null;
                return;
            }
            else
            {
                if (aiForm.cbRetrieveAndReAsk.Checked)
                {
                    RevitTask.RunAsync(
                async (uiApp) =>
                {
                    await Ollama.ShowAssistantText(result.ToString(), chatBox);
                });
                }
            }
            // clear or keep CodeToRun depending on your use case
            job.CodeToRun = null;

            if (aiForm.cbRetrieveAndReAsk.Checked)
            {
                RevitTask.RunAsync(
                async (uiApp) =>
                {
                    string docContext = $"Revit Version: {uiApp.Application.VersionNumber}\n" +
            $"Project: {uiApp.ActiveUIDocument.Document.Title}\n" +
                            $"Active view: {uiApp.ActiveUIDocument.Document.ActiveView.Name}\n";
                    aiForm.ReadSystemPrompts(result.ToString());
                    aiForm.ReadSettings();
                    string fullPrompt = "Current prompt:\n" + aiForm.interactRevitPromptFollowUp + "\n" + "Current document context:\n" + docContext + "\n" +
                                            "Original User question:\n" + userQuery;

                    string response = await Ollama.GetAIResponse(fullPrompt, chatBox, true);
                });
            }
        }

        public string GetName() => "AI Script External Event Handler";
    }
}
