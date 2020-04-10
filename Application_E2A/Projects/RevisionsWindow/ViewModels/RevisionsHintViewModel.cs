using Autodesk.Revit.DB;

namespace Application_E2A.Projects
{
    public class RevisionsHintViewModel : BaseViewModel
    {
        #region Public Properties
        public RevisionsHintViewModel ThisInstance { get; set; }
        public string Name { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sheet"></param>
        public RevisionsHintViewModel(ViewSheet sheet)
        {
            try
            {
                this.Name = sheet.LookupParameter(Constants.ViewClusterIn).AsString() + " | " + sheet.SheetNumber + " | " + sheet.Name;
            }
            catch { this.Name = "ViewSheet - View_Cluser not accessible"; }
        }
        #endregion
    }
}
