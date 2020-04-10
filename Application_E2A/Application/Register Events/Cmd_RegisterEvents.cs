using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application_E2A.Projects;

namespace Application_E2A
{
    /// <summary>
    /// Class that registers all events/actions that require proceeding transactions in Revit DB
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Cmd_RegisterEvents : IExternalCommand
    {
        #region Public Fields
        public static Cmd_RegisterEvents thisCmd = null;
        public static bool Registered = false;

        #region GENERIC ExEvents Declarations
        public ExternalEvent ExEvent_Generic_GoToView;
        public ExternalEvent ExEvent_Generic_ZoomViewToElement;
        public ExternalEvent ExEvent_Generic_RemoveInstance;
        #endregion

        #region REVISIONS ExEvents Declarations
        public ExternalEvent ExEvent_Revision_Issued;
        public ExternalEvent ExEvent_Revision_Visibility;
        public ExternalEvent ExEvent_Revision_ChangeComment;
        public ExternalEvent ExEvent_Revision_ChangeRevDescription;
        public ExternalEvent ExEvent_Revision_NewRevision;
        public ExternalEvent ExEvent_Revision_HideAllButThis;
        public ExternalEvent ExEvent_Revision_Reorder;
        #endregion

        #endregion

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            return Execute(commandData.Application);
        }

        public Result Execute(UIApplication uiapp)
        {
            thisCmd = this;

            //For Each action that requires change in Revit DB (open transaction)
            //Initialize new instance of Event Handler
            //Assign Event Handler instance to ExEvent
            #region Assigning Handlers to ExEvents

            #region GENERIC
            EventHandler_Generic_GoToView handler_Generic_GoToView = new EventHandler_Generic_GoToView();
            this.ExEvent_Generic_GoToView = ExternalEvent.Create(handler_Generic_GoToView);

            EventHandler_Generic_ZoomViewToElement handler_Generic_ZoomViewToElement = new EventHandler_Generic_ZoomViewToElement();
            this.ExEvent_Generic_ZoomViewToElement = ExternalEvent.Create(handler_Generic_ZoomViewToElement);

            EventHandler_Generic_RemoveInstance handler_Generic_RemoveInstance = new EventHandler_Generic_RemoveInstance();
            this.ExEvent_Generic_RemoveInstance = ExternalEvent.Create(handler_Generic_RemoveInstance);
            #endregion

            #region REVISIONS
            EventHandler_Revision_Issued handler_Revision_Issued = new EventHandler_Revision_Issued();
            this.ExEvent_Revision_Issued = ExternalEvent.Create(handler_Revision_Issued);

            EventHandler_Revision_Visibility handler_Revision_Visibility = new EventHandler_Revision_Visibility();
            this.ExEvent_Revision_Visibility = ExternalEvent.Create(handler_Revision_Visibility);

            EventHandler_Revision_ChangeComment handler_Revision_ChangeComment = new EventHandler_Revision_ChangeComment();
            this.ExEvent_Revision_ChangeComment = ExternalEvent.Create(handler_Revision_ChangeComment);

            EventHandler_Revision_ChangeRevDescription handler_Revision_ChangeRevDescription = new EventHandler_Revision_ChangeRevDescription();
            this.ExEvent_Revision_ChangeRevDescription = ExternalEvent.Create(handler_Revision_ChangeRevDescription);

            EventHandler_Revision_NewRevision handler_Revision_NewRevision = new EventHandler_Revision_NewRevision();
            this.ExEvent_Revision_NewRevision = ExternalEvent.Create(handler_Revision_NewRevision);

            EventHandler_Revision_HideAllButThis handler_Revision_HideAllButThis = new EventHandler_Revision_HideAllButThis();
            this.ExEvent_Revision_HideAllButThis = ExternalEvent.Create(handler_Revision_HideAllButThis);

            EventHandler_Revision_Reorder handler_Revision_Reorder = new EventHandler_Revision_Reorder();
            this.ExEvent_Revision_Reorder = ExternalEvent.Create(handler_Revision_Reorder);
            #endregion

            #endregion

            Registered = true;
            return Result.Succeeded;
        }
    }
}
