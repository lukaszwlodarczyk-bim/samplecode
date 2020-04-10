using System;

namespace Application_E2A.Projects
{
    /// <summary>
    /// Class that stores content of each Line (PrintLineViewModel)
    /// </summary>
    public class PrintContentViewModel : IComparable
    {
        #region Public Properties
        public string Content { get; set; }
        public double MaxContentWidth { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxContentWidth"></param>
        public PrintContentViewModel(string text, double maxContentWidth)
        {
            this.Content = text;
            this.MaxContentWidth = maxContentWidth;
        }
        #endregion

        /// <summary>
        /// Method that has to be implemented from IComparable interface
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            PrintContentViewModel value = obj as PrintContentViewModel;
            if (value != null)
                return this.Content.CompareTo(value.Content);
            else
                throw new ArgumentException("Object is not a PrintContentViewModel");
        }
    }
}
