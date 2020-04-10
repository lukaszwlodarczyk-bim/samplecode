using System;
using System.Windows;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;

namespace Application_E2A
{
    public class ThisApplication : IExternalApplication
    {
        internal static ThisApplication thisApp = null;

        #region Public Fields
        public UIApplication uiapp = null;
        public UIDocument uidoc = null;
        public Document doc = null;

        public string projectNumber = string.Empty;
        public static bool Window_Revisions_Opened;
        #endregion

        #region OnStrartup
        /// <summary>
        /// Actions to Be Performed when Application Starts
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                thisApp = this;

                //Assign Handlers to Events
                application.ControlledApplication.ApplicationInitialized += new EventHandler<ApplicationInitializedEventArgs>(ControlledApplication_ApplicationInitialized);
                application.ControlledApplication.DocumentOpening += new EventHandler<DocumentOpeningEventArgs>(ControlledApplication_DocumentOpening);
                application.ControlledApplication.DocumentOpened += new EventHandler<DocumentOpenedEventArgs>(ControlledApplication_DocumentOpened);
                application.ViewActivated += new EventHandler<ViewActivatedEventArgs>(UIControlledApplication_ViewActivated);

                //Add Tab to Revit Panel, in case DockableWindow is not visible and has to be fired from button
                RibbonPanel_E2A.AddRibbonPanel(application);

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("error", ex.ToString());
                return Result.Failed;
            }
        }
        #endregion

        #region OnShutdown
        /// <summary>
        /// Actions to Be Performed when Application Closes
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
        #endregion

        #region private Events Handlers

        #region ApplicationInitialized
        /// <summary>
        /// Actions performed when application has been initialized but Document is not opened yet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlledApplication_ApplicationInitialized(object sender, Autodesk.Revit.DB.Events.ApplicationInitializedEventArgs e)
        {
            if (this.uiapp == null)
                this.uiapp = new UIApplication(sender as Autodesk.Revit.ApplicationServices.Application);

            //Register Dockable Window Event
            Cmd_RegisterDockableWindow cmd_RegisterWindow = new Cmd_RegisterDockableWindow();
            cmd_RegisterWindow.Execute(this.uiapp);

            //Register All Events that would require open transactions in Revit DB
            Cmd_RegisterEvents cmd_RegisterEvents = new Cmd_RegisterEvents();
            cmd_RegisterEvents.Execute(this.uiapp);
        }
        #endregion

        #region DocumentOpening
        /// <summary>
        /// Actions performed whilst Document is opening
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlledApplication_DocumentOpening(object sender, Autodesk.Revit.DB.Events.DocumentOpeningEventArgs e)
        {
            if (this.uiapp == null)
                this.uiapp = new UIApplication(sender as Autodesk.Revit.ApplicationServices.Application);

            //register events if haven't been registered yet
            if ((Cmd_RegisterDockableWindow.thisCmd == null) && (Cmd_RegisterDockableWindow.Registered == false))
            {
                Cmd_RegisterDockableWindow cmd_RegisterWindow = new Cmd_RegisterDockableWindow();
                cmd_RegisterWindow.Execute(this.uiapp);
            }

            if ((Cmd_RegisterEvents.thisCmd == null) && (Cmd_RegisterEvents.Registered == false))
            {
                Cmd_RegisterEvents cmd_RegisterEvents = new Cmd_RegisterEvents();
                cmd_RegisterEvents.Execute(this.uiapp);
            }
        }
        #endregion

        #region DocumentOpened
        /// <summary>
        /// Actions performed when Document has been opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ControlledApplication_DocumentOpened(object sender, DocumentOpenedEventArgs args)
        {
            this.doc = args.Document;
            this.uidoc = thisApp.uiapp.ActiveUIDocument;
            this.projectNumber = this.doc.Title;

            //register events if haven't been registered yet
            if ((Cmd_RegisterDockableWindow.thisCmd == null) && (Cmd_RegisterDockableWindow.Registered == false))
            {
                Cmd_RegisterDockableWindow cmd_RegisterWindow = new Cmd_RegisterDockableWindow();
                cmd_RegisterWindow.Execute(this.uiapp);
            }

            if ((Cmd_RegisterEvents.thisCmd == null) && (Cmd_RegisterEvents.Registered == false))
            {
                Cmd_RegisterEvents cmd_RegisterEvents = new Cmd_RegisterEvents();
                cmd_RegisterEvents.Execute(this.uiapp);
            }

            Cmd_ShowDockableWindow cmd_ShowWindow = new Cmd_ShowDockableWindow();
            cmd_ShowWindow.Execute(sender as UIApplication);
        }
        #endregion

        #region ViewActivated
        /// <summary>
        /// Actions performed when any view has been made ActiveView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIControlledApplication_ViewActivated(object sender, ViewActivatedEventArgs e)
        {
            if ((this.doc != null) && (thisApp.doc != e.CurrentActiveView.Document))
            {
                this.doc = e.CurrentActiveView.Document;
                this.projectNumber = this.doc.Title;
            }  
        }
        #endregion

        #endregion
    }
}
