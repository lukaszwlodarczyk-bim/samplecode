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
    public class EventHandler_Revision_ChangeRevDescription : IExternalEventHandler
    {
        private Revision Element { get; set; }
        private string NewText { get; set; }
        private string senderName;

        public static EventHandler_Revision_ChangeRevDescription thisCmd;

        #region Default Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EventHandler_Revision_ChangeRevDescription()
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
            if( (this.Element == null) && (this.Element.Issued == true) ) return;

            using (Transaction t = new Transaction(this.Element.Document, "ChangeRevData"))
            {
                t.Start();
                try
                {
                    switch (senderName)
                    {
                        case "Description":
                            this.Element.Description = NewText;
                            break;
                        case "Date":
                            this.Element.RevisionDate = NewText;
                            break;
                        case "IssuedBy":
                            this.Element.IssuedBy = NewText;
                            break;
                    }
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
        /// <param name="newText"></param>
        /// <param name="senderName"></param>
        public void GetData(Revision el, string newText, string senderName)
        {
            this.Element = el;
            this.NewText = newText;
            this.senderName = senderName;
        }
    }
}
