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
	public class RoundDoubleConverter : IValueConverter
	{
		private readonly int Digits = 3;
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is double)
			{
				double val = (double)value;
				double roundedValue = Math.Round(val, 3);
				return roundedValue;
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			double possibleVal = 0;
			if (Double.TryParse(value.ToString(), out possibleVal))
			{
				var val = (double)possibleVal;
				return Math.Round(val, 3);
			}
			return value;
		}
	}
}
