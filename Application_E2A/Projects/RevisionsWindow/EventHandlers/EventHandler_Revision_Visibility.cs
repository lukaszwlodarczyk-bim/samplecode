using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Windows;

namespace Application_E2A
{
    public class EventHandler_Revision_Visibility : IExternalEventHandler
    {
        #region Public Fields
        public static EventHandler_Revision_Visibility thisCmd;
        #endregion

        #region Public Properties
        public Element Element { get; set; }
        public Document Document { get; set; }
        public Result OperationResult { get; set; }
        #endregion

        #region Default Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EventHandler_Revision_Visibility()
        {
            thisCmd = this;
        }
        #endregion

        /// <summary>
        /// Method that is executed when command is fired
        /// </summary>
        /// <param name="app"></param>
        public void Execute(UIApplication app)
        {
            using (Transaction t = new Transaction(this.Document, "RevVisibility"))
            {
                t.Start();
                Revision rev = this.Element as Revision;
                try
                {
                    if (rev == null) return;

                    if (rev.Visibility == RevisionVisibility.Hidden)
                        rev.Visibility = RevisionVisibility.CloudAndTagVisible;
                    else
                        rev.Visibility = RevisionVisibility.Hidden;

                }
                catch(Exception ex){MessageBox.Show(ex.Message + "/n" + ex.StackTrace);}
                t.Commit();
            }
        }

        /// <summary>
        /// Method that has to implemented from interface
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.GetType().Name;
        }

        /// <summary>
        /// Assign Data from Model
        /// </summary>
        /// <param name="el"></param>
        public void AssignDocumentAndElement(Element el)
        {
            this.Document = el.Document;
            this.Element = el;
        }
    }
}
