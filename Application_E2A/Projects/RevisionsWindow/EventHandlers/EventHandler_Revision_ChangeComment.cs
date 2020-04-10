using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
using System;
using System.Windows;

namespace Application_E2A.Projects
{
    public class EventHandler_Revision_ChangeComment : IExternalEventHandler
    {
        #region Private Fields
        private RevisionsCommentViewModel Comment;
        private string newText;
        #endregion

        #region Public Fields
        public static EventHandler_Revision_ChangeComment thisCmd;
        #endregion

        #region Default Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EventHandler_Revision_ChangeComment()
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
            if (this.Comment == null) return;

            Document doc = this.Comment.Children.First().Cloud.Document;
            if ((doc.GetElement(this.Comment.Children.First().Cloud.RevisionId) as Revision).Issued == false)
            {
                using (Transaction t = new Transaction(doc, "RevCommentChange"))
                {
                    t.Start();
                    try
                    {
                        foreach (RevisionsViewViewModel viewCloud in this.Comment.Children)
                        {
                            viewCloud.Cloud.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set(this.newText);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message + "/n" + ex.StackTrace); }
                    t.Commit();
                }
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

        /// <summary>
        /// Assign Data from Model
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="newText"></param>
        public void GetData(RevisionsCommentViewModel comment, string newText)
        {
            this.Comment = comment;
            this.newText = newText;
        }
    }
}
