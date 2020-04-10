using System;
using System.Windows;
using System.Windows.Controls;
using Application_E2A.Projects;


//using Application_E2A.Projects;
using Autodesk.Revit.UI;

namespace Application_E2A
{
    /// <summary>
    /// DockablePane initial Window 
    /// </summary>
    public partial class MainPage : Page, Autodesk.Revit.UI.IDockablePaneProvider
    {
        #region Default Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Set Presets for DockablePane
        /// </summary>
        /// <param name="data"></param>
        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this as FrameworkElement;
            data.InitialState = new DockablePaneState();
            //Set DockablePane Window as Tab in Properties Palette
            data.InitialState.DockPosition = DockPosition.Tabbed;
            data.InitialState.TabBehind = DockablePanes.BuiltInDockablePanes.PropertiesPalette;
        }
        #endregion

        #region private Methods

        #region MANAGEMENT

        /// <summary>
        /// Button to fire Revisions Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Revision_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //check if Revisions window is already open
                if (ThisApplication.Window_Revisions_Opened == false)
                {
                    //Initialize new instance of Revisions Window
                    Window_Revisions window = new Window_Revisions();
                    window.Show();
                }
                else
                {
                    //if Revisions Window is already created
                    if (Window_Revisions.ThisWindowInstance != null)
                    {
                        //Set Revisions Window as active
                        Window_Revisions.ThisWindowInstance.Visibility = System.Windows.Visibility.Visible;
                        Window_Revisions.ThisWindowInstance.Activate();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n" + ex.StackTrace); }
        }

        #endregion

        #endregion
    }
}
