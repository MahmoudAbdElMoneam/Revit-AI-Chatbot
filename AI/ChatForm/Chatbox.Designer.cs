
using System.Drawing;
using System.Windows.Forms;

namespace AIChat
{
	partial class Chatbox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.attachButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.chatTextbox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.chatItemsPanel = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bottomPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.SystemColors.ControlText;
            this.bottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomPanel.Controls.Add(this.panel1);
            this.bottomPanel.Controls.Add(this.chatTextbox);
            this.bottomPanel.Controls.Add(this.sendButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPanel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.bottomPanel.Location = new System.Drawing.Point(0, 0);
            this.bottomPanel.Margin = new System.Windows.Forms.Padding(0);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Padding = new System.Windows.Forms.Padding(2);
            this.bottomPanel.Size = new System.Drawing.Size(547, 114);
            this.bottomPanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.attachButton);
            this.panel1.Controls.Add(this.removeButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(393, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(8, 32, 8, 32);
            this.panel1.Size = new System.Drawing.Size(83, 108);
            this.panel1.TabIndex = 8;
            // 
            // attachButton
            // 
            this.attachButton.BackColor = System.Drawing.Color.GhostWhite;
            this.attachButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.attachButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.attachButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.attachButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.attachButton.Image = global::AIChat.Properties.Resources.fileattach;
            this.attachButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.attachButton.Location = new System.Drawing.Point(8, 32);
            this.attachButton.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.attachButton.Name = "attachButton";
            this.attachButton.Padding = new System.Windows.Forms.Padding(0, 12, 0, 12);
            this.attachButton.Size = new System.Drawing.Size(46, 44);
            this.attachButton.TabIndex = 6;
            this.attachButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.attachButton.UseVisualStyleBackColor = false;
            // 
            // removeButton
            // 
            this.removeButton.BackColor = System.Drawing.Color.Red;
            this.removeButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.removeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.removeButton.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold);
            this.removeButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.removeButton.Location = new System.Drawing.Point(50, 32);
            this.removeButton.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(25, 44);
            this.removeButton.TabIndex = 5;
            this.removeButton.Text = "X";
            this.removeButton.UseVisualStyleBackColor = false;
            this.removeButton.Visible = false;
            // 
            // chatTextbox
            // 
            this.chatTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatTextbox.BackColor = System.Drawing.Color.Black;
            this.chatTextbox.ForeColor = System.Drawing.Color.White;
            this.chatTextbox.Location = new System.Drawing.Point(2, 2);
            this.chatTextbox.Margin = new System.Windows.Forms.Padding(0);
            this.chatTextbox.Multiline = true;
            this.chatTextbox.Name = "chatTextbox";
            this.chatTextbox.Size = new System.Drawing.Size(391, 109);
            this.chatTextbox.TabIndex = 7;
            this.chatTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chatTextbox_KeyDown);
            // 
            // sendButton
            // 
            this.sendButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.sendButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.sendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.sendButton.Location = new System.Drawing.Point(476, 2);
            this.sendButton.Margin = new System.Windows.Forms.Padding(2);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(67, 108);
            this.sendButton.TabIndex = 1;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = false;
            // 
            // chatItemsPanel
            // 
            this.chatItemsPanel.AutoScroll = true;
            this.chatItemsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatItemsPanel.Location = new System.Drawing.Point(0, 0);
            this.chatItemsPanel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.chatItemsPanel.Name = "chatItemsPanel";
            this.chatItemsPanel.Size = new System.Drawing.Size(547, 625);
            this.chatItemsPanel.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chatItemsPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.bottomPanel);
            this.splitContainer1.Size = new System.Drawing.Size(547, 741);
            this.splitContainer1.SplitterDistance = 625;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 3;
            // 
            // Chatbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Chatbox";
            this.Size = new System.Drawing.Size(547, 741);
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel bottomPanel;
		private System.Windows.Forms.Button sendButton;
		private System.Windows.Forms.Button attachButton;
		private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.TextBox chatTextbox;
		public System.Windows.Forms.Panel chatItemsPanel;
        private SplitContainer splitContainer1;
        private Panel panel1;
    }
}
