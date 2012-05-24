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
using GeomindMe.ViewModels;

namespace GeomindMe.Views
{
    public partial class ToDoItemsListView : UserControl
    {
        public ToDoItemsListView()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var senderFrameworkElement = sender as FrameworkElement;
            if (senderFrameworkElement == null)
            {
                return;
            }

            var viewModel = senderFrameworkElement.DataContext as ToDoItemsListViewModel;
            if (viewModel == null)
            {
                return;
            }

            if (viewModel.SelectedToDoItem == null)
            {
                return;
            }

            viewModel.ViewDetailsCommand.Execute(null);
        }
    }
}
