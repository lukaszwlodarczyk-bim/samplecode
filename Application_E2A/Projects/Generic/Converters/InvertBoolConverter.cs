using System;
using System.Globalization;
using System.Windows.Data;

namespace Application_E2A.Projects
{
    public class InvertBoolConverter : IValueConverter
    {
        /// <summary>
        /// This instance of ValueConverter to be referenced to xaml code
        /// </summary>
        public static InvertBoolConverter Instance = new InvertBoolConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
                return false;
            else
                return true; ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
