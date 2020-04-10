using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Application_E2A.Projects
{
    /// <summary>
    /// Class that stores the list of Lines (PrintLineViewModel)
    /// </summary>
    public class PrintHeaderViewModel
    {
        #region Properties
        public string HeaderName { get; set; }
        public ObservableCollection<PrintLineViewModel> Children { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="headerName"></param>
        /// <param name="lineItems"></param>
        public PrintHeaderViewModel(string headerName, List<PrintLineViewModel> lineItems)
        {
            this.HeaderName = headerName;
            this.Children = new ObservableCollection<PrintLineViewModel>(lineItems);
        }
        #endregion
    }
}
