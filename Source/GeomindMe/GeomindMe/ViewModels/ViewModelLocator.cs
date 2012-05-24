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
using System.ComponentModel;
using GeomindMe.Models;
using GeomindMe.Services;
using Microsoft.Phone.Shell;

namespace GeomindMe.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            if (DesignerProperties.IsInDesignTool)
            {

            }
            else
            {

            }
        }

        #region MainViewModel
        private static MainViewModel _mainViewModel;
        public static MainViewModel MainViewModelStatic
        {
            get
            {
                if (_mainViewModel == null)
                {
                    CreateMainViewModel();
                }

                return _mainViewModel;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel MainViewModel
        {
            get
            {
                return MainViewModelStatic;
            }
        }

        public static void ClearMainViewModel()
        {
            _mainViewModel.Cleanup();
            _mainViewModel = null;
        }

        public static void CreateMainViewModel()
        {
            if (_mainViewModel == null)
            {
                _mainViewModel = new MainViewModel();
            }
        }

        #endregion

        #region ToDoItemsListViewModel
        private static ToDoItemsListViewModel _toDoItemsListViewModel;
        public static ToDoItemsListViewModel ToDoItemsListViewModelStatic
        {
            get
            {
                if (_toDoItemsListViewModel == null)
                {
                    CreateToDoItemsListViewModel();
                }

                return _toDoItemsListViewModel;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ToDoItemsListViewModel ToDoItemsListViewModel
        {
            get
            {
                return ToDoItemsListViewModelStatic;
            }
        }

        public static void ClearToDoItemsListViewModel()
        {
            _toDoItemsListViewModel.Cleanup();
            _toDoItemsListViewModel = null;
        }

        public static void CreateToDoItemsListViewModel()
        {
            if (_toDoItemsListViewModel == null)
            {
                if (DesignerProperties.IsInDesignTool)
                {
                    _toDoItemsListViewModel = new ToDoItemsListViewModel(new DesignToDoItemRepository());
                }
                else
                {
                    _toDoItemsListViewModel = new ToDoItemsListViewModel(new ToDoItemRepository());
                }
            }
        }


        #endregion

        #region ToDoItemEditViewModel
        private static ToDoItemEditViewModel _toDoItemEditViewModel;
        public static ToDoItemEditViewModel ToDoItemEditViewModelStatic
        {
            get
            {
                if (_toDoItemEditViewModel == null)
                {
                    CreateToDoEditViewModel();
                }

                return _toDoItemEditViewModel;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ToDoItemEditViewModel ToDoItemEditViewModel
        {
            get
            {
                return ToDoItemEditViewModelStatic;
            }
        }

        public static void ClearToDoEditViewModel()
        {
            _toDoItemEditViewModel.Cleanup();
            _toDoItemEditViewModel = null;
        }

        public static void CreateToDoEditViewModel()
        {
            if (_toDoItemEditViewModel == null)
            {
                if (DesignerProperties.IsInDesignTool)
                {
                    _toDoItemEditViewModel = new ToDoItemEditViewModel(new DesignToDoItemRepository(), 4);
                }
                else
                {
                    _toDoItemEditViewModel = new ToDoItemEditViewModel(new ToDoItemRepository());
                }

            }
        }
        #endregion

        #region ToDoItemDetailsViewModel
        private static readonly string ToDoItemDetailsViewModelPropertyName = "ToDoItemDetailsViewModel";
        private static ToDoItemDetailsViewModel _toDoItemDetailsViewModel;
        public static ToDoItemDetailsViewModel ToDoItemDetailsViewModelStatic
        {
            get
            {
                if (_toDoItemDetailsViewModel == null)
                {
                    CreateToDoItemDetailsViewModel();
                }

                return _toDoItemDetailsViewModel;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ToDoItemDetailsViewModel ToDoItemDetailsViewModel
        {
            get
            {
                return ToDoItemDetailsViewModelStatic;
            }
        }

        public static void ClearToDoItemDetailsViewModel()
        {
            _toDoItemDetailsViewModel.Cleanup();
            _toDoItemDetailsViewModel = null;
        }

        public static void CreateToDoItemDetailsViewModel()
        {
            if (_toDoItemDetailsViewModel == null)
            {
                if (DesignerProperties.IsInDesignTool)
                {
                    _toDoItemDetailsViewModel = new ToDoItemDetailsViewModel(new DesignToDoItemRepository(), 5);
                }
                else
                {
                    _toDoItemDetailsViewModel = new ToDoItemDetailsViewModel(new ToDoItemRepository());
                }
            }
        }

        public void SaveToDoItemDetailsViewModelToApplicationState()
        {
            PhoneApplicationService.Current.State[ToDoItemDetailsViewModelPropertyName] = ToDoItemDetailsViewModel;
        }

        public void LoadToDoItemDetailsViewModelFromApplicationState()
        {
            if (!PhoneApplicationService.Current.State.ContainsKey(ToDoItemDetailsViewModelPropertyName))
            {
                return;
            }

            var viewModel = PhoneApplicationService.Current.State[ToDoItemDetailsViewModelPropertyName] as ToDoItemDetailsViewModel;
            if (viewModel == null)
            {
                return;
            }

            ViewModelLocator._toDoItemDetailsViewModel = viewModel;
        }
        #endregion

        #region SettingsViewModel
        private static SettingsViewModel _settingsViewModel;

        public static SettingsViewModel SettingsViewModelStatic
        {
            get
            {
                if (_settingsViewModel == null)
                {
                    CreateSettingsViewModel();
                }

                return _settingsViewModel;
            }
        }

         [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return SettingsViewModelStatic;
            }
        }

        public static void ClearSettingsViewModel()
        {
            _settingsViewModel.Cleanup();
            _settingsViewModel = null;
        }

        public static void CreateSettingsViewModel()
        {
            if (_settingsViewModel == null)
            {
                _settingsViewModel = new SettingsViewModel();
            }
        }


        #endregion

        public static void Cleanup()
        {
            ClearMainViewModel();
            ClearToDoItemsListViewModel();
            ClearToDoEditViewModel();
            ClearToDoItemDetailsViewModel();
            ClearSettingsViewModel();
        }

        //Saves data to App state on Deactivate
        public void SaveDataToApplicationState()
        {
            //SaveToDoItemDetailsViewModelToApplicationState();
            //SaveCurrentUriToApplicationState();

            //No data that should be saved 
            //only current navigation source is saved as values are passed on navigation
        }

        //Loads data from App state on Activate
        public void LoadDataFromApplicationState()
        {
            //LoadToDoItemDetailsViewModelFromApplicationState();
            //LoadCurrentUriFromApplicationStateAndNavigateTo();
        }
    }
}
