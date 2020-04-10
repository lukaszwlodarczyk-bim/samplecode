using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Application_E2A.Projects
{
    /// <summary>
    /// ViewModel Class for Window Revision Hints (View)
    /// </summary>
    public class WindowRevisionsHintViewModel : WindowViewModel
    {
        #region Private Fields
        private Window mWindow;
        #endregion

        #region Public Properties
        public string Title { get; set; } = Constants.uiWindowTitleRevHints;
        public ObservableCollection<RevisionsSheetGroupViewModel> Items { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="window"></param>
        /// <param name="rev"></param>
        public WindowRevisionsHintViewModel(Window window, Revision rev) : base(window, true)
        {
            this.mWindow = window;
            this.RefreshButtonVisible = System.Windows.Visibility.Hidden;
            this.WindowMinimumWidth = 250;
            this.WindowMinimumHeight = 250;

            List<string> sheetgroups = new List<string>() { "Suggestions", "Assigned Sheets" };
            List<ViewSheet> AllSheets = new FilteredElementCollector(ThisApplication.thisApp.doc).OfClass(typeof(ViewSheet))
                        .ToElements().Cast<ViewSheet>().Where(sheet => sheet.get_Parameter(BuiltInParameter.SHEET_SCHEDULED).AsInteger().Equals(1)).ToList();

            this.Items = new ObservableCollection<RevisionsSheetGroupViewModel>(sheetgroups.Select(name => new RevisionsSheetGroupViewModel(name, AllSheets, rev)));
        }
        #endregion
    }
}
