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
    public class BoolToTextConverter : IValueConverter
    {
		public string TrueValue { get; set; }
		public string FalseValue { get; set; }
		public string DefaultValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return DefaultValue;
            }
            
            if (value.GetType() != typeof(bool))
            {
                return DefaultValue;
            }

            bool boolValue = (bool)value;
            if(boolValue)
            {
                return TrueValue;
            }
            else
            {
                return FalseValue;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
