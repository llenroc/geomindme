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
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using GeomindMe.ViewModels;
using System.Device.Location;

namespace GeomindMe.Views
{
	public partial class ToDoItemDetailsViewPage : PhoneApplicationPage
	{
		public ToDoItemDetailsViewPage()
		{
			InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (NavigationContext.QueryString.ContainsKey("id"))
            {
                string idQueryString = NavigationContext.QueryString["id"];
                int id = 0;
                if (!Int32.TryParse(idQueryString, out id))
                {
                    throw new ArgumentException("id is not valid value!");
                }

                ToDoItemDetailsViewModel viewModel = (ToDoItemDetailsViewModel)this.DataContext;
                string currentUri = e.Uri.OriginalString;
				viewModel.OnToDoItemChanged += new GeomindMe.ViewModels.ToDoItemDetailsViewModel.LocationEventHandler(
						(geoLocation)=>
						{
							Dispatcher.BeginInvoke(new Action(() =>
							{
								NavigateMap(new GeoCoordinate(geoLocation.Latitude, geoLocation.Longitude), true);
							}));
						});
                viewModel.Load(id, currentUri);
				
                return;
            }
            else
            {
                throw new ArgumentNullException("id must not be null!");
            }
        }

		private void NavigateMap(System.Device.Location.GeoCoordinate todoLocation, bool closeZoom)
		{
			DetailsMap.Center = todoLocation;
			if (closeZoom)
			{
				DetailsMap.ZoomLevel = 14;
			}
		}

		private void OnAppBarButtonEditClick(object sender, EventArgs e)
		{

			var viewModel = DataContext as ToDoItemDetailsViewModel;
			if (viewModel == null)
			{
				return;
			}

			if (viewModel.ToDoItem == null)
			{
				return;
			}

			viewModel.EditCommand.Execute(null);
		}

		private void OnAppBarButtonCompleteClick(object sender, EventArgs e)
		{
			var viewModel = DataContext as ToDoItemDetailsViewModel;
			if (viewModel == null)
			{
				return;
			}

			if (viewModel.ToDoItem == null)
			{
				return;
			}

			viewModel.CompleteCommand.Execute(null);
		}

		private void OnAppBarButtonDeleteClick(object sender, EventArgs e)
		{
			var viewModel = DataContext as ToDoItemDetailsViewModel;
			if (viewModel != null)
			{
				viewModel.DeleteCommand.Execute(null);
			}
		}

		private void OnAppBarButtonPinToStartClick(object sender, EventArgs e)
		{
			var viewModel = DataContext as ToDoItemDetailsViewModel;
			if (viewModel != null)
			{
				viewModel.PinToStartCommand.Execute(null);
			}
		}

		private void OnAppBarButtonGetDirectionsClick(object sender, EventArgs e)
		{
			var viewModel = DataContext as ToDoItemDetailsViewModel;
			if (viewModel != null)
			{
				viewModel.GetDirectionsCommand.Execute(null);
			}
		}

		private void OnAppBarButtonSendAsEmail(object sender, EventArgs e)
		{
			var viewModel = DataContext as ToDoItemDetailsViewModel;
			if (viewModel != null)
			{
				viewModel.SendAsEmailCommand.Execute(null);
			}
		}

		private void OnAppBarButtonSendAsSms(object sender, EventArgs e)
		{
			var viewModel = DataContext as ToDoItemDetailsViewModel;
			if (viewModel != null)
			{
				viewModel.SendAsSmsCommand.Execute(null);
			}
		}
	}
}