using Autodesk.Revit.UI;
using Newtonsoft.Json;
using OllamaSharp;
using OllamaSharp.Models;
using AIChat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIChat
{
    public partial class AiChatForm : Form
    {
        public string settingsFolder = "";
		public string settingsPath = "";
		public Dictionary<string, string> settings = [];

		public AiChatForm(UIApplication app)
        {
            InitializeComponent();

			ChatboxInfo cbi = new ChatboxInfo();
            cbi.NamePlaceholder = "";
            cbi.PhonePlaceholder = "";

            var chat_panel = new Chatbox(cbi, app, this);
            chat_panel.Name = "chat_panel";
            chat_panel.Dock = DockStyle.Fill;
            panelChat.Controls.Add(chat_panel);

            string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            settingsFolder = Directory.GetParent(assemblyPath).Parent.FullName;
            settingsFolder += "\\Settings\\";
			if (!Directory.Exists(settingsFolder))
				Directory.CreateDirectory(settingsFolder);
            settingsFolder += app.Application.Username+ "\\";
            if (!Directory.Exists(settingsFolder))
                Directory.CreateDirectory(settingsFolder);
            settingsPath = settingsFolder + "ollama.json";
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
                tbOllamaPath.Text = folderPath+"\\";
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

            string settingsContent = JsonConvert.SerializeObject(settings);
			File.WriteAllText(settingsPath, settingsContent);
		}

        private async void tbOllamaPath_TextChanged(object sender, EventArgs e)
        {
				await startOllama();
        }

		int panel1Width = 300;
        private void btnCollapse_Click(object sender, EventArgs e)
        {
			if (splitContainer1.Panel1.Width > 0)
				splitContainer1.Panel1.Width = 0;
			else
				splitContainer1.Panel1.Width = panel1Width;
        }
    }
}
