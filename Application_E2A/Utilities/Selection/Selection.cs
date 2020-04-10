using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application_E2A
{
    /// <summary>
    /// Partial class that serves as a library of methods
    /// </summary>
    public static partial class Utilities
    {
        #region Method SelectElement
        /// <summary>
        /// Make the given element a current selection in UI
        /// </summary>
        /// <param name="uidoc"></param>
        /// <param name="el"></param>
        public static void SelectElement(UIDocument uidoc, Element el)
        {
            if (el == null)
                return;
            uidoc.Selection.SetElementIds(new List<ElementId>() {el.Id});
        }
        #endregion
    }
}
