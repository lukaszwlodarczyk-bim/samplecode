using System.Windows;
using System.Windows.Input;

namespace Application_E2A.Projects
{
    /// <summary>
    /// ViewModel Class for Window Revisions (View)
    /// </summary>
    public class WindowRevisionsViewModel : WindowViewModel, IBaseWindow
    {
        #region Private Fields
        private Window mWindow;
        #endregion

        #region Public Properties
        public WindowRevisionsViewModel ThisInstance { get; set; }
        public RevisionsStructureViewModel RevisionsStructureViewModel { get; set; }
        public ICommand RefreshCommand { get; set; }
        public string Title { get; set; }
        public string NumberOfIncorrect { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="window"></param>
        public WindowRevisionsViewModel(Window window) : base(window)
        {
            this.mWindow = window;
            this.ThisInstance = this;
            this.Title = Constants.uiWindowTitleRevisions;
            Globals.NumberOfIncorrectRevisions = 0;

            //Create Data Structure
            this.RevisionsStructureViewModel = new RevisionsStructureViewModel();
            this.NumberOfIncorrect = Globals.NumberOfIncorrectRevisions.ToString();

            this.RefreshCommand = new RelayCommand(this.RefreshWindow);
        }
        #endregion

        /// <summary>
        /// Recreates new instance of window
        /// </summary>
        public void RefreshWindow()
        {
            this.mWindow.DataContext = new WindowRevisionsViewModel(this.mWindow);
        }
    }
}
