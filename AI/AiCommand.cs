#region Namespaces
using AIChat.AI;
using AIChat.AI.DockablePane;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using ColorCode.Compilation.Languages;
using Newtonsoft.Json;
using Revit.Async;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.IO; // requires System.Web.Extensions
using System.IO;
using System.Linq;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using UIFramework;
using Application = Autodesk.Revit.ApplicationServices.Application;
using RibbonItem = Autodesk.Revit.UI.RibbonItem;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;
using TaskDialog = Autodesk.Revit.UI.TaskDialog;

#endregion

namespace AIChat
{
    public class AiApp : IExternalApplication
    {
        #region Data
        public static DockablePaneId dockablePaneId = new DockablePaneId(new Guid("{0C5FFF75-285A-4360-94B9-FBC12CE8DF73}"));
        internal static AiApp thisApp = null;
        private string appName = "AI Chat";
        MainPage m_mainPage;
        private PaneHostControl _paneControl;
        UIApplication uiApplication;
        #endregion
        /// <summary>
        /// Register a dockable Window
        /// </summary>
        public void RegisterDockableWindow(UIApplication application, Guid mainPageGuid)
        {
            uiApplication = application;
            dockablePaneId = new DockablePaneId(mainPageGuid);
            application.RegisterDockablePane(dockablePaneId, appName, GetMainWindow() as IDockablePaneProvider);
        }
        public void RegisterDockableWindow(UIControlledApplication application, Guid mainPageGuid)
        {            
            dockablePaneId = new DockablePaneId(mainPageGuid);
            application.RegisterDockablePane(dockablePaneId, appName, GetMainWindow() as IDockablePaneProvider);
        }
        //public void SetupDockablePane(DockablePaneProviderData data)
        //{
        //    data.FrameworkElement = _paneControl;  // IMPORTANT
        //    data.InitialState = new DockablePaneState
        //    {
        //        DockPosition = DockPosition.Right
        //    };
        //}
        /// <summary>
        /// Create the new WPF Page that Revit will dock.
        /// </summary>
        public void CreateWindow()
        {
            m_mainPage = new MainPage(null);
        }
        /// <summary>
        /// Show or hide a dockable pane.
        /// </summary>
        public void SetWindowVisibility(Autodesk.Revit.UI.UIApplication application, bool state)
        {
            DockablePane pane = application.GetDockablePane(dockablePaneId);
            if (pane != null)
            {
                bool isShown = pane.IsShown();
                if (!isShown)
                    pane.Show();
                else
                    pane.Hide();
            }
        }
        public bool IsMainWindowAvailable()
        {
            if (m_mainPage == null)
                return false;

            bool isAvailable = true;
            try { bool isVisible = m_mainPage.IsVisible; }
            catch (Exception)
            {
                isAvailable = false;
            }
            return isAvailable;
        }
        public MainPage GetMainWindow()
        {
            if (!IsMainWindowAvailable())
            {
                CreateWindow();
            }
            //throw new InvalidOperationException("Main window not constructed.");
            return m_mainPage;
        }
        public Autodesk.Revit.UI.DockablePaneId MainPageDockablePaneId
        {

            get { return dockablePaneId; }
        }
        public Result OnStartup(UIControlledApplication uiApp)
        {
            thisApp = this;
            //// 2. Register with Revit
            //_paneControl = new PaneHostControl();
            AiApp.thisApp.RegisterDockableWindow(uiApp, AiApp.dockablePaneId.Guid);
            // Create a new ribbon tab
            string tabName = "AI Tools";
            string panelName = "AI";
            string btnName = "AIChat";
            string btnText = "AI Chat";
            string commandPath = "AIChat.AiCommand";
            string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string assemblyFolder = Directory.GetParent(assemblyPath) + "\\";
            string resourcesFolder = Directory.GetParent(assemblyFolder).Parent.FullName;
            resourcesFolder += "\\" + "Resources" + "\\";

            RibbonControl ribbon = ComponentManager.Ribbon;
            bool foundTab = false;
            if (ribbon != null || ribbon.Tabs.Count > 0)
            {
                foreach (RibbonTab tab in ribbon.Tabs)
                {
                    if (tab.Name == tabName)
                    { foundTab = true; break; }
                }
            }
            if (!foundTab)
                uiApp.CreateRibbonTab(tabName);

            // Create a new ribbon panel on the custom tab
            List<RibbonPanel> panels = uiApp.GetRibbonPanels(tabName);

            RibbonPanel myPanel = (from p in panels
                                   where p.Name == panelName
                                   select p).FirstOrDefault();
            myPanel ??= uiApp.CreateRibbonPanel(tabName, panelName);

            // Create PushButtonData for your command
            PushButtonData buttonData = new(
                btnName, // Internal name
                btnText, // Text on the button
                assemblyPath, // Path to your assembly
                 commandPath  // Full class name of your IExternalCommand
            );
            List<RibbonItem> panelItems = myPanel.GetItems().ToList();
            for (int i = 0; i < panelItems.Count(); i++)
            {
                RibbonItem ri = panelItems[i];
                if (ri.Name == btnName)
                    return Result.Succeeded;
            }
            // Add the button to the panel
            PushButton myButton = myPanel.AddItem(buttonData) as PushButton;
            myButton.LongDescription = "Must have Ollama installed.";
            // Add an icon (LargeImage for 32x32, Image for 16x16)
            myButton.Image = new BitmapImage(new Uri($"{resourcesFolder}AIChat.png"));
            myButton.LargeImage = new BitmapImage(new Uri($"{resourcesFolder}AIChat.png"));
            myButton.AvailabilityClassName = "AIChat.ZeroDocAvailability";
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
    public class ZeroDocAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication app, CategorySet selectedCategories)
        {
            // Allow running without an active document
            return true;
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class AiCommand : IExternalCommand
    {
        public static ExternalEvent AiExternalEvent;
        public static AiExternalEventHandler AiHandler;
        public static AiScriptJob CurrentJob = new AiScriptJob();
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            var executor = new RevitCodeExecutor();
            AiHandler = new AiExternalEventHandler(executor);
            AiExternalEvent = ExternalEvent.Create(AiHandler);

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;


            RevitTask.Initialize(uiapp);
            ButtonAiChatCommand toRun = new ButtonAiChatCommand();
            toRun.uiApp = uiapp;
            toRun.subAiExternalEvent = AiExternalEvent;
            toRun.subAiHandler = AiHandler;
            toRun.Execute(commandData);
            //AiChatForm form = new AiChatForm(uiapp);
            //form.Show();
            return Result.Succeeded;
        }

