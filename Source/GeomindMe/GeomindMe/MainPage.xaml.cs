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
using GeomindMe.Models;
using Microsoft.Phone.Shell;

namespace GeomindMe
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.RefreshToDoItems();
            
        }

        private void OnAppBarButtonAddToDoItemClick(object sender, EventArgs e)
        {
            var toDoItemsListViewModel = ToDoItemsListPivotItem.DataContext as ToDoItemsListViewModel;
            if (toDoItemsListViewModel != null)
            {
                toDoItemsListViewModel.AddNewCommand.Execute(null);
            }
        }

        private void OnAppBarButtonRefreshClick(object sender, EventArgs e)
        {
            RefreshToDoItems();
        }

        private void RefreshToDoItems()
        {
            var toDoItemsListViewModel = ToDoItemsListPivotItem.DataContext as ToDoItemsListViewModel;
            if (toDoItemsListViewModel != null)
            {
                toDoItemsListViewModel.LoadIncompletedCommand.Execute(null);
            }
        }

        private void Pivot_LoadedPivotItem(object sender, PivotItemEventArgs e)
        {

        }

        private void OnAppBarButtonLocateMeClick(object sender, EventArgs e)
        {

        }

        private void OnAppBarAboutClick(object sender, EventArgs e)
        {
            var mainViewModel = this.DataContext as MainViewModel;
            if (mainViewModel != null)
            {
                mainViewModel.AboutCommand.Execute(null);
            }
        }

        private void OnAppBarSettingsClick(object sender, EventArgs e)
        {
            var mainViewModel = this.DataContext as MainViewModel;
            if (mainViewModel != null)
            {
                mainViewModel.SettingsCommand.Execute(null);
            }
        }

        private void ShowCompletedMenuItem_Click(object sender, EventArgs e)
        {
            var toDoItemsListViewModel = ToDoItemsListPivotItem.DataContext as ToDoItemsListViewModel;
            if (toDoItemsListViewModel != null)
            {
                toDoItemsListViewModel.LoadCompletedCommand.Execute(null);
            }
        }

        private void ShowIcompletedMenuItem_Click(object sender, EventArgs e)
        {
            var toDoItemsListViewModel = ToDoItemsListPivotItem.DataContext as ToDoItemsListViewModel;
            if (toDoItemsListViewModel != null)
            {
                toDoItemsListViewModel.LoadIncompletedCommand.Execute(null);
            }
        }

        


    }
}