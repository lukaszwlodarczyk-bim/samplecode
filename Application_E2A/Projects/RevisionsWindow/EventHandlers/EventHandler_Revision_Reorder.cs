using Application_E2A.Projects;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application_E2A
{
    public class EventHandler_Revision_Reorder : IExternalEventHandler
    {
        #region Private Fields
        private WindowRevisionsViewModel instance;
        #endregion

        #region Public Fields
        public static EventHandler_Revision_Reorder thisCmd;
        #endregion

        #region Default Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EventHandler_Revision_Reorder()
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
            using (Transaction t = new Transaction(ThisApplication.thisApp.doc, "ReorderRevisions"))
            {
                t.Start();
                List<Revision> sortedRevisions = new FilteredElementCollector(ThisApplication.thisApp.doc)
                    .OfClass(typeof(Revision)).ToElements().Cast<Revision>().ToList();
                //Sort by date
                sortedRevisions.Sort((x, y) => DateTime.Compare(ConvertToDateTime(x), ConvertToDateTime(y)));

                Revision.ReorderRevisionSequence(ThisApplication.thisApp.doc, sortedRevisions.Select(rev => rev.Id).ToList());
                t.Commit();

                this.instance.RefreshWindow();
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
        /// <param name="instance"></param>
        public void GetData(WindowRevisionsViewModel instance)
        {
            this.instance = instance;
        }

        /// <summary>
        /// Converts string format into DateTime format
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        private DateTime ConvertToDateTime(Revision rev)
        {
            string dataformat = rev.RevisionDate;

            //Assign date from the string
            DateTime temp;
            if (DateTime.TryParse(rev.RevisionDate, out temp))
                return temp;
            else
                return DateTime.Now;
        }

    }
}
