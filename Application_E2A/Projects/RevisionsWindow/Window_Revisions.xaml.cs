using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Application_E2A.Projects
{
    /// <summary>
    /// Interaction logic for Window_Revisions.xaml
    /// </summary>
    public partial class Window_Revisions : Window
    {
        #region Public Fields
        public static Window_Revisions ThisWindowInstance;
        #endregion

        #region Default Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Window_Revisions()
        {
            InitializeComponent();
            ThisWindowInstance = this;

            ThisApplication.Window_Revisions_Opened = true;
        }
        #endregion

        #region private events MAIN
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new WindowRevisionsViewModel(this);
        }
        #endregion

        #region private events HEADER

        private void Header_NewRevision(object sender, RoutedEventArgs e)
        {
            try
            {
                RevisionsStructureViewModel tree = ((Button)sender).Tag as RevisionsStructureViewModel;

                EventHandler_Revision_NewRevision.thisCmd.GetData(tree);
                Cmd_RegisterEvents.thisCmd.ExEvent_Revision_NewRevision.Raise();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace); }
        }

        private void Header_Reorder(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowRevisionsViewModel instance = ((Button)sender).Tag as WindowRevisionsViewModel;

                EventHandler_Revision_Reorder.thisCmd.GetData(instance);
                Cmd_RegisterEvents.thisCmd.ExEvent_Revision_Reorder.Raise();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace); }
        }

        #endregion

        #region private events REVISION

        private void MenuItem_Revision_Print(object sender, RoutedEventArgs e)
        {
            RevisionsRevisionViewModel instance = ((MenuItem)sender).Tag as RevisionsRevisionViewModel;

            try
            {
                List<PrintHeaderViewModel> Headers = new List<PrintHeaderViewModel>();
                int counter = 0;

                Headers.Add(new PrintHeaderViewModel(instance.RevisionName + " / " + instance.RevisionDate, new List<PrintLineViewModel>(instance.Children
                    .Select(comment => new PrintLineViewModel(new List<string>()
                        {
                        (counter=counter+1).ToString() +".",
                        comment.Comment
                        },
                        PageInfo.ColumnWidths_Revisions)))));


                //Assign PageInfo
                PageInfo.AssignPageInfo_Revisions("Revision");

                //Add additional empty line
                List<PrintHeaderViewModel> HeadersWithAdditionalEmptyLines = Utilities.ForEachRecordAddEmptyLine(Headers, PageInfo.ColumnWidths_Revisions);

                //Send Complete data to print
                Utilities.PrintA4Vertical(HeadersWithAdditionalEmptyLines, "Revision_"+instance.RevisionName);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace); }
        }

        private void MenuItem_Revision_SwitchVisibility(object sender, RoutedEventArgs e)
        {
            try
            {
                RevisionsRevisionViewModel instance = ((MenuItem)sender).Tag as RevisionsRevisionViewModel;
                Revision target = instance.Revision;
                //instance.Visibility = (instance.Visibility == true) ? false : true;

                EventHandler_Revision_Visibility.thisCmd.AssignDocumentAndElement(target);
                Cmd_RegisterEvents.thisCmd.ExEvent_Revision_Visibility.Raise();
            }
            catch (Exception ex){MessageBox.Show(ex.Message + "/n" + ex.StackTrace);}
        }

        private void MenuItem_Revision_HideAllButThis(object sender, RoutedEventArgs e)
        {
            try
            {
                RevisionsRevisionViewModel instance = ((MenuItem)sender).Tag as RevisionsRevisionViewModel;
                Revision target = instance.Revision;

                EventHandler_Revision_HideAllButThis.thisCmd.AssignDocumentAndElement(target);
                Cmd_RegisterEvents.thisCmd.ExEvent_Revision_HideAllButThis.Raise();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "/n" + ex.StackTrace); }
        }

        private void MenuItem_Revision_Issued(object sender, RoutedEventArgs e)
        {
            try
            {
                RevisionsRevisionViewModel instance = ((MenuItem)sender).Tag as RevisionsRevisionViewModel;
                Revision target = instance.Revision;
                //instance.Issued = (instance.Issued == true) ? false : true;
                
                foreach(RevisionsCommentViewModel comment in instance.Children)
                {
                    comment.Issued = instance.Issued;
                    foreach(RevisionsViewViewModel view in comment.Children)
                    {
                        view.Issued = instance.Issued;
                    }
                }
                
                EventHandler_Revision_Issued.thisCmd.AssignDocumentAndElement(target);
                Cmd_RegisterEvents.thisCmd.ExEvent_Revision_Issued.Raise();
            }
            catch (Exception ex) {MessageBox.Show(ex.Message + "/n" + ex.StackTrace);}
        }

        private void MenuItem_Revision_DisplayHint(object sender, RoutedEventArgs e)
        {
            RevisionsRevisionViewModel instance = ((MenuItem)sender).Tag as RevisionsRevisionViewModel;
            Revision target = instance.Revision;

            Window_RevisionsHint window = new Window_RevisionsHint(target);
            window.ShowDialog();
        }

        private void TextBox_Rev_Desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    RevisionsRevisionViewModel instance = (RevisionsRevisionViewModel)((TextBox)sender).Tag;
                    string newTextValue = ((TextBox)sender).Text;

                    EventHandler_Revision_ChangeRevDescription.thisCmd.GetData(instance.Revision, newTextValue, "Description");
                    Cmd_RegisterEvents.thisCmd.ExEvent_Revision_ChangeRevDescription.Raise();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message + "\n" + ex.StackTrace); }
            }
        }

        private void TextBox_Rev_Date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    RevisionsRevisionViewModel instance = (RevisionsRevisionViewModel)((TextBox)sender).Tag;
                    string newTextValue = ((TextBox)sender).Text;

                    EventHandler_Revision_ChangeRevDescription.thisCmd.GetData(instance.Revision, newTextValue, "Date");
                    Cmd_RegisterEvents.thisCmd.ExEvent_Revision_ChangeRevDescription.Raise();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message + "\n" + ex.StackTrace); }
            }
        }

        private void TextBox_Rev_IssuedBy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    RevisionsRevisionViewModel instance = (RevisionsRevisionViewModel)((TextBox)sender).Tag;
                    string newTextValue = ((TextBox)sender).Text;

                    EventHandler_Revision_ChangeRevDescription.thisCmd.GetData(instance.Revision, newTextValue, "IssuedBy");
                    Cmd_RegisterEvents.thisCmd.ExEvent_Revision_ChangeRevDescription.Raise();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message + "\n" + ex.StackTrace); }
            }
        }

        #endregion

        #region private events COMMENT

        private void TextBox_Comment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    RevisionsCommentViewModel instance = (RevisionsCommentViewModel)((TextBox)sender).Tag;
                    string newTextValue = ((TextBox)sender).Text;

                    EventHandler_Revision_ChangeComment.thisCmd.GetData(instance, newTextValue);
                    Cmd_RegisterEvents.thisCmd.ExEvent_Revision_ChangeComment.Raise();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message + "\n" + ex.StackTrace); }
            }
        }

        #endregion

        #region private events REVISIION CLOUD

        private void MenuItem_Cloud_GoTo(object sender, RoutedEventArgs e)
        {
            Element element = ((RevisionsViewViewModel)((MenuItem)sender).Tag).Cloud;

            if (ThisApplication.thisApp.uidoc.ActiveView.Id != element.OwnerViewId)
                Utilities.MakeViewActive(ThisApplication.thisApp.uidoc, element.OwnerViewId);

            if (element is AnnotationSymbol)
            {
                Autodesk.Revit.UI.UIView uiview = Utilities.GetActiveUIView(ThisApplication.thisApp.uidoc);
                XYZ[] corners = Utilities.GetElementRectangle(element, 10, 10);
                Utilities.ZoomToRectangle(uiview, corners);
            }
            else
                ThisApplication.thisApp.uidoc.ShowElements(element);
        }

        private void MenuItem_Cloud_Select(object sender, RoutedEventArgs e)
        {
            try
            {
                RevisionsViewViewModel instance = ((RevisionsViewViewModel)((MenuItem)sender).Tag);
                Utilities.SelectElement(ThisApplication.thisApp.uidoc, instance.Cloud);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "/n" + ex.StackTrace); }
        }

        private void MenuItem_Cloud_Remove(object sender, RoutedEventArgs e)
        {
            RevisionsViewViewModel instance = (RevisionsViewViewModel)((MenuItem)sender).Tag;
            Element element = instance.Cloud;

            EventHandler_Generic_RemoveInstance.thisCmd.AssignDocumentAndElement(element);
            Cmd_RegisterEvents.thisCmd.ExEvent_Generic_RemoveInstance.Raise();

            instance.Parent.Children.Remove(instance);
        }

        #endregion

    }
}
