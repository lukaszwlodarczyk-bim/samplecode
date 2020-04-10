using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Application_E2A.Projects
{
    public class RevisionsSheetGroupViewModel : BaseViewModel
    {
        #region Public Properties
        public RevisionsHintViewModel ThisInstance { get; set; }
        public string GroupName { get; set; }
        public ObservableCollection<RevisionsHintViewModel> Children { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="AllSheets"></param>
        /// <param name="rev"></param>
        public RevisionsSheetGroupViewModel(string groupName, List<ViewSheet> AllSheets, Revision rev)
        {
            this.GroupName = groupName;
            //Get All Sheet with assigned revision
            List<ViewSheet> AssignedSheets = new List<ViewSheet>(AllSheets.Where(sheet => CheckForRevision(sheet, rev)));

            switch (groupName)
            {
                case "Suggestions":
                    if (AssignedSheets.Count == 0) break;
                    //Get All Unique Cluster_In values
                    HashSet<string> uniqueClusters = new HashSet<string>();

                    List<ViewSheet> temp = new List<ViewSheet>();
                    try
                    {
                        uniqueClusters = new HashSet<string>(AssignedSheets.Select(sheet => sheet.LookupParameter(Constants.ViewClusterIn).AsString()));
                        List<ViewSheet> UnassignedSheets = new List<ViewSheet>(AllSheets.Where(sheet => !CheckForRevision(sheet, rev)));

                        foreach(ViewSheet sheet in UnassignedSheets)
                        {
                            if (uniqueClusters.Contains(sheet.LookupParameter(Constants.ViewClusterIn).AsString()))
                                temp.Add(sheet);
                        }
                    }
                    catch { }
                    if (temp.Count > 0) temp.OrderBy(sheet => sheet.SheetNumber);

                    this.Children = new ObservableCollection<RevisionsHintViewModel>(temp.Select(sheet => new RevisionsHintViewModel(sheet)));
                    break;

                case "Assigned Sheets":
                    this.Children = new ObservableCollection<RevisionsHintViewModel>(AssignedSheets.Select(sheet => new RevisionsHintViewModel(sheet)));
                    break;
            }
        }
        #endregion

        /// <summary>
        /// Check if Revision is on the ViewSheet
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rev"></param>
        /// <returns></returns>
        private bool CheckForRevision(ViewSheet sheet, Revision rev)
        {
            bool result = false;
            if ((sheet == null) || (rev == null)) return result;
            if (sheet.GetAllRevisionIds().Count == 0) return result;

            foreach(ElementId id in sheet.GetAllRevisionIds())
            {
                if (id.IntegerValue == rev.Id.IntegerValue) return true;
            }
            return result;
        }
    }
}
