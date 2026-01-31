using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using Markdig;
using Markdown.ColorCode;
//using Microsoft.Web.WebView2.WinForms;
using OllamaSharp;
using OllamaSharp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Control = System.Windows.Forms.Control;
using Document = Autodesk.Revit.DB.Document;
using Panel = System.Windows.Forms.Panel;

namespace AIChat
{
    public static class Ollama
    {
        //ollama run danielsheep/Qwen3-Coder-30B-A3B-Instruct-1M-Unsloth:UD-Q4_K_XL

        public static AiChatForm form;
        public static string selectedAIModel = "";
        public static OllamaApiClient ollama;
        /// <summary>
        /// https://github.com/ollama/ollama/releases
        ///	Download: ollama-windows-amd64.zip
        ///	CMD
        ///	ollama serve
        ///	ollama pull gpt-oss-safeguard:20b
        /// </summary>
        /// <param name="formInstance"></param>
        /// <returns></returns>
        public static async Task BeginOllama(AiChatForm formInstance)
        {

            form = formInstance;
            await StartOllama();
        }
        public static async Task<IEnumerable<Model>> StartOllama()
        {
            // set up the client
            IEnumerable<Model> availableModels = await InitializeClient();
            string[] availableModelNames = availableModels.Select(x => x.Name).ToArray();
            if (availableModelNames.Count() > 0)
            {
                form.cbAIModels.Items.AddRange(availableModelNames);
                form.cbAIModels.SelectedText = availableModelNames.FirstOrDefault();
                selectedAIModel = availableModelNames.FirstOrDefault();
                ollama.SelectedModel = selectedAIModel;
                ollama.Config.Model = selectedAIModel;

                IEnumerable<RunningModel> runningModels = await ollama.ListRunningModelsAsync();
                if (runningModels.Count() == 0)
                {

                    await OllamaPullModel(selectedAIModel);
                    //OllamaCommand($"run {selectedAIModel}");
                    runningModels = await ollama.ListRunningModelsAsync();

                }
            }
            return availableModels;
        }
        private static async Task<IEnumerable<Model>> InitializeClient()
        {
            //var uri = new Uri("http://localhost:11434");
            ollama = new OllamaApiClient("http://localhost:11434");
            IEnumerable<Model> availableModels = null;
            try
            {
                availableModels = await ollama.ListLocalModelsAsync();
            }
            catch (Exception ex)
            {
                OllamaCommand("serve");
                bool ollamaRunning = false;
                while (!ollamaRunning)
                {
                    try
                    {
                        availableModels = await ollama.ListLocalModelsAsync();
                        ollamaRunning = true;
                    }
                    catch
                    {
                    }
                }
            }

            if (selectedAIModel != "")
                ollama.Config.Model = selectedAIModel;
            return availableModels;
        }
        private static async Task OllamaPullModel(string modelName)
        {
            await foreach (var status in ollama.PullModelAsync(modelName))
                Console.WriteLine($"{status.Percent}% {status.Status}");

        }
        public static async Task ShowAssistantText(string text, Chatbox chatBox)
        {
            TextChatModel textModel = new TextChatModel();
            if (chatBox != null)
            {
                textModel = new TextChatModel()
                {
                    Author = Ollama.ollama!=null? Ollama.ollama.SelectedModel:"No AI Model Found",
                    Inbound = true,
                    Body = "",
                    Read = true,
                    Time = DateTime.Now
                };
                await chatBox.AddMessage(textModel);
                int thisMessageIndex = chatBox.chatItemsPanel.Controls.Count;
                System.Windows.Forms.Control[] chatPanel = chatBox.chatItemsPanel.Controls.Find((thisMessageIndex - 1).ToString(), false).ToArray();
                ChatItem chatItem = chatPanel.FirstOrDefault() as ChatItem;
                System.Windows.Forms.Panel chatItemPanel = chatItem.Parent as System.Windows.Forms.Panel;
                System.Windows.Forms.Control chatText = chatItem.Controls.Find("bodyTextBox", true).FirstOrDefault();

                var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseAutoIdentifiers().UseEmphasisExtras().Build();
                string formattedText = Markdig.Markdown.ToHtml(text, pipeline);
                await Ollama.UpdateChat(formattedText, chatItemPanel, chatText);
            }
        }

