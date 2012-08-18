using System.Windows;
using Microsoft.Phone.Scheduler;
using System.Device.Location;
using System;
using System.Linq;
using GeomindMe.Models;
using Microsoft.Phone.Shell;
using System.Diagnostics;
using GeomindMe.Helpers;

namespace GeomindMe.ReminderScheduler
{
	public class ScheduledAgent : ScheduledTaskAgent
	{
		private static volatile bool _classInitialized;

		/// <remarks>
		/// ScheduledAgent constructor, initializes the UnhandledException handler
		/// </remarks>
		public ScheduledAgent()
		{
			if (!_classInitialized)
			{
				_classInitialized = true;
				// Subscribe to the managed exception handler
				Deployment.Current.Dispatcher.BeginInvoke(delegate
				{
					Application.Current.UnhandledException += ScheduledAgent_UnhandledException;
				});
			}
		}

		/// Code to execute on Unhandled Exceptions
		private void ScheduledAgent_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				// An unhandled exception has occurred; break into the debugger
				System.Diagnostics.Debugger.Break();
			}
		}

		/// <summary>
		/// Agent that runs a scheduled task
		/// </summary>
		/// <param name="task">
		/// The invoked task
		/// </param>
		/// <remarks>
		/// This method is called when a periodic or resource intensive task is invoked
		/// </remarks>
		protected override void OnInvoke(ScheduledTask task)
		{
			GetCurrentGpsPosition();                        
		}

		private void GetCurrentGpsPosition()
		{
			bool areLocationServicesEnabled = SettingsHelper.IsLocationServicesEnabled();
			if (areLocationServicesEnabled)
			{
				GeoCoordinateWatcher geoWatcher = new GeoCoordinateWatcher();
				geoWatcher.MovementThreshold = 0;
				geoWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(geoWatcher_PositionChanged);
				if (!geoWatcher.TryStart(true, TimeSpan.FromSeconds(13)))
				{
					Debug.WriteLine("could not start gps on background agent");
				}
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

			var geoWatcher = sender as GeoCoordinateWatcher;
			if (geoWatcher == null)
			{
				return;
			}
			geoWatcher.Stop();

			var currentLocation = position.Location;

			ProcessCurrentLocation(currentLocation);
		}

		private void ProcessCurrentLocation(GeoCoordinate currentLocation)
		{
			ToDoItemRepository toDoItemRepository = new ToDoItemRepository();
			var nearToDoItems = toDoItemRepository.GetToDoItemsNearGeoLocation(currentLocation.Latitude,
														   currentLocation.Longitude).ToList();
			if ((nearToDoItems != null) && (nearToDoItems.Count > 0))
			{
				NotifyUserForNearToDoItems(nearToDoItems, currentLocation);
			}
			else
			{
				NotifyComplete();
			}
		}

		private void NotifyUserForNearToDoItems(System.Collections.Generic.List<ToDoItem> nearToDoItems, GeoCoordinate currentLocation)
		{
			foreach (var toDoItem in nearToDoItems)
			{
				if (toDoItem == null)
				{
					continue;
				}
				string uriAddress = string.Format("/Views/ToDoItemDetailsViewPage.xaml?id={0}", toDoItem.ToDoItemId);
				string content = string.Format("{0} : on {1}", toDoItem.Text, toDoItem.LocationAddress);
				ShellToast popupMessage = new ShellToast()
				{
					Title = "Geo TO DO in range",
					Content = content,
					NavigationUri = new Uri(uriAddress, UriKind.Relative)
				};
				popupMessage.Show();
			}
			NotifyComplete();
		}

	}
}