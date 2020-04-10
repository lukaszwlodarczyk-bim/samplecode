using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Application_E2A.Projects
{
    /// <summary>
    /// Class that stores the list of Contents (PrintContentViewModel)
    /// </summary>
    public class PrintLineViewModel
    {
        #region Public Properties
        public ObservableCollection<PrintContentViewModel> Children { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="contentMaxWidths"></param>
        public PrintLineViewModel(List<string> contents, List<double> contentMaxWidths)
        {
            //if one of input lists is empty, create empty list of Children
            if ((contents.Count < 1) || (contentMaxWidths.Count < 1) )
            {
                this.Children = new ObservableCollection<PrintContentViewModel>();
                return;
            }

            //if input lists are not empty
            this.Children = new ObservableCollection<PrintContentViewModel>();

            for (int i=0; i<contents.Count; i++)
            {
                this.Children.Add(new PrintContentViewModel(contents[i], contentMaxWidths[i]));
            }
        }
        #endregion
    }
}
