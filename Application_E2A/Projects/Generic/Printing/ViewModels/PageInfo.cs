using System;
using System.Collections.Generic;

namespace Application_E2A.Projects
{
    public static class PageInfo
    {
        #region Private Fields
        //Set values for A4 page
        private static int maxLinesVertical = 55; 
        private static int maxLinesHorizontal = 33;
        #endregion

        #region Public Fields
        public static string HeaderTitle;
        public static string HeaderDate;
        public static List<string> ColumnNames = new List<string>();
        public static List<double> ColumnWidths = new List<double>();

        public static int PageCurrentFirstRow_Header;
        public static int PageCurrentFirstRow_Line;
        public static int MaxLinesPerPage = 1;
        public static int HeaderHeight = 2;

        #region Revisions
        public static List<string> ColumnNames_Revisions = new List<string>();
        public static List<double> ColumnWidths_Revisions = new List<double>() { 25, 500 };
        #endregion

        #endregion

        /// <summary>
        /// Data that is assigned PageInfo instance in Revisions Tool
        /// </summary>
        /// <param name="headerName"></param>
        public static void AssignPageInfo_Revisions(string headerName)
        {
            //Reset indices
            PageInfo.PageCurrentFirstRow_Header = 0;
            PageInfo.PageCurrentFirstRow_Line = 0;

            //Fixed depending on PrintablePageSize
            PageInfo.MaxLinesPerPage = maxLinesVertical;

            PageInfo.HeaderTitle = headerName;
            PageInfo.HeaderDate = DateTime.Now.ToString("dd.MM.yyyy");
            PageInfo.ColumnNames = PageInfo.ColumnNames_Revisions;
            PageInfo.ColumnWidths = PageInfo.ColumnWidths_Revisions;
        }
    }
}