        private string testCode(UIApplication app)
        {

            // Common Revit variables available to the snippet:
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = uidoc.Document;

            // ---- BEGIN MODEL CODE ----
            // ---- END MODEL CODE ----
            return "succeeded";
        }
    }



    public class ButtonAiChatCommand : ICommand
    {
        public string errors = "";
        public string settingsFolder = "";
        public Dictionary<string, string> settings = [];
        public ExternalEvent subAiExternalEvent;
        public AiExternalEventHandler subAiHandler;
        private bool addedFormToWPF = false;
        //public static ExternalEvent AiExternalEvent;
        //public static AiExternalEventHandler AiHandler;
        //public static AiScriptJob CurrentJob = new AiScriptJob();
        public UIApplication uiApp { get; set; }
        public ExternalCommandData commandData { get; set; }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        public async void Execute(object parameter)
        {
            commandData = (ExternalCommandData)parameter;
            await RevitTask.RunAsync(
                async app =>
                {
                    AiChatForm form = new AiChatForm(app, subAiExternalEvent, subAiHandler);                    
                    //form.Show();
                    try
                    {
                        //bool isPaneRegistered = false;
                        //try
                        //{
                        //    commandData.Application.GetDockablePane(AiApp.dockablePaneId);
                        //    isPaneRegistered = true;
                        //}
                        //catch (Exception ex)
                        //{
                        //    isPaneRegistered = false;
                        //}

                        // 1. Instantiate Panel
                        //if (!AiApp.thisApp.IsMainWindowAvailable())

                        if (!addedFormToWPF)
                        {
                            addedFormToWPF = true;
                            //    AiApp.thisApp.GetMainWindow().AddWindowsForm(form);
                            //}
                            AiApp.thisApp.GetMainWindow().DataContext = form;
                            AiApp.thisApp.GetMainWindow().AddForm(form);
                        }
                        AiApp.thisApp.SetWindowVisibility(commandData.Application, true);
                    }
                    catch (Exception)
                    {
                        TaskDialog.Show("Dockable Dialogs", "Dialog not registered.");
                    }
                    errors = "";
                });

        }
    }
    // Helper class to wrap the Revit window handle
    public class JtWindowHandle : IWin32Window
    {
        private IntPtr _hwnd;
        public JtWindowHandle(IntPtr h) { _hwnd = h; }
        public IntPtr Handle { get { return _hwnd; } }
    }
}
