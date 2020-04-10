using System;
using System.Globalization;
using System.Windows.Data;

namespace Application_E2A.Projects
{
    public class ErrorMessageToBool : IValueConverter
    {
        /// <summary>
        /// This instance of ValueConverter to be referenced to xaml code
        /// </summary>
        public static ErrorMessageToBool Instance = new ErrorMessageToBool();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == string.Empty)
                return false;
            else
                return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
