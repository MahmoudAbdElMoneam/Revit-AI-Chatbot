
//using Microsoft.Web.WebView2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace AIChat
{
	partial class ChatItem
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
            this.authorPanel = new System.Windows.Forms.Panel();
            this.authorLabel = new System.Windows.Forms.Label();
            this.bodyPanel = new System.Windows.Forms.Panel();
            this.bodyTextBox = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.authorPanel.SuspendLayout();
            this.bodyPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // authorPanel
            // 
            this.authorPanel.Controls.Add(this.authorLabel);
            this.authorPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.authorPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.authorPanel.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.authorPanel.Location = new System.Drawing.Point(15, 58);
            this.authorPanel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.authorPanel.MaximumSize = new System.Drawing.Size(0, 40);
            this.authorPanel.Name = "authorPanel";
            this.authorPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.authorPanel.Size = new System.Drawing.Size(163, 40);
            this.authorPanel.TabIndex = 8;
            // 
            // authorLabel
            // 
            this.authorLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.authorLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            this.authorLabel.Location = new System.Drawing.Point(0, 8);
            this.authorLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(239, 32);
            this.authorLabel.TabIndex = 0;
            this.authorLabel.Text = "System - 10/22/2020";
            this.authorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bodyPanel
            // 
            this.bodyPanel.BackColor = System.Drawing.SystemColors.ControlText;
            this.bodyPanel.Controls.Add(this.bodyTextBox);
            this.bodyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bodyPanel.Location = new System.Drawing.Point(15, 8);
            this.bodyPanel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.bodyPanel.Name = "bodyPanel";
            this.bodyPanel.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bodyPanel.Size = new System.Drawing.Size(163, 50);
            this.bodyPanel.TabIndex = 9;
            // 
            // bodyTextBox
            // 
            this.bodyTextBox.AutoScroll = true;
            this.bodyTextBox.AutoScrollMinSize = new System.Drawing.Size(155, 30);
            this.bodyTextBox.BackColor = System.Drawing.SystemColors.Highlight;
            this.bodyTextBox.BaseStylesheet = null;
            this.bodyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bodyTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.bodyTextBox.Location = new System.Drawing.Point(3, 2);
            this.bodyTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.bodyTextBox.Name = "bodyTextBox";
            this.bodyTextBox.Padding = new System.Windows.Forms.Padding(5);
            this.bodyTextBox.Size = new System.Drawing.Size(157, 46);
            this.bodyTextBox.TabIndex = 7;
            this.bodyTextBox.Text = "HTML";
            // 
            // ChatItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.Controls.Add(this.bodyPanel);
            this.Controls.Add(this.authorPanel);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MinimumSize = new System.Drawing.Size(193, 106);
            this.Name = "ChatItem";
            this.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            this.Size = new System.Drawing.Size(193, 106);
            this.authorPanel.ResumeLayout(false);
            this.bodyPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel authorPanel;
		private System.Windows.Forms.Label authorLabel;
		private System.Windows.Forms.Panel bodyPanel;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel bodyTextBox;
    }
}
