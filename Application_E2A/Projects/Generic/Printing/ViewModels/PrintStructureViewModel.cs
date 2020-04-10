using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Application_E2A.Projects
{
    /// <summary>
    /// Class that stores the whole data of MVVM
    /// </summary>
    public class PrintStructureViewModel
    {
        #region Prvate Fields
        private ProjectInfo mInfo;
        #endregion

        #region Public Properties
        public string ProjectNumber { get; set; }
        public string ProjectName { get; set; }
        public string CurrentPageNumber { get; set; }
        public string TotalPageNumber { get; set; }
        public string HeaderName { get; set; } = PageInfo.HeaderTitle;
        public string HeaderDate { get; set; } = PageInfo.HeaderDate;

        public ObservableCollection<PrintHeaderViewModel> Children { get; set; }
        public ObservableCollection<PrintContentViewModel> ColumnHeaders { get; set; } = new ObservableCollection<PrintContentViewModel>(
               PageInfo.ColumnNames.Zip(PageInfo.ColumnWidths, (name, width) => new PrintContentViewModel(name, width)));

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="totalPageNumber"></param>
        /// <param name="headers"></param>
        public PrintStructureViewModel(int pageNumber, int totalPageNumber, List<PrintHeaderViewModel> headers) : base()
        {
            //Assign Data to Whole Data Structure
            this.mInfo = ThisApplication.thisApp.doc.ProjectInformation;
            this.ProjectName = this.mInfo.Name;
            this.ProjectNumber = this.mInfo.Number;
            this.CurrentPageNumber = (pageNumber + 1).ToString();
            this.TotalPageNumber = totalPageNumber.ToString();

            //if data is empty
            if (headers.Count < 1)
            {
                this.Children = new ObservableCollection<PrintHeaderViewModel>();
                return;
            }

            //if data is not empty
            List<PrintHeaderViewModel> filteredHeaders = FilterDataPerPage(headers);
            List<PrintHeaderViewModel> filteredHeadersCopy = new List<PrintHeaderViewModel>(filteredHeaders);

            for (int k = filteredHeadersCopy.Count - 1; k >= 0; k--)
            {
                if (filteredHeadersCopy[k].Children.Count < 1)
                    filteredHeaders.Remove(filteredHeadersCopy[k]);
            }

            this.Children = new ObservableCollection<PrintHeaderViewModel>(filteredHeaders);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Method that distributes headers and headers content across pages
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        private static List<PrintHeaderViewModel> FilterDataPerPage(List<PrintHeaderViewModel> headers)
        {
            List<PrintHeaderViewModel> filteredHeaders = new List<PrintHeaderViewModel>();
            int currentLinesNumber = 0;
            int filteredHeadersCounter = 0;

            for (int i = PageInfo.PageCurrentFirstRow_Header; i < headers.Count; i++)
            {
                currentLinesNumber = currentLinesNumber + PageInfo.HeaderHeight;
                if (currentLinesNumber <= PageInfo.MaxLinesPerPage)
                {
                    filteredHeaders.Add(new PrintHeaderViewModel(headers[i].HeaderName, new List<PrintLineViewModel>()));

                    for (int j = PageInfo.PageCurrentFirstRow_Line; j < headers[i].Children.Count; j++)
                    {
                        currentLinesNumber = currentLinesNumber + Utilities.CalculateNumberOfLinesRequired(headers[i].Children[j]);

                        if (currentLinesNumber <= PageInfo.MaxLinesPerPage)
                        {
                            filteredHeaders[filteredHeadersCounter].Children.Add(headers[i].Children[j]);

                        }
                        else
                        {
                            PageInfo.PageCurrentFirstRow_Header = i;
                            PageInfo.PageCurrentFirstRow_Line = j;

                            return filteredHeaders;
                        }
                    }
                }
                else
                {
                    PageInfo.PageCurrentFirstRow_Header = i;
                    PageInfo.PageCurrentFirstRow_Line = 0;

                    return filteredHeaders;
                }
                filteredHeadersCounter++;
                PageInfo.PageCurrentFirstRow_Line = 0;
            }
            return filteredHeaders;
        }
        #endregion
    }
}