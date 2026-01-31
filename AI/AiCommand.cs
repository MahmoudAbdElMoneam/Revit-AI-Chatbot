#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using Newtonsoft.Json;
using Revit.Async;
using AIChat.AI;
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
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Application = Autodesk.Revit.ApplicationServices.Application;
using RibbonItem = Autodesk.Revit.UI.RibbonItem;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;

#endregion

namespace AIChat
{
    internal class AiApp : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication uiApp)
        {
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
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;


            RevitTask.Initialize(uiapp);
            ButtonAiChatCommand toRun = new ButtonAiChatCommand();
            toRun.uiApp = uiapp;
            toRun.Execute(this);
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

        public static ExternalEvent AiExternalEvent;
        public static AiExternalEventHandler AiHandler;
        public static AiScriptJob CurrentJob = new AiScriptJob();
        public UIApplication uiApp { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        public async void Execute(object parameter)
        {
            await RevitTask.RunAsync(
                async app =>
                {
                    var executor = new RevitCodeExecutor();
                    AiHandler = new AiExternalEventHandler(executor);
                    AiExternalEvent = ExternalEvent.Create(AiHandler);
                    AiChatForm form = new AiChatForm(app);
                    
                    // Webview initialisation handler, called when control instantiated and ready
                    form.Show();
                    errors = "";
                });

        }
    }
}
