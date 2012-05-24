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
using GeomindMe.Helpers;
using System.Windows.Controls.Primitives;
using JediNinja.Controls.WP;

namespace GeomindMe.Views
{
    public partial class ToDoItemEditViewPage : PhoneApplicationPage
    {
        public ToDoItemEditViewPage()
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

                ToDoItemEditViewModel viewModel = this.DataContext as ToDoItemEditViewModel;
                if (viewModel == null)
                {
                    throw new NullReferenceException("vieModel is Null!");
                }
                viewModel.Load(id);
                this.GeoLocationControlPicker.SetProperties(viewModel.ToDoItem.LocationAddress,
                                                            viewModel.ToDoItem.LocationLatitude,
                                                            viewModel.ToDoItem.LocationLongitude);
                return;
            }
            else
            {
                ToDoItemEditViewModel viewModel = (ToDoItemEditViewModel)this.DataContext;
                if (viewModel == null)
                {
                    throw new NullReferenceException("vieModel is Null!");
                }
                viewModel.CreateNew();
            }
        }

        private void OnAppBarButtonSaveClick(object sender, EventArgs e)
        {
			//Workaround to update bindings
            ApplicationBarHelper.UpdateBindingOnFocussedControl();

            var viewModel = DataContext as ToDoItemEditViewModel;
            if (viewModel != null)
            {
                double latitude = this.GeoLocationControlPicker.Latitude;
                double longitude = this.GeoLocationControlPicker.Longitude;
                string address = this.GeoLocationControlPicker.Address;
                
                viewModel.ToDoItem.LocationLatitude = latitude;
                viewModel.ToDoItem.LocationLongitude = longitude;
                viewModel.ToDoItem.LocationAddress = address;

                viewModel.SaveCommand.Execute(null);
            }
        }

        private void OnAppBarButtonCancelClick(object sender, EventArgs e)
        {
            var viewModel = DataContext as ToDoItemEditViewModel;
            if (viewModel != null)
            {
                viewModel.CancelCommand.Execute(null);
            }
        }

    }
}