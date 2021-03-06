﻿using Autodesk.Revit.UI;
using System;
using Autodesk.Revit.DB;
using System.Windows;

namespace Application_E2A
{
    /// <summary>
    /// Event Handler to execute ZoomToElement Action
    /// </summary>
    public class EventHandler_Generic_ZoomViewToElement : IExternalEventHandler
    {
        #region Private Fields
        private Element mElement;
        #endregion

        #region Public Fields
        public static EventHandler_Generic_ZoomViewToElement thisCmd;
        #endregion

        #region Default Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EventHandler_Generic_ZoomViewToElement()
        {
            thisCmd = this;
        }
        #endregion

        /// <summary>
        /// Method that is performed when Handler is executed
        /// </summary>
        /// <param name="app"></param>
        public void Execute(UIApplication app)
        {
            UIView uiview = Utilities.GetActiveUIView(ThisApplication.thisApp.uidoc);
            XYZ[] corners = Utilities.GetElementRectangle(this.mElement, 10, 10);
            Utilities.ZoomToRectangle(uiview, corners);
        }

        /// <summary>
        /// Method that has to be implemented from interface
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.GetType().Name;
        }
    }
}
