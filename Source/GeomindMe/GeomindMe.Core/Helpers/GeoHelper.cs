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

namespace JediNinja.Controls.WP.Helpers
{
    public class GeoHelper
    {
        public static double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double latitude1 = lat1;
            double longitude1 = lon1;

            double latitude2 = lat2;
            double longitude2 = lon2;

            double earthRadius = 6372;

            double pi = Math.PI;
            double e = (pi * latitude1 / 180);
            double f = (pi * longitude1 / 180);
            double g = (pi * latitude2 / 180);
            double h = (pi * longitude2 / 180);
            double i = (Math.Cos(e) * Math.Cos(g) * Math.Cos(f) * Math.Cos(h) + Math.Cos(e) * Math.Sin(f) * Math.Cos(g) * Math.Sin(h) + Math.Sin(e) * Math.Sin(g));
            double j = (Math.Acos(i));
            double k = (earthRadius * j);

            return k;

        }
    }
}
