using System.Reflection;
using Autodesk.Revit.UI;

namespace Application_E2A
{
    /// <summary>
    /// Class to create Tab in Ribbon Panel and assign data
    /// </summary>
    public class RibbonPanel_E2A 
    {
        #region Public Methods

        /// <summary>
        /// Create Instance of Tab in Ribbon Panel and assign Data
        /// </summary>
        /// <param name="application"></param>
        public static void AddRibbonPanel(UIControlledApplication application)
        {
            // Create a custom ribbon tab
            //Set Tab Name
            string tabName = Constants.uiTabName;
            application.CreateRibbonTab(tabName);

            // Add a new ribbon panel
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "DockableWindow");
            // Get dll assembly path
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            //Assign Data and Actions to Buttons
            PushButtonData b1Data = new PushButtonData(
                "cmdShowDockableWindow",
                "  Show  " + System.Environment.NewLine + "  Tab  ",
                thisAssemblyPath,
                "Application_E2A.RibbonPanel_ShowDockableWindow");

            PushButtonData b2Data = new PushButtonData(
                "cmdHideDockableWindow",
                "  Hide  " + System.Environment.NewLine + "  Tab  ",
                thisAssemblyPath,
                "Application_E2A.RibbonPanel_HideDockableWindow");

            PushButton pb1 = ribbonPanel.AddItem(b1Data) as PushButton;
            pb1.ToolTip = "Show Dockable Tab";
            PushButton pb2 = ribbonPanel.AddItem(b2Data) as PushButton;
            pb2.ToolTip = "Hide Dockable Tab";
        }

        #endregion

        #region private Methods

        /// <summary>
        /// Method that makes Dockable Window Active
        /// </summary>
        private void ShowDocakbleWindow()
        {
            //Acquire UIApplication instance from ThisApplication class
            UIApplication uiapp = ThisApplication.thisApp.uiapp;

            //skip if uiapp is not assigned
            if (uiapp == null) return;

            if ((Cmd_RegisterEvents.thisCmd != null) && (Cmd_RegisterEvents.Registered == true))
            {
                DockablePane dp = uiapp.GetDockablePane(Cmd_RegisterDockableWindow.dpid);
                dp.Show();
            }
            else
            {
                TaskDialog.Show("Cmd_ShowDockableWindow", "DockablePane not Registered. Will not be displayed");
            }
        }

        #endregion
    }
}