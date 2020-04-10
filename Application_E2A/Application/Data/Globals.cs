using Autodesk.Revit.DB;
using System.Collections.Generic;
using Application_E2A.Projects;

namespace Application_E2A
{
    /// <summary>
    /// Class that stores all global parameters used across solution
    /// </summary>
    public static class Globals
    {
        #region PAGINATION parameters
        public static int PageFirstRow_Tasks;
        public static int PageFirstRow_Revision;
        #endregion

        #region REVISIONS parameters
        public static int NumberOfIncorrectRevisions;
        #endregion
    }
}
