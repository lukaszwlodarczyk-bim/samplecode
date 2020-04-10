using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Application_E2A.Projects
{
    public class RevisionsStructureViewModel : BaseViewModel
    {
        #region Public Properties
        public static RevisionsStructureViewModel ThisInstance { get; set; }
        public ObservableCollection<RevisionsRevisionViewModel> Items { get; set; }
        #endregion

        #region Default Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public RevisionsStructureViewModel()
        {
            RevisionsStructureViewModel.ThisInstance = this;

            this.Items = new ObservableCollection<RevisionsRevisionViewModel>( new FilteredElementCollector(ThisApplication.thisApp.doc)
                .OfClass(typeof(Revision)).ToElements().Cast<Revision>()
                .OrderBy(rev => rev.SequenceNumber).Reverse()
                .Select(rev => new RevisionsRevisionViewModel(rev)) 
                );
        }
        #endregion
    }
}
