using System.Collections.Generic;
using System.Windows.Controls;

namespace Application_E2A.Projects
{
    public partial class PageToPrint : Page
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageTotalNumber"></param>
        /// <param name="headers"></param>
        public PageToPrint(int pageNumber, int pageTotalNumber, List<PrintHeaderViewModel> headers)
        {
            InitializeComponent();
            this.DataContext = new PrintStructureViewModel(pageNumber, pageTotalNumber, headers);
        }
        #endregion
    }
}
