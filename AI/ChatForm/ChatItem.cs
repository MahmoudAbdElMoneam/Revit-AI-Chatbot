using Autodesk.Revit.DB;
//using Microsoft.Web.WebView2.Core;
//using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using AIChat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.WinForms;
using Application = System.Windows.Forms.Application;
using Color = System.Drawing.Color;
using MessageBox = System.Windows.Forms.MessageBox;

namespace AIChat
{
    public partial class ChatItem : UserControl
    {
        public IChatModel ChatModel { get; set; }
        public string settingsFolder = "";
        //public CoreWebView2Environment env = null;
        public ChatItem()
        {
            InitializeComponent();
            //await bodyTextBox.EnsureCoreWebView2Async(null);
            //bodyTextBox.CoreWebView2.NavigateToString("<html>No messages were found.</html>");
            authorLabel.Text = "System " + DateTime.Now.ToShortTimeString();
        }

        public ChatItem(IChatModel chatModel)
        {
            InitializeComponent();

            if (chatModel == null)
            {
                chatModel = new TextChatModel()
                {
                    Author = "System",
                    Body = "Welcome to our AI Chat, you can allow the model to interact with an opened Revit model if you check the box on the left.",
                    Inbound = true,
                    Time = DateTime.Now
                };
            }

            ChatModel = chatModel;
            //From AI Model
            if (chatModel.Inbound)
            {
                //authorLabel.Dock = DockStyle.Left;
                //bodyPanel.Dock = DockStyle.Left;
                //bodyPanel.BackColor = Color.FromArgb(100, 101, 165);
                //bodyTextBox.Dock = DockStyle.Left;
                bodyTextBox.BackColor = Color.FromArgb(100, 101, 165);
            }
            else
            {
                //bodyPanel.Dock = DockStyle.Right;
                //authorLabel.Dock = DockStyle.Right;
                //bodyTextBox.CoreWebView2.TextAlign = HorizontalAlignment.Right;
                bodyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
                bodyTextBox.Dock = DockStyle.Right;
                authorLabel.RightToLeft = RightToLeft.Yes;
                authorLabel.Dock = DockStyle.Right;
            }

            //Fills in the label. 
            if (chatModel.Time > DateTime.Today)
            {
                authorLabel.Text = $"{chatModel.Author ?? "System"}, {chatModel.Time.ToShortTimeString()}";
            }
            else
            {
                authorLabel.Text = $"{chatModel.Author ?? "System"}, {chatModel.Time.ToShortDateString()}";
            }
        }
        public async Task InitializeWebView()
        {
            //if (env == null)
            {
                //env = await CoreWebView2Environment.CreateAsync(
                //userDataFolder: Path.Combine(Path.GetTempPath(), "YourApp"),
                //options: new CoreWebView2EnvironmentOptions(allowSingleSignOnUsingOSPrimaryAccount: true));
            }
            //await bodyTextBox.EnsureCoreWebView2Async(env);
            switch (ChatModel.Type)
            {
                case "text":
                    var textmodel = ChatModel as TextChatModel;
                    string html = AddHtmlStyles(textmodel.Body.Trim());
                    bodyTextBox.Text = html;
                    //bodyTextBox.NavigateToString(html);
                    break;
                case "image":
                    var imagemodel = ChatModel as ImageChatModel;
                    bodyTextBox.Visible = false;
                    //TheArtOfDev.HtmlRenderer.WinForms.HtmlRender.RenderToImage("<p><h1>Hello World</h1>This is html rendered text</p>");
                    //image.Save("image.png", ImageFormat.Png);
                    bodyPanel.BackgroundImage = imagemodel.Image;
                    bodyPanel.BackColor = Color.GhostWhite;
                    bodyPanel.BackgroundImageLayout = ImageLayout.Stretch;
                    break;
                case "attachment":
                    var attachmentmodel = ChatModel as AttachmentChatModel;
                    bodyPanel.BackColor = Color.OrangeRed;
                    bodyTextBox.BackColor = Color.OrangeRed;
                    //bodyTextBox.NavigateToString(AddHtmlStyles("Click to download: {attachmentmodel.Filename}"));
                    bodyTextBox.Text = AddHtmlStyles("Click to download: {attachmentmodel.Filename}");
                    bodyTextBox.Click += DownloadAttachment;
                    break;
                default:
                    break;
            }
            //bodyTextBox.CoreWebView2.Stop();
        }
        public string AddHtmlStyles(string body)
        {
            string htmlContent = @"
                    <html>
                        <head>
                            <!-- Method 1: Internal Stylesheet -->
                            <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"" rel=""stylesheet"">

                            <style>
                                body {
                                    font-family: Arial, sans-serif;color: white;
                                    font-size: 12px;
                                    padding: 0px;
                                    margin: 0px;
                                    height: max-content;"
                                     + (ChatModel.Inbound == true ? @"background-color: #647896;" : @"background-color: #4169E1;") +
                                @"}
                                .chat-container {
                                    padding: 1rem;
                                  }
                                  .message {
                                    max-width: 800px;
                                    margin-bottom: 0.75rem;
                                    padding: 0.75rem 1rem;
                                    border-radius: 0.75rem;
                                    line-height: 1.5;
                                    white-space: pre-wrap;
                                    word-wrap: break-word;
                                  }
                                  .message.user {
                                    margin-left: auto;
                                    background: #0f766e;
                                  }
                                  .message.assistant {
                                    margin-right: auto;
                                    background: #020617;
                                    border: 1px solid #1f2937;
                                  }
                                  pre {
                                    background: #020617;
                                    border-radius: 0.5rem;
                                    padding: 0.75rem;
                                    overflow-x: auto;
                                  }
                                  code {
                                    font-family: Consolas, 'Fira Code', monospace;
                                  }
                                  a {
                                    color: #38bdf8;
                                  }
                            </style>
                        </head>
                        <body>
                            <!-- Method 2: Inline Style -->
                            <div id='container'>"
                                + body +
                            @"</div>
                            <script src=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js""></script>
                        </body>
                    </html>";

            return htmlContent;
        }
        void DownloadAttachment(object sender, EventArgs e)
        {
            var attachmentmodel = ChatModel as AttachmentChatModel;
            if (attachmentmodel.Attachment != null)
            {
                //Borrows the download logic of how browsers download label files. Note that if you are using Mac and Linux, first of all, how did you get this working?
                //But more importantly, fullpath will not lead into your downloads folder, mostly there is now Environment.SpecialFolder.Downloads.
                string fullpath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", attachmentmodel.Filename);
                int count = 1;
                while (System.IO.File.Exists(fullpath))
                {
                    string file = System.IO.Path.GetFileNameWithoutExtension(fullpath);
                    string ext = System.IO.Path.GetExtension(fullpath);
                    string dir = System.IO.Path.GetDirectoryName(fullpath);

                    fullpath = System.IO.Path.Combine(dir, $"{file}({count++}){ext}");
                }

                System.IO.File.WriteAllBytes(fullpath, attachmentmodel.Attachment);
                MessageBox.Show("Attachment " + attachmentmodel.Filename + " was downloaded to the path " + fullpath, "File Downloaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Attachment " + attachmentmodel.Filename + " could not be found.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public async void ResizeBubbles(int maxwidth)
        {
            if (ChatModel == null)
            {
                return;
            }
            else
            {
                //SuspendLayout();

                //The chat bubble is set to the Fill Dockstyle, which means that it recieves all increases in height and width. In order to change the height or width of the chat bubble then,
                //all we really need to do is change the height/width of the control, and add all the padding in between for calculations.
                switch (ChatModel.Type)
                {
                    case "image":
                        //The goal is to resize the image around the restrictions we are given. If the image's width is beyond MaxWidth, then resize it to be smaller.
                        var imagemodel = ChatModel as ImageChatModel;
                        //Have to consider the padding involved in both width and height.
                        if (imagemodel.Image.Width < maxwidth + (Width - bodyPanel.Width))
                        {
                            //Best case scenario: The image width is less than MaxWidth. Then we just need to resize the height so that it is a tight fit.
                            bodyPanel.Width = imagemodel.Image.Width;

                            //Can't set the height of the bodyPanel directly, but we know it scales linearly with Height.
                            Height = imagemodel.Image.Height + (Height - bodyPanel.Height);
                        }
                        else
                        {
                            //This is a slightly harder problem. If the image width is less than MaxWidth, bodyPanel will have to be resized until they match.
                            //This will resize the width, let's find how much the height changes.
                            double ratio = (double)maxwidth / (double)imagemodel.Image.Width;
                            int adjheight = (int)(imagemodel.Image.Height * ratio);

                            bodyPanel.Width = maxwidth;
                            Height = adjheight + (Height - bodyPanel.Height);
                        }
                        break;
                    case "text":
                        var textmodel = ChatModel as TextChatModel;
                        //Ah, this is hell. Alright, so the implementation for this is similar to the image one, except the width can't be automatically calculated. Instead, it
                        //has to be calculated through the size and length of the text. See TextChange().
                        //string body = textmodel.Body;
                        //if (body == "")
                        //    break;
                        //string htmlContent = await bodyTextBox.CoreWebView2.ExecuteScriptAsync("document.documentElement.outerHTML");
                        //string htmlContent = await bodyTextBox.CoreWebView2.ExecuteScriptAsync("document.getElementByTagName('body')");
                        int height = 0;
                        //await TextWebViewChange(body, bodyTextBox);
                        //TextChange(body);
                        await TextHtmlChange();

                        break;
                    case "attachment":
                        var attachmodel = ChatModel as AttachmentChatModel;
                        //TextChange(attachmodel);
                        break;
                    default:
                        break;
                }
            }

            //ResumeLayout();
            async Task TextHtmlChange()
            {
                int stringwidth = bodyTextBox.DisplayRectangle.Width + (bodyTextBox.DisplayRectangle.X *-1);
                System.Drawing.Size chatItemSize = this.SizeFromClientSize(bodyTextBox.ClientSize);
                bool sizeChanged = false;

                var gfx = this.CreateGraphics();                

                var size = HtmlRender.Measure(gfx, bodyTextBox.GetHtml(), 0);
                if (stringwidth != 0 && this.Width != stringwidth)
                {
                    bodyTextBox.Width =Math.Min((int)Math.Round(bodyPanel.Width*0.8), (int)(size.Width+20));
                    sizeChanged = true;
                }
                int height = (int)size.Height;
                    //bodyTextBox.DisplayRectangle.Height + (bodyTextBox.DisplayRectangle.Y * -1);
                if (height != 0 && this.Height != height)
                {
                    //bodyTextBox.Height = height;
                    Height = (int)Math.Round(height * (double)1.11)+100;
                    //this.Height = height + 10;
                    //sizeChanged = true;
                }
                if(sizeChanged)
                    Application.DoEvents();
            }
            //async Task TextWebViewChange(string body, WebView2 webView)
            //{
            //    int stringwidth = await GetWebWidth(webView);
            //    if(stringwidth!=0)
            //        this.Width = stringwidth;
            //    int height = await GetWebHeight(webView);
            //    if (height != 0)
            //        this.Height = height;
            //    //    bodyPanel.Height = Height;
            //}
            void TextChange(string body)
            {
                // Execute script to get the height of the content
                
                int fontheight = 24;
                var gfx = this.CreateGraphics();
                int lines = 1;
                double stringwidth = gfx.MeasureString(body, new Font(FontFamily.GenericSansSerif, 12f)).Width;

                //The system is as follows. The box width can only go to MaxWidth, if it goes to MaxWidth, then wordwrap will bring the text down to a new line.
                //In order to fit it, we'll need to adjust the height by a certain amount of units.
                if (stringwidth < maxwidth + bodyPanel.Width - bodyTextBox.Width)
                {
                    //This is great, we can set the width to be a small as the actual text.
                    bodyPanel.Width = (int)(stringwidth + bodyPanel.Width - bodyTextBox.Width + 5);
                }
                else
                {
                    lines = 0;
                    bodyPanel.Width = maxwidth + bodyPanel.Width - bodyTextBox.Width;
                    string noescapebody = body.Replace("\r\n", string.Empty).Replace("\r\n", string.Empty);
                    stringwidth = gfx.MeasureString(noescapebody, bodyTextBox.Font).Width;

                    while (stringwidth > 0)
                    {
                        stringwidth -= bodyPanel.Width;
                        lines++;
                    }
                }
                if (body.Contains("\n"))
                {
                    while (body.Contains("\r\n"))
                    {
                        body = body.Remove(body.IndexOf("\r\n"), "\r\n".Length);
                        lines++;
                    }
                    while (body.Contains("\n"))
                    {
                        body = body.Remove(body.IndexOf("\n"), "\n".Length);
                        lines++;
                    }
                }

                //Adjusts the height based on the number of lines.
                Height = (lines * fontheight) + Height - bodyTextBox.Height;
            }
        }
    }
}
