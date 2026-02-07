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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCollapse = new System.Windows.Forms.Button();
            this.panelChat = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnDeleteAIModel = new System.Windows.Forms.Button();
            this.cbAIModels = new System.Windows.Forms.ComboBox();
            this.cbRetrieveAndReAsk = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbInteractRevit = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnDeletePrompt = new System.Windows.Forms.Button();
            this.tbPrompt = new System.Windows.Forms.TextBox();
            this.btnAddPrompt = new System.Windows.Forms.Button();
            this.cbPromptName = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbOllamaPath = new System.Windows.Forms.TextBox();
            this.btnBrowseOllama = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 22, 2, 3);
            this.groupBox1.Size = new System.Drawing.Size(674, 593);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnCollapse
            // 
            this.btnCollapse.Image = global::AIChat.Properties.Resources.settings_32;
            this.btnCollapse.Location = new System.Drawing.Point(-3, 3);
            this.btnCollapse.Margin = new System.Windows.Forms.Padding(2);
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.Size = new System.Drawing.Size(52, 40);
            this.btnCollapse.TabIndex = 2;
            this.btnCollapse.UseVisualStyleBackColor = true;
            this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
            // 
            // panelChat
            // 
            this.panelChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChat.Location = new System.Drawing.Point(2, 37);
            this.panelChat.Margin = new System.Windows.Forms.Padding(2);
            this.panelChat.Name = "panelChat";
            this.panelChat.Size = new System.Drawing.Size(670, 553);
            this.panelChat.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 2);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(987, 593);
            this.splitContainer1.SplitterDistance = 310;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Size = new System.Drawing.Size(310, 593);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.btnDeleteAIModel);
            this.groupBox5.Controls.Add(this.cbAIModels);
            this.groupBox5.Controls.Add(this.cbRetrieveAndReAsk);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.cbInteractRevit);
            this.groupBox5.Location = new System.Drawing.Point(6, 94);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(299, 135);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "AI Model Settings";
            // 
            // btnDeleteAIModel
            // 
            this.btnDeleteAIModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteAIModel.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeleteAIModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAIModel.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteAIModel.Image = global::AIChat.Properties.Resources.delete;
            this.btnDeleteAIModel.Location = new System.Drawing.Point(265, 34);
            this.btnDeleteAIModel.Name = "btnDeleteAIModel";
            this.btnDeleteAIModel.Size = new System.Drawing.Size(28, 23);
            this.btnDeleteAIModel.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnDeleteAIModel, "Save or Add new System Prompt");
            this.btnDeleteAIModel.UseVisualStyleBackColor = false;
            this.btnDeleteAIModel.Click += new System.EventHandler(this.btnDeleteAIModel_Click);
            // 
            // cbAIModels
            // 
            this.cbAIModels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAIModels.FormattingEnabled = true;
            this.cbAIModels.Location = new System.Drawing.Point(98, 33);
            this.cbAIModels.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbAIModels.Name = "cbAIModels";
            this.cbAIModels.Size = new System.Drawing.Size(162, 24);
            this.cbAIModels.TabIndex = 3;
            this.cbAIModels.SelectedIndexChanged += new System.EventHandler(this.cbAIModels_SelectedIndexChanged_1);
            this.cbAIModels.Enter += new System.EventHandler(this.tb_Enter);
            this.cbAIModels.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // cbRetrieveAndReAsk
            // 
            this.cbRetrieveAndReAsk.AutoSize = true;
            this.cbRetrieveAndReAsk.Checked = true;
            this.cbRetrieveAndReAsk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRetrieveAndReAsk.Location = new System.Drawing.Point(18, 108);
            this.cbRetrieveAndReAsk.Margin = new System.Windows.Forms.Padding(2);
            this.cbRetrieveAndReAsk.Name = "cbRetrieveAndReAsk";
            this.cbRetrieveAndReAsk.Size = new System.Drawing.Size(196, 20);
            this.cbRetrieveAndReAsk.TabIndex = 6;
            this.cbRetrieveAndReAsk.Text = "Allow retrieval and asking AI";
            this.toolTip1.SetToolTip(this.cbRetrieveAndReAsk, "2 Step execution. Allow running code to retrieve data, then execute the user\'s qu" +
        "ery.");
            this.cbRetrieveAndReAsk.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "AI Model";
            // 
            // cbInteractRevit
            // 
            this.cbInteractRevit.AutoSize = true;
            this.cbInteractRevit.Checked = true;
            this.cbInteractRevit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInteractRevit.Location = new System.Drawing.Point(18, 75);
            this.cbInteractRevit.Margin = new System.Windows.Forms.Padding(2);
            this.cbInteractRevit.Name = "cbInteractRevit";
            this.cbInteractRevit.Size = new System.Drawing.Size(199, 20);
            this.cbInteractRevit.TabIndex = 6;
            this.cbInteractRevit.Text = "Interact With Revit Document";
            this.toolTip1.SetToolTip(this.cbInteractRevit, "Allow reading and writing operations on the Revit File");
            this.cbInteractRevit.UseVisualStyleBackColor = true;
            this.cbInteractRevit.CheckedChanged += new System.EventHandler(this.cbInteractRevit_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnDeletePrompt);
            this.groupBox4.Controls.Add(this.tbPrompt);
            this.groupBox4.Controls.Add(this.btnAddPrompt);
            this.groupBox4.Controls.Add(this.cbPromptName);
            this.groupBox4.Location = new System.Drawing.Point(5, 235);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(300, 350);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "System Prompts";
            this.toolTip1.SetToolTip(this.groupBox4, "Instructions to the AI model to consider before providing answers to user\'s queri" +
        "es.");
            // 
            // btnDeletePrompt
            // 
            this.btnDeletePrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeletePrompt.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeletePrompt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePrompt.ForeColor = System.Drawing.Color.Black;
            this.btnDeletePrompt.Image = global::AIChat.Properties.Resources.delete;
            this.btnDeletePrompt.Location = new System.Drawing.Point(239, 31);
            this.btnDeletePrompt.Name = "btnDeletePrompt";
            this.btnDeletePrompt.Size = new System.Drawing.Size(28, 23);
            this.btnDeletePrompt.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnDeletePrompt, "Save or Add new System Prompt");
            this.btnDeletePrompt.UseVisualStyleBackColor = false;
            this.btnDeletePrompt.Click += new System.EventHandler(this.btnDeletePrompt_Click);
            // 
            // tbPrompt
            // 
            this.tbPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPrompt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPrompt.Location = new System.Drawing.Point(8, 62);
            this.tbPrompt.Multiline = true;
            this.tbPrompt.Name = "tbPrompt";
            this.tbPrompt.Size = new System.Drawing.Size(292, 282);
            this.tbPrompt.TabIndex = 2;
            this.tbPrompt.Enter += new System.EventHandler(this.tb_Enter);
            this.tbPrompt.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // btnAddPrompt
            // 
            this.btnAddPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPrompt.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddPrompt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPrompt.ForeColor = System.Drawing.Color.Black;
            this.btnAddPrompt.Image = global::AIChat.Properties.Resources.save;
            this.btnAddPrompt.Location = new System.Drawing.Point(272, 31);
            this.btnAddPrompt.Name = "btnAddPrompt";
            this.btnAddPrompt.Size = new System.Drawing.Size(28, 23);
            this.btnAddPrompt.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnAddPrompt, "Save or Add new System Prompt");
            this.btnAddPrompt.UseVisualStyleBackColor = false;
            this.btnAddPrompt.Click += new System.EventHandler(this.btnAddPrompt_Click);
            // 
            // cbPromptName
            // 
            this.cbPromptName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPromptName.FormattingEnabled = true;
            this.cbPromptName.Location = new System.Drawing.Point(8, 31);
            this.cbPromptName.Name = "cbPromptName";
            this.cbPromptName.Size = new System.Drawing.Size(225, 24);
            this.cbPromptName.TabIndex = 0;
            this.cbPromptName.SelectedValueChanged += new System.EventHandler(this.cbPromptName_SelectedValueChanged);
            this.cbPromptName.Enter += new System.EventHandler(this.tb_Enter);
            this.cbPromptName.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tbOllamaPath);
            this.groupBox3.Controls.Add(this.btnBrowseOllama);
            this.groupBox3.Location = new System.Drawing.Point(5, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 63);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ollama path";
            // 
            // tbOllamaPath
            // 
            this.tbOllamaPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOllamaPath.Location = new System.Drawing.Point(8, 28);
            this.tbOllamaPath.Name = "tbOllamaPath";
            this.tbOllamaPath.Size = new System.Drawing.Size(185, 22);
            this.tbOllamaPath.TabIndex = 1;
            this.tbOllamaPath.TextChanged += new System.EventHandler(this.tbOllamaPath_TextChanged);
            this.tbOllamaPath.Enter += new System.EventHandler(this.tb_Enter);
            this.tbOllamaPath.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // btnBrowseOllama
            // 
            this.btnBrowseOllama.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseOllama.Location = new System.Drawing.Point(201, 28);
            this.btnBrowseOllama.Name = "btnBrowseOllama";
            this.btnBrowseOllama.Size = new System.Drawing.Size(93, 28);
            this.btnBrowseOllama.TabIndex = 0;
            this.btnBrowseOllama.Text = "Browse";
            this.btnBrowseOllama.UseVisualStyleBackColor = true;
            this.btnBrowseOllama.Click += new System.EventHandler(this.btnBrowseOllama_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 590);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(992, 31);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(92, 23);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(159, 25);
            this.toolStripStatusLabel1.Text = "Select Model and Chat";
            // 
            // AiChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(992, 621);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(598, 628);
            this.Name = "AiChatForm";
            this.ShowIcon = false;
            this.Text = "Local AI Bot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AiChatForm_FormClosing);
            this.Load += new System.EventHandler(this.AiChatForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private GroupBox groupBox4;
        public TextBox tbPrompt;
        private Button btnAddPrompt;
        private ComboBox cbPromptName;
        private ToolTip toolTip1;
        private GroupBox groupBox5;
        private Button btnDeletePrompt;
        private Button btnDeleteAIModel;
    }
}

