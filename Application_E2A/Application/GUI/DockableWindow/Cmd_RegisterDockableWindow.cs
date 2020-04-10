using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;


namespace Application_E2A
{
    /// <summary>
    /// Command that registers DockableWindow Pane
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Cmd_RegisterDockableWindow : IExternalCommand
    {
        #region Public Fields
        public static Cmd_RegisterDockableWindow thisCmd = null;
        public static bool Registered = false;
        public static DockablePaneId dpid;
        #endregion

        #region private Fields
        MainPage MainDockableWindow = null;
        #endregion

        #region Default Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Cmd_RegisterDockableWindow()
        {
            thisCmd = this;
        }
        #endregion

        #region Execute Overloads
        /// <summary>
        /// Method that is executed when Command is fired
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            return this.Execute(commandData.Application);
        }

        /// <summary>
        /// Method that is executed when Command is fired
        /// </summary>
        /// <param name="uiapp"></param>
        /// <returns></returns>
        public Result Execute(UIApplication uiapp)
        {
            this.MainDockableWindow = new MainPage();
            DockablePaneProviderData data = new DockablePaneProviderData();
            dpid = new DockablePaneId(new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));

            MainDockableWindow.SetupDockablePane(data);
            uiapp.RegisterDockablePane(dpid, "SampleTool-Revisions", this.MainDockableWindow as IDockablePaneProvider);

            Registered = true;
            return Result.Succeeded;
        }
        #endregion
    }
}
