using Autodesk.Revit.UI;
using System;
using Autodesk.Revit.DB;
using System.Windows;

namespace Application_E2A
{
    /// <summary>
    /// Event Handler to execute RemoveInstance Action
    /// </summary>
    public class EventHandler_Generic_RemoveInstance : IExternalEventHandler
    {
        #region Private Fields
        private Element mElement;
        #endregion

        #region Public Fields
        public static EventHandler_Generic_RemoveInstance thisCmd;
        #endregion

        #region Default Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EventHandler_Generic_RemoveInstance()
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
            using(Transaction t = new Transaction(this.mElement.Document, "RemoveInstance"))
            {
                t.Start();
                if (this.mElement.Pinned)
                    this.mElement.Pinned = false;

                this.mElement.Document.Delete(this.mElement.Id);
                t.Commit();
            }
        }

        /// <summary>
        /// Method that has to be implemented from interface
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.GetType().Name;
        }

        /// <summary>
        /// Assign Element to Remove
        /// </summary>
        /// <param name="el"></param>
        public void AssignDocumentAndElement(Element el)
        {
            this.mElement = el;
        }
    }
}
