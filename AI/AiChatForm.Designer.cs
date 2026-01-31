using System.Drawing;
using System.Windows.Forms;

namespace AIChat
{
    partial class AiChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCollapse = new System.Windows.Forms.Button();
            this.panelChat = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbRetrieveAndReAsk = new System.Windows.Forms.CheckBox();
            this.cbInteractRevit = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbOllamaPath = new System.Windows.Forms.TextBox();
            this.btnBrowseOllama = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAIModels = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCollapse);
            this.groupBox1.Controls.Add(this.panelChat);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 26, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(1028, 818);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnCollapse
            // 
            this.btnCollapse.Image = global::AIChat.Properties.Resources.settings_32;
            this.btnCollapse.Location = new System.Drawing.Point(0, 3);
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.Size = new System.Drawing.Size(63, 45);
            this.btnCollapse.TabIndex = 2;
            this.btnCollapse.UseVisualStyleBackColor = true;
            this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
            // 
            // panelChat
            // 
            this.panelChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChat.Location = new System.Drawing.Point(3, 41);
            this.panelChat.Name = "panelChat";
            this.panelChat.Size = new System.Drawing.Size(1022, 773);
            this.panelChat.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 2);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1334, 818);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbRetrieveAndReAsk);
            this.groupBox2.Controls.Add(this.cbInteractRevit);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbAIModels);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(302, 818);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // cbRetrieveAndReAsk
            // 
            this.cbRetrieveAndReAsk.AutoSize = true;
            this.cbRetrieveAndReAsk.Checked = true;
            this.cbRetrieveAndReAsk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRetrieveAndReAsk.Location = new System.Drawing.Point(14, 206);
            this.cbRetrieveAndReAsk.Name = "cbRetrieveAndReAsk";
            this.cbRetrieveAndReAsk.Size = new System.Drawing.Size(200, 21);
            this.cbRetrieveAndReAsk.TabIndex = 6;
            this.cbRetrieveAndReAsk.Text = "Allow retrieval and asking AI";
            this.cbRetrieveAndReAsk.UseVisualStyleBackColor = true;
            // 
            // cbInteractRevit
            // 
            this.cbInteractRevit.AutoSize = true;
            this.cbInteractRevit.Checked = true;
            this.cbInteractRevit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInteractRevit.Location = new System.Drawing.Point(14, 167);
            this.cbInteractRevit.Name = "cbInteractRevit";
            this.cbInteractRevit.Size = new System.Drawing.Size(203, 21);
            this.cbInteractRevit.TabIndex = 6;
            this.cbInteractRevit.Text = "Interact With Revit Document";
            this.cbInteractRevit.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tbOllamaPath);
            this.groupBox3.Controls.Add(this.btnBrowseOllama);
            this.groupBox3.Location = new System.Drawing.Point(6, 30);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(294, 76);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ollama path";
            // 
            // tbOllamaPath
            // 
            this.tbOllamaPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOllamaPath.Location = new System.Drawing.Point(9, 33);
            this.tbOllamaPath.Margin = new System.Windows.Forms.Padding(4);
            this.tbOllamaPath.Name = "tbOllamaPath";
            this.tbOllamaPath.Size = new System.Drawing.Size(152, 22);
            this.tbOllamaPath.TabIndex = 1;
            this.tbOllamaPath.TextChanged += new System.EventHandler(this.tbOllamaPath_TextChanged);
            // 
            // btnBrowseOllama
            // 
            this.btnBrowseOllama.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseOllama.Location = new System.Drawing.Point(172, 33);
            this.btnBrowseOllama.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseOllama.Name = "btnBrowseOllama";
            this.btnBrowseOllama.Size = new System.Drawing.Size(112, 34);
            this.btnBrowseOllama.TabIndex = 0;
            this.btnBrowseOllama.Text = "Browse";
            this.btnBrowseOllama.UseVisualStyleBackColor = true;
            this.btnBrowseOllama.Click += new System.EventHandler(this.btnBrowseOllama_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "AI Model";
            // 
            // cbAIModels
            // 
            this.cbAIModels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAIModels.FormattingEnabled = true;
            this.cbAIModels.Location = new System.Drawing.Point(110, 116);
            this.cbAIModels.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbAIModels.Name = "cbAIModels";
            this.cbAIModels.Size = new System.Drawing.Size(188, 24);
            this.cbAIModels.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 816);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1340, 36);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(111, 28);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(190, 29);
            this.toolStripStatusLabel1.Text = "Select Model and Chat";
            // 
            // AiChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1340, 852);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AiChatForm";
            this.ShowIcon = false;
            this.Text = "Local AI Bot";
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbAIModels;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private Panel panelChat;
        private GroupBox groupBox3;
        private Button btnBrowseOllama;
        public TextBox tbOllamaPath;
        public CheckBox cbInteractRevit;
        public CheckBox cbRetrieveAndReAsk;
        private Button btnCollapse;
    }
}

