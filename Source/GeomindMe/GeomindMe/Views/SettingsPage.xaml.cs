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
using GeomindMe.ViewModels;

namespace GeomindMe.Views
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void OnAppBarButtonSaveClick(object sender, EventArgs e)
        {
            var mainViewModel = this.DataContext as SettingsViewModel;
            if (mainViewModel != null)
            {
                mainViewModel.SaveCommand.Execute(null);
            }
        }

        private void OnAppBarButtonCancelClick(object sender, EventArgs e)
        {
            var mainViewModel = this.DataContext as SettingsViewModel;
            if (mainViewModel != null)
            {
                mainViewModel.CancelCommand.Execute(null);
            }
        }

        private void OnButtonTestClick(object sender, RoutedEventArgs e)
        {
            var mainViewModel = this.DataContext as SettingsViewModel;
            if (mainViewModel != null)
            {
                mainViewModel.TestCommand.Execute(null);
            }
        }

        private void OnButtonSwitchClick(object sender, EventArgs e)
        {
            var mainViewModel = this.DataContext as SettingsViewModel;
            if (mainViewModel != null)
            {
                mainViewModel.SwitchScheduleTaskActivationCommand.Execute(null);
            }
        }
    }
}