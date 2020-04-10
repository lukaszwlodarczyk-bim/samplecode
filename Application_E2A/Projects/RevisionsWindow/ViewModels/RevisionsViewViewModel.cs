using Autodesk.Revit.DB;

namespace Application_E2A.Projects
{
    public class RevisionsViewViewModel : BaseViewModel
    {
        #region Public Fields
        public RevisionsCommentViewModel Parent;
        #endregion

        #region Public Properties
        public RevisionsViewViewModel ThisInstance { get; set; } = null;
        public View View { get; set; } = null;
        public string ViewName { get; set; } = string.Empty;
        public RevisionCloud Cloud { get; set; } = null;
        public bool Issued { get; set; } = false;
        public bool HasErrors { get; set; } = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cloud"></param>
        /// <param name="parent"></param>
        /// <param name="issued"></param>
        public RevisionsViewViewModel(RevisionCloud cloud, RevisionsCommentViewModel parent, bool issued)
        {
            if(cloud != null)
            {
                this.ThisInstance = this;
                this.Parent = parent;
                this.Issued = issued;
                this.View = ThisApplication.thisApp.doc.GetElement(cloud.OwnerViewId) as View;

                this.Cloud = cloud;
                this.ViewName = this.View.LookupParameter(Constants.ViewClusterIn).AsString() + " | " + this.View.Name;

                //Check if RevCloud is correctly placed on the ViewSheet
                ViewSheet sheet = this.View as ViewSheet;
                if (sheet == null)
                {
                    Globals.NumberOfIncorrectRevisions++;
                    this.Parent.Parent.HasErrors = true;
                    this.HasErrors = true;
                }
            }
        }
        #endregion
    }
}
