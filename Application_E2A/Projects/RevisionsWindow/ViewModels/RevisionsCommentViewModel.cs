using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Application_E2A.Projects
{
    public class RevisionsCommentViewModel : BaseViewModel
    {
        #region Public Properties
        public RevisionsCommentViewModel ThisInstance { get; set; }
        public RevisionsRevisionViewModel Parent { get; set; }
        public string Comment { get; set; } = "";
        public ObservableCollection<RevisionsViewViewModel> Children { get; set; }
        public bool Issued { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="revClouds"></param>
        /// <param name="issued"></param>
        /// <param name="parent"></param>
        public RevisionsCommentViewModel(List<RevisionCloud> revClouds, bool issued, RevisionsRevisionViewModel parent)
        {
            this.ThisInstance = this;
            this.Issued = issued;
            this.Parent = parent;

            try
            {
                this.Comment = revClouds.First().get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).AsString();
            }
            catch { }

            this.Children = new ObservableCollection<RevisionsViewViewModel>(revClouds.Select(cloud => new RevisionsViewViewModel(cloud, this, issued)));
        }
        #endregion
    }
}
