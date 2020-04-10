using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace Application_E2A.Projects
{
    /// <summary>
    /// Interaction logic for Window_MigrateSheets.xaml
    /// </summary>
    public partial class Window_RevisionsHint : Window
    {
        #region Private Fields
        private Revision Rev;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rev"></param>
        public Window_RevisionsHint(Revision rev)
        {
            this.Rev = rev;
            InitializeComponent();
        }
        #endregion

        #region private Events MAIN
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new WindowRevisionsHintViewModel(this, this.Rev);
        }
        #endregion
    }
}
