using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;


namespace Application_E2A
{
    /// <summary>
    /// Command that maked DockableWindow Visible
    /// </summary>
    class Cmd_ShowDockableWindow : IExternalCommand
    {
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
            return Execute(commandData.Application);
        }

        /// <summary>
        /// Method that is executed when Command is fired
        /// </summary>
        /// <param name="uiapp"></param>
        /// <returns></returns>
        public Result Execute(UIApplication uiapp)
        {
            if ((Cmd_RegisterEvents.thisCmd != null) && (Cmd_RegisterEvents.Registered == true))
            {
                DockablePane dp = uiapp.GetDockablePane(Cmd_RegisterDockableWindow.dpid);
                dp.Show();
                return Result.Succeeded;
            }
            else
            {
                TaskDialog.Show("Cmd_ShowDockableWindow", "DockablePane not Registered. Will not be displayed");
                return Result.Failed;
            }  
        }
        #endregion
    }
}
