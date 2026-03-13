using Autodesk.Revit.UI;
using ColorCode.Compilation.Languages;
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

namespace AIChat.AI.DockablePane
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class MainPage : Page, IDockablePaneProvider
    {
        private string m_mainPageGuid;
        public MainPage(AiChatForm form)
        {
            InitializeComponent();
            m_mainPageGuid = AiApp.thisApp.MainPageDockablePaneId.Guid.ToString();

            // Host your WinForms control
            if (form != null)
            {
                var myControl = new PaneHostControl(form);
                winFormHost.Child = myControl;
            }
        }
       public void AddForm(AiChatForm form)
        {
            if (form != null)
            {
                var myControl = new PaneHostControl(form);
                winFormHost.Child = myControl;
            }
        }
        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this;
            data.VisibleByDefault = false; // Hidden by default
            data.InitialState = new DockablePaneState()
            {
                DockPosition = DockPosition.Tabbed,
                TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser
            };
        }
        /// <summary>
        /// The guid of the main docking page.
        /// </summary>
        public Guid MainPageGuid
        {
            get
            {
                Guid retval = Guid.Empty;
                if (m_mainPageGuid == "null")
                    return retval;
                else
                {

                    try
                    {
                        retval = new Guid(m_mainPageGuid);

                    }
                    catch (Exception)
                    {
                    }
                    return retval;
                }
            }
        }
    }
}
