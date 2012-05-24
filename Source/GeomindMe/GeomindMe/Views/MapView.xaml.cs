using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Device.Location;
using Microsoft.Phone.Controls.Maps;
using System.Diagnostics;

namespace GeomindMe.Views
{
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            GeoMap.ZoomLevel++;
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            GeoMap.ZoomLevel--;
        }

        private void MeButton_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentGpsPosition();
        }

        private void GetCurrentGpsPosition()
        {
            GeoCoordinateWatcher geoWatcher = new GeoCoordinateWatcher();
            geoWatcher.MovementThreshold = 50;
            geoWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(geoWatcher_PositionChanged);
            if (!geoWatcher.TryStart(false, TimeSpan.FromSeconds(30)))
            {
                Debug.WriteLine("Unsuccessful gps start");
            }
        }

        void geoWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            var position = e.Position;
            if (position == null)
            {
                return;
            }
            if (position.Location == GeoCoordinate.Unknown)
            {
                return;
            }

            var currentLocation = position.Location;
            var pushPin = locationPushPin;
            pushPin.Location = currentLocation;
            NavigateToPositionOnMap(currentLocation);

            var geoWatcher = sender as GeoCoordinateWatcher;
            if (geoWatcher == null)
            {
                return;
            }
            geoWatcher.Stop();

            
        }

        public void NavigateToPositionOnMap(GeoCoordinate geoCoordinate)
        {
            GeoMap.Center = geoCoordinate;
            GeoMap.ZoomLevel = 8;
        }

        private void MapItemsControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
