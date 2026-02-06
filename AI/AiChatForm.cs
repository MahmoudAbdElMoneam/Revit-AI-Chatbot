using AIChat;
using AIChat.AI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using DocumentFormat.OpenXml.Office.SpreadSheetML.Y2023.MsForms;
using Newtonsoft.Json;
using OllamaSharp;
using OllamaSharp.Models;
using OllamaSharp.Models.Chat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;
using Form = System.Windows.Forms.Form;

namespace AIChat
{
    public partial class AiChatForm : Form
    {
        public string settingsFolder = "";
        public string settingsPath = "";
        public Dictionary<string, string> settings = [];
        public Dictionary<string, string> prompts = new Dictionary<string, string>();
        public ExternalEvent subAiExternalEvent;
        public AiExternalEventHandler subAiHandler;
        UIApplication uiApp;
        public AiChatForm(UIApplication _app, ExternalEvent _subAiExternalEvent, AiExternalEventHandler _subAiHandler)
        {
            InitializeComponent();
            subAiExternalEvent = _subAiExternalEvent;
            subAiHandler = _subAiHandler;
            // Make the form a child of the main Revit window for proper behavior
            IntPtr revitWindowHandle = ComponentManager.ApplicationWindow;
            //new System.Windows.Forms.Form(new JtWindowHandle(revitWindowHandle)); // Use helper class (see below)

            uiApp = _app;
            ChatboxInfo cbi = new ChatboxInfo();
            cbi.NamePlaceholder = "";
            cbi.PhonePlaceholder = "";

            var chat_panel = new Chatbox(cbi, _app, this);
            chat_panel.Name = "chat_panel";
            chat_panel.Dock = DockStyle.Fill;
            panelChat.Controls.Add(chat_panel);

            string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            settingsFolder = Directory.GetParent(assemblyPath).Parent.FullName;
            settingsFolder += "\\Settings\\";
            if (!Directory.Exists(settingsFolder))
                Directory.CreateDirectory(settingsFolder);
            settingsFolder += _app.Application.Username + "\\";
            if (!Directory.Exists(settingsFolder))
                Directory.CreateDirectory(settingsFolder);
            settingsPath = settingsFolder + "ollama.json";


            if (tbOllamaPath.Text == "")
            {
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string ollamaInstallPath = Path.Combine(localAppData, "Programs", "Ollama");
                // You can also attempt to verify if 'ollama.exe' exists in this directory
                string ollamaExecutablePath = Path.Combine(ollamaInstallPath, "ollama.exe");
                if (System.IO.File.Exists(ollamaExecutablePath))
                {
                    tbOllamaPath.Text = ollamaInstallPath + "\\";
                }
                else
                {
                    string pathVariable = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
                    if (string.IsNullOrEmpty(pathVariable))
                    {
                        pathVariable = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
                    }
                    if (!string.IsNullOrEmpty(pathVariable))
                    {
                        string[] paths = pathVariable.Split(';');
                        foreach (string path in paths)
                        {
                            if (path.Contains("Ollama") && System.IO.File.Exists(System.IO.Path.Combine(path, "ollama.exe")))
                            {
                                tbOllamaPath.Text = path + "\\";
                                break;
                            }
                        }
                    }
                }
            }
            ReadSystemPrompts("Not yet recieved, user must allow interaction with Revit file and send a message first.");
            ReadSettings();

        }
        // Make Load event async
        private async Task startOllama()
        {
            // Await for completion before proceeding
            WriteSettings();
            await Ollama.BeginOllama(this);
        }
        private async void AiChatForm_Load(object sender, EventArgs e)
        {
            this.Activate();
            await startOllama();

            // Or, if you want to show UI first:
            // _ = LoadDataAsync(); // Fire-and-forget (use with care)
        }
        private void cbAIModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ollama.selectedAIModel = cbAIModels.SelectedItem.ToString();
            Ollama.ollama.SelectedModel = Ollama.selectedAIModel;
        }

