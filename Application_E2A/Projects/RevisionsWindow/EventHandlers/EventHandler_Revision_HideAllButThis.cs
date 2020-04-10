using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Application_E2A.Projects;

namespace Application_E2A
{
    public class EventHandler_Revision_HideAllButThis : IExternalEventHandler
    {
        #region Private Fields
        private Revision mElement;
        private Document mDocument;
        #endregion

        #region Public Fields
        public static EventHandler_Revision_HideAllButThis thisCmd;
        #endregion

        #region Default Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EventHandler_Revision_HideAllButThis()
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
            if (this.mElement == null) return;
            List<Revision> AllRevisions = new FilteredElementCollector(this.mDocument)
                .OfClass(typeof(Revision)).ToElements().Cast<Revision>().ToList();

            using (Transaction t = new Transaction(this.mDocument, "HideAllButThis"))
            {
                t.Start();

                foreach (Revision rev in AllRevisions)
                {
                    if (this.mElement.Id.IntegerValue == rev.Id.IntegerValue)
                        SwitchVisiibilityOn(rev);
                    else
                        SwitchVisiibilityOff(rev);
                }

                foreach(RevisionsRevisionViewModel revModel in RevisionsStructureViewModel.ThisInstance.Items)
                {
                    if (revModel.Revision.Id.IntegerValue != this.mElement.Id.IntegerValue)
                        revModel.Visibility = false;
                    else
                        revModel.Visibility = true;
                }

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

        /// <summary>
        /// Switches On visibility for Rev Clouds and Tags
        /// </summary>
        /// <param name="rev"></param>
        private void SwitchVisiibilityOn(Revision rev)
        {
            if (rev == null) return;

            if (rev.Visibility == RevisionVisibility.Hidden)
                rev.Visibility = RevisionVisibility.CloudAndTagVisible;
        }

        /// <summary>
        /// Switches Off visibility for Rev Clouds and Tags
        /// </summary>
        /// <param name="rev"></param>
        private void SwitchVisiibilityOff(Revision rev)
        {
            if (rev == null) return;

            if (rev.Visibility != RevisionVisibility.Hidden)
                rev.Visibility = RevisionVisibility.Hidden;
        }
    }
}
