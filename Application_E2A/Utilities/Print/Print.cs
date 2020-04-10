using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Application_E2A.Projects;

namespace Application_E2A
{
    /// <summary>
    /// Partial class that serves as a library of methods
    /// </summary>
    public static partial class Utilities
    {
        #region Method PrintA4Vertical
        /// <summary>
        /// method that fires printing dialog. Prints A4 Vertical
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="printJobName"></param>
        public static void PrintA4Vertical(List<PrintHeaderViewModel> headers, string printJobName = "Print")
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                //values provided below are deafualt values for A4 vertical
                DocumentPaginator paginator = new Paginator(new Size(793.700787401575, 1122.51968503937), headers);
                printDialog.PrintDocument(paginator, printJobName);
            }
        }
        #endregion

        #region Method CalculateNumberOfLinesRequired
        /// <summary>
        /// method that calculates the number of substrings that are required to fit given data string into given char length
        /// </summary>
        /// <param name="printLine"></param>
        /// <returns></returns>
        public static int CalculateNumberOfLinesRequired(PrintLineViewModel printLine)
        {
            //return 1 if given data string is empty
            int numberOfLines = 1;
            if ((printLine == null) || (printLine.Children.Count == 0)) return 1;

            //default factor for caluculation
            double factor = 6.6666666;

            //loop through all strings that are in give data Row
            foreach (PrintContentViewModel content in printLine.Children)
            {
                int number = Utilities.SplitIntoSubstrings(content.Content, (int)(content.MaxContentWidth / factor)).Count;
                if (number > numberOfLines)
                    numberOfLines = number;
            }

            return numberOfLines;
        }
        #endregion

        #region Method ForEachRecordAddEmptyLine
        /// <summary>
        /// For each header, adjust number of string lines to given column width
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="columnWidths"></param>
        /// <returns></returns>
        public static List<PrintHeaderViewModel> ForEachRecordAddEmptyLine(List<PrintHeaderViewModel> headers, List<double> columnWidths)
        {
            List<PrintHeaderViewModel> HeadersWithEmptyLine = new List<PrintHeaderViewModel>();
            foreach (PrintHeaderViewModel header in headers)
            {
                //get data from existing headers, crete new list of lines
                PrintHeaderViewModel newHeader = new PrintHeaderViewModel(header.HeaderName, new List<PrintLineViewModel>());
                int counter = 0;

                //loop though all lines in given header
                foreach (PrintLineViewModel line in header.Children)
                {
                    if(counter == header.Children.Count - 1)
                    {
                        //If element is the last one dont add empty line
                        newHeader.Children.Add(line);
                    }
                    else
                    {
                        newHeader.Children.Add(line);
                        //add an emty line
                        newHeader.Children.Add(new PrintLineViewModel(new List<string>() { " " }, columnWidths));
                    }
                    counter++;
                }
                //add newly created header to list of headers
                HeadersWithEmptyLine.Add(newHeader);
            }
            return HeadersWithEmptyLine;
        }
        #endregion
    }
}
