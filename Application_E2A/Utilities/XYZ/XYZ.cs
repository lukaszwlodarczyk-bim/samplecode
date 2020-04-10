using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Application_E2A
{
    /// <summary>
    /// Partial class that serves as a library of methods
    /// </summary>
    public static partial class Utilities
    {
        #region Method GetMidPointBewteenPoints
        /// <summary>
        /// Returns mid point between 2 points
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        public static XYZ GetMidPointBetweenPoints(XYZ pt1, XYZ pt2)
        {
            XYZ midPt = null;
            if( (pt1!=null) && (pt2 != null))
            {
                midPt = new XYZ((pt1.X + pt2.X) / 2, (pt1.Y + pt2.Y) / 2, (pt1.Z + pt2.Z) / 2);
            }
            return midPt;
        }
        #endregion
    }
}
