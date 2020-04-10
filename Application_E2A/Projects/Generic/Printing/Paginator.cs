using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Application_E2A.Projects
{
    public class Paginator : DocumentPaginator
    {
        #region Private Fields
        private List<PrintHeaderViewModel> mHeaders;
        private Size mPageSize;
        #endregion

        #region PublicProperties
        public override bool IsPageCountValid { get { return true; } }
        public override int PageCount { get { return this.GetPageCount(this.mHeaders); } }

        public override Size PageSize
        {
            get { return this.mPageSize; }
            set { this.mPageSize = value; }
        }
        public override IDocumentPaginatorSource Source { get { return null; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="headers"></param>
        public Paginator(Size size, List<PrintHeaderViewModel> headers)
        {
            this.mPageSize = size;
            this.mHeaders = headers;
        }
        #endregion

        /// <summary>
        /// Returns DocumentPage instance from page number
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public override DocumentPage GetPage(int pageNumber)
        {
            //Here Data should be submitted that takes page number as parameter
            var page = new PageToPrint(pageNumber, this.PageCount, this.mHeaders)
            {
                Width = PageSize.Width,
                Height = PageSize.Height,
            };
            page.Measure(PageSize);
            page.Arrange(new Rect(new Point(0, 0), PageSize));
            page.UpdateLayout();

            return new DocumentPage(page);
        }

        /// <summary>
        /// Method that has to implemented from DocumentPaginator interface
        /// Returns total number of pages
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        private int GetPageCount(List<PrintHeaderViewModel> headers)
        {
            int pageCount = 1;
            int currentLinesNumber = 0;

            for (int i = 0; i < headers.Count; i++)
            {
                currentLinesNumber = currentLinesNumber + PageInfo.HeaderHeight;
                for (int j = 0; j < headers[i].Children.Count; j++)
                {

                    int linesPerRecord = Utilities.CalculateNumberOfLinesRequired(headers[i].Children[j]);
                    currentLinesNumber = currentLinesNumber + linesPerRecord;

                    if ((currentLinesNumber) > PageInfo.MaxLinesPerPage)
                    {
                        pageCount++;
                        currentLinesNumber = PageInfo.HeaderHeight + Utilities.CalculateNumberOfLinesRequired(headers[i].Children[j]);
                    }
                }

            }
            return pageCount;
        }
    }
}
