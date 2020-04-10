using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Application_E2A
{
    /// <summary>
    /// Partial class that serves as a library of methods
    /// </summary>
    public static partial class Utilities
    {
        #region Method MakeViewActive Overloads
        /// <summary>
        /// Makes given view an active view in Revit UI
        /// </summary>
        /// <param name="uidoc"></param>
        /// <param name="view"></param>
        public static void MakeViewActive(UIDocument uidoc, View view)
        {
            //SHOULD BE PLACED OUTSIDE TRANSACTION !
            if (uidoc == null) return;

            //check if the given view is not already active
            if (uidoc.Document.ActiveView.Id.IntegerValue != view.Id.IntegerValue)
                uidoc.ActiveView = view;
        }

        /// <summary>
        /// Makes given view an active view in Revit UI
        /// </summary>
        /// <param name="uidoc"></param>
        /// <param name="viewId"></param>
        public static void MakeViewActive(UIDocument uidoc, ElementId viewId)
        {
            //SHOULD BE PLACED OUTSIDE TRANSACTION !
            if (uidoc == null) return;

            //check if the given view is not already active
            if (uidoc.Document.ActiveView.Id.IntegerValue != viewId.IntegerValue)
                uidoc.ActiveView = uidoc.Document.GetElement(viewId) as View;
        }
        #endregion

        #region Method GetActiveUIView
        /// <summary>
        /// Return UI data about current view
        /// </summary>
        /// <param name="uidoc"></param>
        /// <returns></returns>
        public static UIView GetActiveUIView(UIDocument uidoc)
        {
            UIView uiview = null;
            if (uidoc == null) return uiview;

            View activeView = uidoc.Document.ActiveView;
            //Get all open views as uiview
            IList<UIView> uiviews = uidoc.GetOpenUIViews();
            if (uiviews.Count == 0) return uiview;

            //Loop through all open uiviews to get uiview of active view
            foreach (UIView uv in uiviews)
            {
                if (uv.ViewId.Equals(activeView.Id))
                {
                    uiview = uv;
                    break;
                }
            }
            return uiview;
        }
        #endregion

        #region Method GetElementRectangle
        /// <summary>
        /// Returns rectange as array of two points XYZ, with centroid as elements placement point
        /// </summary>
        /// <param name="el"></param>
        /// <param name="x_extend"></param>
        /// <param name="y_extend"></param>
        /// <returns></returns>
        public static XYZ[] GetElementRectangle(Element el, double x_extend, double y_extend)
        {
            //Initialize empty array to collect two points XYZ
            XYZ[] corners = new XYZ[2];
            if (el == null) return corners;

            //Get Element placement point
            XYZ center = null;
            LocationPoint pt = el.Location as LocationPoint;
            if (pt != null)
            {
                center = pt.Point;
            }
            else
            {
                LocationCurve crv = el.Location as LocationCurve;
                if (crv != null)
                    center = crv.Curve.GetEndPoint(0);
            }

            //caluclate two points
            if (center != null)
            {
                //if center is not null, means that element is a model element (mot annotation element)
                corners[0] = new XYZ(center.X - x_extend / 2, center.Y - y_extend / 2, 0);
                corners[1] = new XYZ(center.X + x_extend / 2, center.Y + y_extend / 2, 0);
            }
            else
            {
                //if center(placement point) could not be collected, means that element is an annotation element
                //center will be calculated as midPoint between element bbox corners
                BoundingBoxXYZ bbox = el.get_BoundingBox(el.Document.ActiveView);
                if (bbox != null)
                    center = Utilities.GetMidPointBetweenPoints(bbox.Min, bbox.Max);

                if (center != null)
                {
                    corners[0] = new XYZ(center.X - x_extend / 2, center.Y - y_extend / 2, 0);
                    corners[1] = new XYZ(center.X + x_extend / 2, center.Y + y_extend / 2, 0);
                }
            }

            return corners;
        }
        #endregion

        #region Method ZoomToRectangle Overloads
        /// <summary>
        /// Use RevitAPI method ZoomAndCenterRectangle to zoom to give element
        /// </summary>
        /// <param name="uiview"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public static void ZoomToRectangle(UIView uiview, XYZ p1, XYZ p2)
        {
            if((uiview != null) && (p1!= null) && (p2 != null) )
                uiview.ZoomAndCenterRectangle(p1, p2);
        }

        /// <summary>
        /// Use RevitAPI method ZoomAndCenterRectangle to zoom to give element
        /// </summary>
        /// <param name="uiview"></param>
        /// <param name="pts"></param>
        public static void ZoomToRectangle(UIView uiview, XYZ[] pts)
        {
            if ((uiview != null) && (pts.Length==2) )
            {
                if( (pts[0]!=null) && (pts[1] != null) )
                    uiview.ZoomAndCenterRectangle(pts[0], pts[1]);
            }
        }
        #endregion

        #region Method HideAllElementsInViewButElement
        /// <summary>
        /// Hide All elements in the view apart from input Element. 
        /// Warning: method is not supported forElements within RevitLinkElement
        /// </summary>
        /// <param name="view"></param>
        /// <param name="elToRemainVisible"></param>
        public static void HideAllElementsInViewButElement(View view, Element elToRemainVisible)
        {
            //Collect all elements visible in the view
            if ((view == null) || (elToRemainVisible == null))
                return;
            List<Element> AllElementsInView = new FilteredElementCollector(view.Document, view.Id)
                .WhereElementIsNotElementType().ToElements().ToList();
            if (AllElementsInView.Count == 0) return;

            //hiding elements in the view, required a transaction
            Transaction t = new Transaction(view.Document, "HieElementsInView");
            t.Start();

            foreach (Element el in AllElementsInView)
            {
                //skip element that has to remain visible
                //else - for every ElementId to hide create new List
                if (el.Id.IntegerValue == elToRemainVisible.Id.IntegerValue) continue;
                else view.HideElements(new List<ElementId>() { el.Id });
            }
            t.Commit();
        }
        #endregion
    }
}
