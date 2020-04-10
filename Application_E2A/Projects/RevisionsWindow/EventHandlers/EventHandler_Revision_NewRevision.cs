using Application_E2A.Projects;
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
    public class EventHandler_Revision_NewRevision : IExternalEventHandler
    {
        #region Private Fields
        private RevisionsStructureViewModel treeData;
        #endregion

        #region Public Fields
        public static EventHandler_Revision_NewRevision thisCmd;
        #endregion

        #region Default Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EventHandler_Revision_NewRevision()
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
            Revision newRev = null;
            using (Transaction t = new Transaction(ThisApplication.thisApp.doc, "New Revision"))
            {
                t.Start();
                try
                {
                    newRev = Revision.Create(ThisApplication.thisApp.doc);
                    newRev.NumberType = RevisionNumberType.Alphanumeric;
                }
                catch (Exception ex){MessageBox.Show(ex.Message + "/n" + ex.StackTrace);}
                t.Commit();
            }
            //creates new revision - as last in the revision stack
            this.treeData.Items.Insert(0, new RevisionsRevisionViewModel(newRev));
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
        /// <param name="tree"></param>
        public void GetData(RevisionsStructureViewModel tree)
        {
            this.treeData = tree;
        }
    }
}
