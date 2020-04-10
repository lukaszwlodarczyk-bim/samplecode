using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Application_E2A.Projects
{
    public class RevisionsRevisionViewModel : BaseViewModel
    {
        #region Public Properties
        public RevisionsRevisionViewModel ThisInstance { get; set; }
        public string RevisionName { get; set; }
        public string RevisionDate { get; set; }
        public string IssuedBy { get; set; }
        public Revision Revision { get; set; }
        public bool Visibility { get; set; }
        public bool Issued { get; set; }
        public bool HasErrors { get; set; } = false;
        public ObservableCollection<RevisionsCommentViewModel> Children { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rev"></param>
        public RevisionsRevisionViewModel(Revision rev)
        {
            this.ThisInstance = this;

            this.Revision = rev;
            this.RevisionName = rev.Description;
            this.RevisionDate = rev.RevisionDate;
            this.IssuedBy = rev.IssuedBy;

            this.Issued = rev.Issued;
            this.Visibility = (rev.Visibility == RevisionVisibility.Hidden) ? false : true;

            List<RevisionCloud> CloudsAssignedToRevision = new FilteredElementCollector(ThisApplication.thisApp.doc).OfClass(typeof(RevisionCloud))
                .ToElements().Cast<RevisionCloud>().Where(cloud => cloud.RevisionId.IntegerValue.Equals(rev.Id.IntegerValue)).ToList();


            //Create Children List (List of revision Clouds)
            HashSet<string> uniqueComments = new HashSet<string>(CloudsAssignedToRevision.Select(cloud => cloud.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).AsString()));

            this.Children = new ObservableCollection<RevisionsCommentViewModel>(uniqueComments.Select(uniqueComment => new List<RevisionCloud>(CloudsAssignedToRevision
                .Where(cloud => cloud.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).AsString().Equals(uniqueComment)))).Select(list => new RevisionsCommentViewModel(list, rev.Issued, this)));

        }
        #endregion
    }
}
