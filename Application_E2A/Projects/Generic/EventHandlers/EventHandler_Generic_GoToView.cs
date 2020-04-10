using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace Application_E2A
{
    /// <summary>
    /// Event Handler to execute GoToView Action
    /// </summary>
    public class EventHandler_Generic_GoToView : IExternalEventHandler
    {
        #region Private Fields
        private Element mElement;
        #endregion

        #region Public Fields
        public static EventHandler_Generic_GoToView thisCmd;
        #endregion

        #region Default Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EventHandler_Generic_GoToView()
        {
            thisCmd = this;
        }
        #endregion

        /// <summary>
        /// Method that is performed when Handler is executed
        /// </summary>
        /// <param name="app"></param>
        public void Execute(UIApplication app)
        {
            if(this.mElement is AnnotationSymbol)
            {
                if (app.ActiveUIDocument.ActiveView.Id != this.mElement.OwnerViewId)
                    Utilities.MakeViewActive(app.ActiveUIDocument, this.mElement.OwnerViewId);

                UIView uiview = Utilities.GetActiveUIView(ThisApplication.thisApp.uidoc);
                XYZ[] corners = Utilities.GetElementRectangle(this.mElement, 10, 10);
                Utilities.ZoomToRectangle(uiview, corners);
            }
            else
            {
                app.ActiveUIDocument.ShowElements(this.mElement);
            }
        }

        /// <summary>
        /// Method that has to be implemented from interface
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.GetType().Name;
        }

    }
}
