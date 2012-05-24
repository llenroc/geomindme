using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace GeomindMe.Helpers
{
    public class BoolToOnOffConverter : IValueConverter
    {
        private readonly string  ON_VALUE = "On";
        private readonly string  OFF_VALUE = "Off";

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            
            if (value.GetType() != typeof(bool))
            {
                return null;
            }

            bool boolValue = (bool)value;
            if(boolValue)
            {
                return ON_VALUE;
            }
            else
            {
                return OFF_VALUE;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
