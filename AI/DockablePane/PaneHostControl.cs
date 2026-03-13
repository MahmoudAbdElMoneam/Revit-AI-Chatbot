using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Control = System.Windows.Forms.Control;
using Panel = System.Windows.Forms.Panel;
using UserControl = System.Windows.Forms.UserControl;

namespace AIChat.AI.DockablePane
{
    /// <summary>
    /// Interaction logic for PaneHostControl.xaml
    /// </summary>
    public partial class PaneHostControl : UserControl
    {
        public PaneHostControl(AiChatForm form)
        {
            this.Dock = DockStyle.Fill;
            if(form!=null)
            AddWindowsForm(form);
        }
        public bool addedForm = false;
        public void AddWindowsForm(AiChatForm form)
        {
            //WindowsFormsHost host = new WindowsFormsHost();
            //AiChatForm userForm = new AiChatForm(app, subAiExternalEvent, subAiHandler);
            //userForm.TopLevel = false; // Must be set to false to be embedded
            //userForm.FormBorderStyle = FormBorderStyle.None; // Optional: remove form border
            //host.Child = userForm;
            //host.Children.Add(host); // Add host to your WPF layout container
            //userForm.Show(); // Show the form within the host
            // Create your WinForms form/control
            if (!addedForm)
            {
                addedForm = true;
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                // If ChatForm is now a UserControl:
                this.Controls.Add(form);

                // Optional, if you want it visible immediately
                form.Show();
            }
            // Important: you cannot host a top-level Form directly,
            // so either:
            // - convert it to inherit from UserControl, OR
            // - take its Controls and add them into a WinForms Panel.

            //var panel = new Panel { Dock = DockStyle.Fill, BorderStyle = BorderStyle.None };
            //foreach (Control c in form.Controls)
            //{
            //    c.Parent = null;
            //    panel.Controls.Add(c);
            //}

            //// Host WinForms panel inside WPF
            //host.Child = panel;
        }
    }
}
