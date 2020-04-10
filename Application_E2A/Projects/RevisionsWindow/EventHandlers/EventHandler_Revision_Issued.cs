using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Application_E2A
{
    public class EventHandler_Revision_Issued : IExternalEventHandler
    {
        #region Private Fields
        private Revision mElement;
        private Document mDocument;
        #endregion

        #region Public Fields
        public static EventHandler_Revision_Issued thisCmd;
        #endregion

        #region Default Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EventHandler_Revision_Issued()
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
            using (Transaction t = new Transaction(this.mDocument, "RevIssued"))
            {
                t.Start();
                Revision rev = this.mElement;
                try
                {
                    if (rev == null) return;

                    if (rev.Issued == true)
                        rev.Issued = false;
                    else
                        rev.Issued = true;

                }
                catch (Exception ex){MessageBox.Show(ex.Message + "/n" + ex.StackTrace);}
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
        public void AssignDocumentAndElement(Revision el)
        {
            this.mDocument = el.Document;
            this.mElement = el;
        }
    }
}
