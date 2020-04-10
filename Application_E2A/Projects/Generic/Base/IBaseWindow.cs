using System.Windows;
using System.Windows.Input;

namespace Application_E2A.Projects
{
    public interface IBaseWindow
    {
        #region Public Properties
        ICommand RefreshCommand { get; set; }
        string Title { get; set; }
        #endregion

        #region Public Methods
        void RefreshWindow();
        #endregion
    }
}