        public static List<OllamaSharp.Models.Chat.Message> previousMessages = new List<OllamaSharp.Models.Chat.Message>();
        public static async Task<string> GetAIResponse(string prompt, Chatbox chatBox, bool updateUI = true)
        {
            var chat = new Chat(ollama);
            if(ollama.SelectedModel.Contains("oss"))
                chat.Think = true;
            if (previousMessages != null)
                chat.Messages = previousMessages;
            else

                chat.Options = new RequestOptions { Temperature = 0.7f };
            //while (true)
            {
                Panel chatItemPanel = null;
                Control chatText = null;
                ChatItem chatItem = null;
                if (updateUI && chatBox != null)
                {
                    textModel = new TextChatModel()
                    {
                        Author = ollama.SelectedModel,
                        Inbound = true,
                        Body = "",
                        Read = true,
                        Time = DateTime.Now
                    };
                    await chatBox.AddMessage(textModel);
                    int thisMessageIndex = chatBox.chatItemsPanel.Controls.Count;
                    Control[] chatPanel = chatBox.chatItemsPanel.Controls.Find((thisMessageIndex - 1).ToString(), false).ToArray();
                    chatItem = chatPanel.FirstOrDefault() as ChatItem;
                    //chatItem.Height = 1500;
                    //chatItem.Width = 7000;
                    chatItemPanel = chatItem.Parent as Panel;
                    chatText = chatItem.Controls.Find("bodyTextBox", true).FirstOrDefault();
                }

                // Configure the pipeline with all advanced extensions active
                var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseColorCode().UseEmojiAndSmiley(enableSmileys: true).UseAutoIdentifiers()
                    .UseEmphasisExtras().UseAutoLinks().UseCitations().UseDefinitionLists().UseDiagrams()
                    .UseFigures().UseGridTables().UseListExtras().UseDefinitionLists().UsePipeTables().UseTaskLists().UseCitations()
                    .UseSoftlineBreakAsHardlineBreak().UseCustomContainers().UseGenericAttributes().UseBootstrap().Build();
                //var result = Markdown.ToHtml("This is a text with some *emphasis*", pipeline);
                bool foundCode = false;
                int itteration = 0;
                string fullResponse = "";
                // 3. Send Message & Stream Response
                try
                {
                    //Use Dispatcher.InvokeAsync for non - blocking UI updates

                    IAsyncEnumerable<string> responseTask = chat.SendAsync(prompt);
                    // Iterate over the stream and append to UI
                    await foreach (string answerToken in responseTask)
                    {
                        form.statusStrip1.Text = $"Generating Response.";

                        //if (textModel.InvokeRequired)
                        //{
                        //    // If not on the UI thread, use Invoke to marshal the call
                        //    tbResponse.Invoke((MethodInvoker)delegate
                        //    {
                        //        tbResponse.Text += answerToken;
                        //    });
                        //}
                        if (form.InvokeRequired)
                        {
                            // If not on the UI thread, use Invoke to marshal the call
                            form.Invoke((MethodInvoker)async delegate
                            {
                                fullResponse += answerToken;
                                if (updateUI)
                                {
                                    await UpdateChat(Markdig.Markdown.ToHtml(fullResponse, pipeline), chatItemPanel, chatText);
                                }
                                itteration++;
                            });
                        }
                        else
                        {
                            // If already on the UI thread, update the control directly
                            //tbResponse.Text += answerToken;
                            fullResponse += answerToken;
                            if (updateUI)
                            {
                                await UpdateChat(Markdig.Markdown.ToHtml(fullResponse, pipeline), chatItemPanel, chatText);
                            }
                            itteration++;
                        }
                    }
                    previousMessages.Add(new OllamaSharp.Models.Chat.Message() { Content = prompt });
                    previousMessages.Add(new OllamaSharp.Models.Chat.Message() { Content = fullResponse });
                    form.statusStrip1.Text = $"Response Done.";
                }
                catch (Exception ex)
                {
                    // Handle errors (e.g., Ollama server down)
                    //tbResponse.Invoke((MethodInvoker)delegate
                    //{
                    //    tbResponse.Text += $"\nError: {ex.Message}";
                    //});
                    form.Invoke((MethodInvoker)async delegate
                    {
                        if (updateUI)
                            await UpdateChat($"\nError: {ex.Message}", chatItemPanel, chatText);
                    });
                }
                return fullResponse;
            }
        }
        private static TextChatModel textModel = new TextChatModel();
        public static async Task UpdateChat(string newMessage, Panel chatItemPanel, Control chatText)
        {
            if (newMessage != "")
            {
                //if (form.InvokeRequired)
                {
                    //form.Invoke((MethodInvoker)delegate
                    {
                        ChatItem chatItem = chatText.Parent.Parent as ChatItem;
                        //WebView2 chatControl = (chatText as WebView2);
                        //chatControl.NavigateToString(chatItem.AddHtmlStyles(newMessage));

                            textModel.Body = newMessage;
                        chatText.Text = newMessage;
                        //chatControl.NavigationCompleted += (sender, args) => {
                        //    // Content is fully loaded
                        //    if (args.IsSuccess)
                        //    {
                        chatItem.ResizeBubbles((int)(chatItemPanel.Width * 0.9));
                        //Application.DoEvents();
                        //    }
                        //};
                        //await chatItem.ResizeToContent(chatControl);
                        //var heightString = await chatControl.CoreWebView2.ExecuteScriptAsync("document.body.scrollHeight;");
                        //if (int.TryParse(heightString, out height))
                        //{
                        //	// Resize the container panel
                        //	chatItem.Height = height;
                        //}
                        //int width = 0;
                        //var widthString = await chatControl.CoreWebView2.ExecuteScriptAsync("document.body.scrollHeight;");
                        //if (int.TryParse(widthString, out width))
                        //{
                        //	// Resize the container panel
                        //	chatItem.Width = width;
                        //}


                        //chatItem.ScrollControlIntoView(chatItem);
                        //Application.DoEvents();
                    }
                    //);
                }
                //else
                {
                    //chatText.Text = newMessage;
                    //form.statusStrip1.Text = newMessage;
                    //Application.DoEvents();
                }
            }
            //textModel.Body = newMessage;
        }
        private static void OllamaCommand(string command)
        {
            Process ollamaProcess = null;
            string ollamaSavePath = form.tbOllamaPath.Text;

            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = $"{ollamaSavePath}ollama", // Assumes 'ollama' is in the system's PATH
                                                          //Arguments = $"run {modelName}",
                                                          //Arguments = $"serve",
                    Arguments = $"{command}",
                    UseShellExecute = true,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = false // Set to false if you want to see the console window
                };

                // Start the process
                ollamaProcess = Process.Start(startInfo);

                if (ollamaProcess == null)
                {
                    Console.WriteLine("Failed to start Ollama process.");
                    return;
                }

                Console.WriteLine("Ollama service started in the background.");

                // Optional: You can read the output/error streams for logging or debugging
                ollamaProcess.OutputDataReceived += (sender, e) => form.toolStripStatusLabel1.Text = e.Data;
                //ollamaProcess.ErrorDataReceived += (sender, e) => form.toolStripStatusLabel1.Text = $"ERROR: {e.Data}";
                ollamaProcess.BeginOutputReadLine();
                //ollamaProcess.BeginErrorReadLine();

                //// Keep the main application running while Ollama serves
                //Console.WriteLine("Press any key to stop the Ollama service and exit.");
                //Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Ensure the Ollama process is terminated when the C# app exits
                //if (ollamaProcess != null && !ollamaProcess.HasExited)
                //{
                //    ollamaProcess.Kill();
                //    Console.WriteLine("\nOllama service stopped.");
                //}
            }
        }        

    }

}