        private void btnBrowseOllama_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            // Set validate names and check file exists to false otherwise windows will 
            // not let you select "Folder Selection."
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            // Always default to Folder Selection.
            folderBrowser.FileName = "Folder with Ollama.exe Selection"; // A dummy file name

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderPath = System.IO.Path.GetDirectoryName(folderBrowser.FileName);
                tbOllamaPath.Text = folderPath + "\\";
                // Use the selected folder path
            }
            WriteSettings();
        }
        public void ReadSettings()
        {
            try
            {
                if (!File.Exists(settingsPath))
                    WriteSettings();
                string settingsContent = File.ReadAllText(settingsPath);
                settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(settingsContent);
                if (settings.ContainsKey("ollamaPath"))
                    tbOllamaPath.Text = settings["ollamaPath"];
                if (settings.ContainsKey("cbInteractRevit"))
                {
                    bool cbChecked = false;
                    bool.TryParse(settings["cbInteractRevit"], out cbChecked);
                    cbInteractRevit.Checked = cbChecked;
                }
                if (settings.ContainsKey("savedPrompts"))
                {
                    prompts = JsonConvert.DeserializeObject<Dictionary<string, string>>(settings["savedPrompts"]);
                    for (int i = 0; i < prompts.Count; i++)
                    {
                        cbPromptName.Items.Add(prompts.Keys.ElementAt(i));
                    }
                    if (cbPromptName.Items.Count > 0)
                        cbPromptName.SelectedIndex = 0;

                }
                if (settings == null)
                    WriteSettings();
            }
            catch
            {
                settings = new Dictionary<string, string>();
                WriteSettings();
            }
        }
        public void WriteSettings()
        {
            settings["ollamaPath"] = tbOllamaPath.Text;
            settings["cbInteractRevit"] = cbInteractRevit.Checked.ToString();
            settings["savedPrompts"] = JsonConvert.SerializeObject(prompts);
            string settingsContent = JsonConvert.SerializeObject(settings);
            File.WriteAllText(settingsPath, settingsContent);
        }
        public void UpdateStatus(string status, int progress = 0)
        {
            if (progress > 0)
            {
                if (!toolStripProgressBar1.Visible)
                    toolStripProgressBar1.Visible = true;
                toolStripProgressBar1.Value = progress;
            }
            else
            {
                toolStripProgressBar1.Visible = false;
                toolStripProgressBar1.Value = 0;
            }
            if (toolStripStatusLabel1.Text != status)
            {
                if (statusStrip1.InvokeRequired)
                {
                    // If not, use Invoke to run this method on the UI thread
                    statusStrip1.Invoke(new Action<string, int>(UpdateStatus), new object[] { status, progress });
                }
                else
                {
                    toolStripStatusLabel1.Text = status;
                    statusStrip1.Refresh();
                }
            }
        }
        public string retrieveAndReAskPrompt = "";
        public string interactRevitPrompt = "";
        public string interactRevitPromptFollowUp = "";

        public void ReadSystemPrompts(string firstRunResult)
        {
            retrieveAndReAskPrompt = @"You are a Revit assistant running inside a Revit {0} add-in.
                            You can ask to run tools using C# as a Revit macro code to fill code inside a Run function, 
                            Do not declare a class or method. Do not use top-level statements,                            
                            don't suggest any code that would require additional references or out of Revit {0} API,
                            always return a value. Don't declare using references clauses for any dll, as u run inside an already build function.
                            Inside the existing function the following are already defined, never write any of this 2 lines:
                            UIDocument uidoc = app.ActiveUIDocument;
                            Document doc = uidoc.Document;
                            use StringBuilder instead of Newtonsoft.Json.
                            To assign a Line to Curve, use Curve curve = line;
                            To model a wall use this code in the same order Wall.Create(doc, curve, defaultWallType.Id, wallHeight, double offset=0, bool flip=false, bool structural=false).

                            Use the already declared uidoc and doc instead of Application.ActiveUIDocument.
                            Don't use BuiltInParameter to retrieve parameter values. Never use DisplayUnitType, ParameterType, StringBuilder, only use System, System.Linq, System.Collections.Generic, 
                            Autodesk.Revit.DB, Autodesk.Revit.UI.
                            For units, ignore uints, never use UnitTypeId, instead treat everything as string..
                            Don't use var, declare the actual objects types.
                            To get ElementId, don't use .IntegerValue, instead use elem.Id, or parameter.AsElementId().
                            To retrieve element parameter use element.LookupParameter(ParameterName), no ToList(), or First() are required.
                            To convert type 'Autodesk.Revit.DB.ParameterSet' to 'System.Collections.Generic.ICollection<Autodesk.Revit.DB.Parameter>, you must use a foreach loop.
                            Cast all Collections ToList() as they generate errors, except collection sets, like Parameters.
                            Don't use weird characters such as 》、《.
                            return string markdown responses if needed, and code without declaring top functions, code should be within:
                            ```csharp
                            ```";
            retrieveAndReAskPrompt = string.Format(retrieveAndReAskPrompt, uiApp.Application.VersionNumber);

            if (settings.ContainsKey("prompts") && prompts.ContainsKey("RetrieveAndReAskPrompt"))
            {
                prompts.TryGetValue("RetrieveAndReAskPrompt", out retrieveAndReAskPrompt);
                retrieveAndReAskPrompt = string.Format(retrieveAndReAskPrompt, uiApp.Application.VersionNumber);
                if (!retrieveAndReAskPrompt.Contains($"Revit {uiApp.Application.VersionNumber}"))
                    retrieveAndReAskPrompt += $"Remember that you are a Revit assistant running inside a Revit {uiApp.Application.VersionNumber} Add-In.";

            }
            else
            {
                if (prompts.ContainsKey("RetrieveAndReAskPrompt"))
                    prompts["RetrieveAndReAskPrompt"] = retrieveAndReAskPrompt;
                else
                    prompts.Add("RetrieveAndReAskPrompt", retrieveAndReAskPrompt);
                if (settings.ContainsKey("prompts"))
                    settings["prompts"] = JsonConvert.SerializeObject(prompts);
                else
                    settings.Add("prompts", JsonConvert.SerializeObject(prompts));
            }
            interactRevitPrompt =
                       @"You are a Revit assistant running inside a Revit {0} add-in in 2 steps, when a user asks a question, generate the necessery code to read/implement the requirement from the opened Revit Model,
                        by reading data from the Revit model, let ur code return the data JSON as a string, don't show window/dialog, return JSON.ToString(),
                            Secondly, I will send this data to u with the user query to think how to respond to the user using this data.
                            For the first part you can run tools using C# as a Revit macro code that will fill code inside a Run function, 
                            Do not declare a class or method. Do not use top-level statements,
                            don't suggest any code that would require additional references or out of Revit {0} API,
                            Inside Run the following are already defined:
                            UIDocument uidoc = app.ActiveUIDocument;
                            Document doc = uidoc.Document;
                            use StringBuilder instead of Newtonsoft.Json.
                            Use the already declared uidoc and doc instead of Application.ActiveUIDocument.
                            Don't use BuiltInParameter to retrieve parameter values. Never use DisplayUnitType, ParameterType, StringBuilder, only use System, System.Linq, System.Collections.Generic, Autodesk.Revit.DB, Autodesk.Revit.UI.
                            For units, ignore uints, never use UnitTypeId, instead treat everything as string..
                            always return a value. Don't add using for any dll, as u run inside an already build function.
                            Don't use var, declare the actual objects types.
                            To get ElementId, don't use .IntegerValue, instead use elem.Id, or parameter.AsElementId().
                            To retrieve element parameter use element.LookupParameter(ParameterName), no ToList(), or First() are required.
                            To convert type 'Autodesk.Revit.DB.ParameterSet' to 'System.Collections.Generic.ICollection<Autodesk.Revit.DB.Parameter>, you must use a foreach loop.
                            Cast all Collections ToList() as they generate errors, except collection sets, like Parameters.
                            Don't use weird characters such as 》、《.
                            return responses, and write code without declaring the top function, use available doc, code should be within:
                            ```csharp
                            ```";
            interactRevitPrompt = string.Format(interactRevitPrompt, uiApp.Application.VersionNumber);

            if (settings.ContainsKey("prompts") && prompts.ContainsKey("InteractRevitPrompt"))
            {
                prompts.TryGetValue("InteractRevitPrompt", out interactRevitPrompt);
                interactRevitPrompt = string.Format(interactRevitPrompt, uiApp.Application.VersionNumber);

                if (!interactRevitPrompt.Contains($"Revit {uiApp.Application.VersionNumber}"))
                    interactRevitPrompt += $"Remember that you are a Revit assistant running inside a Revit {uiApp.Application.VersionNumber} Add-In.";
            }
            else
            {
                if (prompts.ContainsKey("InteractRevitPrompt"))
                    prompts["InteractRevitPrompt"] = interactRevitPrompt;
                else
                    prompts.Add("InteractRevitPrompt", interactRevitPrompt);

                if (settings.ContainsKey("prompts"))
                    settings["prompts"] = JsonConvert.SerializeObject(prompts);
                else
                    settings.Add("prompts", JsonConvert.SerializeObject(prompts));
            }
            interactRevitPromptFollowUp =
                       @"You are a Revit assistant running inside a Revit add-in in 2 steps, as a user asked a question, you generated the necessery code to read/implement the requirement from the opened Revit Model,
                        by reading data from the Revit model, the exported data from the data retrieved from the firt step is:{0}.
                            Secondly, think how to respond to the user question using this data.
                            Mention the queried data as a summary, and respond to the user's question.";
            interactRevitPromptFollowUp = string.Format(interactRevitPromptFollowUp, firstRunResult);
            if (settings.ContainsKey("prompts") && prompts.ContainsKey("InteractRevitPromptFollowUp"))
            {
                prompts.TryGetValue("InteractRevitPromptFollowUp", out string interactRevitPromptFollowUp);
                interactRevitPromptFollowUp = string.Format(interactRevitPromptFollowUp, firstRunResult);

                if (!interactRevitPromptFollowUp.Contains($"the data retrieved from the firt step is:{firstRunResult}"))
                    interactRevitPromptFollowUp += $"The data retrieved from the firt step is:{firstRunResult}";
            }
            else
            {
                if (prompts.ContainsKey("InteractRevitPromptFollowUp"))
                    prompts["InteractRevitPromptFollowUp"] = interactRevitPromptFollowUp;
                else
                    prompts.Add("InteractRevitPromptFollowUp", interactRevitPromptFollowUp);
                if (settings.ContainsKey("prompts"))
                    settings["prompts"] = JsonConvert.SerializeObject(prompts);
                else
                    settings.Add("prompts", JsonConvert.SerializeObject(prompts));
            }
            WriteSettings();
        }
        private async void tbOllamaPath_TextChanged(object sender, EventArgs e)
        {
            await startOllama();
        }

        int panel1Width = 300;
        private void btnCollapse_Click(object sender, EventArgs e)
        {
            if (!splitContainer1.Panel1Collapsed)
            {
                splitContainer1.Panel1Collapsed = true;
            }
            else
            {
                splitContainer1.SplitterDistance = panel1Width;
                splitContainer1.Panel1Collapsed = false;
            }
        }

        private void cbPromptName_SelectedValueChanged(object sender, EventArgs e)
        {
            prompts.TryGetValue(cbPromptName.Text, out string prompt);
            tbPrompt.Text = prompt;
        }

        private void btnAddPrompt_Click(object sender, EventArgs e)
        {
            if (cbPromptName.Text != "" && !prompts.ContainsKey(cbPromptName.Text))
            {
                prompts[cbPromptName.Text] = tbPrompt.Text;
                cbPromptName.Items.Add(cbPromptName.Text);
                WriteSettings();
            }
            else if (cbPromptName.Text != "" && prompts.ContainsKey(cbPromptName.Text))
            {
                prompts[cbPromptName.Text] = tbPrompt.Text;
            }
            WriteSettings();
        }

        private void cbInteractRevit_CheckedChanged(object sender, EventArgs e)
        {
            if (cbInteractRevit.Checked && cbRetrieveAndReAsk.Checked)
            {
                cbRetrieveAndReAsk.Enabled = true;
                prompts.TryGetValue("RetrieveAndReAskPrompt", out string prompt);
                tbPrompt.Text = prompt;
                WriteSettings();
            }
            else if (cbInteractRevit.Checked && !cbRetrieveAndReAsk.Checked)
            {
                cbRetrieveAndReAsk.Enabled = true;
                prompts.TryGetValue("InteractRevitPrompt", out string prompt);
                tbPrompt.Text = prompt;
                WriteSettings();
            }
            else if (!cbInteractRevit.Checked)
            {
                tbPrompt.Text = "";
                cbAIModels.SelectedIndex = -1;
                cbRetrieveAndReAsk.Checked = false;
                cbRetrieveAndReAsk.Enabled = false;
                WriteSettings();
            }
        }

        private async void cbAIModels_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            await startOllama();
        }

        private void btnDeletePrompt_Click(object sender, EventArgs e)
        {
            cbPromptName.Items.Remove(cbPromptName.Text);
            if (prompts.ContainsKey(cbPromptName.Text))
                prompts.Remove(cbPromptName.Text);
            tbPrompt.Text = "";
            cbPromptName.SelectedIndex = -1;
            if (cbPromptName.Items.Count > 0)
                cbPromptName.SelectedIndex = 0;
            WriteSettings();
        }
        public bool canClose = false;

        private async void AiChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If the form is not yet allowed to close (i.e., this is the first close attempt)...
            if (!canClose)
            {
                // Cancel the closing event
                e.Cancel = true;
                UpdateStatus("Processing, please wait...");
                // this.Enabled = false; // Note: Disabling the form might make it unresponsive in some scenarios
            }
            else
            {
                subAiExternalEvent.Dispose();
                subAiExternalEvent = null;
                subAiHandler = null;

                //You have to call
                this.Close();// Allow the form to close without interruption
            }
        }

    }
}
