using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_E2A
{
    public class ProjektInfo
    {
        public static ProjektInfo thisInstance;
        public ProjectInfo Info;
        public static List<string> Priviledged = new List<string>() { "XXX", "LWL", "BGO", "AST" };

        public ProjektInfo()
        {
            thisInstance = this;
            this.Info = ThisApplication.thisApp.doc.ProjectInformation;
        }
    }
}
