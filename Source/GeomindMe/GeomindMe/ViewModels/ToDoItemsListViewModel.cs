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
using GeomindMe.Models;
using System.Collections.ObjectModel;
using GeomindMe.Helpers;
using GeomindMe.Services;

namespace GeomindMe.ViewModels
{
    public class ToDoItemsListViewModel : ViewModelBase
    {
        //private static readonly string connection = "isostorage:/GeomindMe.sdf";
        //ToDoItemRepository _repository = new ToDoItemRepository(connection);

        IToDoItemRepository _repository;

        public ToDoItemsListViewModel(IToDoItemRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository must not be null");
            }
            this._repository = repository;
            this.LoadIncomplete();
        }

        private void LoadIncomplete()
        {
            var toDoItems = _repository.GetIncompletedToDoItems();
            ToDoItems = new ObservableCollection<ToDoItem>(toDoItems);
        }

        private ObservableCollection<ToDoItem> _toDoItems;
        public ObservableCollection<ToDoItem> ToDoItems
        {
            get
            {
                return _toDoItems;
            }

            set
            {
                if (_toDoItems == value)
                {
                    return;
                }
                _toDoItems = value;
                RaisePropertyChanged("ToDoItems");
            }
        }

        private ToDoItem _selectedToDoItem;
        public ToDoItem SelectedToDoItem
        {
            get
            {
                return _selectedToDoItem;
            }

            set
            {
                if (_selectedToDoItem == value)
                {
                    return;
                }
                _selectedToDoItem = value;
                RaisePropertyChanged("SelectedToDoItem");
                CanEditToDoItem = (_selectedToDoItem != null);
            }
        }

        #region AddNewCommand
        private RelayCommand _addNewCommand;
        public RelayCommand AddNewCommand
        {
            get
            {
                if (_addNewCommand == null)
                {
                    _addNewCommand =
                        new RelayCommand(
                            () =>
                            {
                                AddNewToDoItemExecute();
                            },
                            () => CanAddNewToDoItem
                        );
                }
                return _addNewCommand;
            }
            set
            {
                _addNewCommand = value;
            }
        }

        public void AddNewToDoItemExecute()
        {
            string uriAddress = "/Views/ToDoItemEditViewPage.xaml";
            NavigationController.Instance.Navigate(new Uri(uriAddress, UriKind.Relative));
        }

        private bool _canAddNewToDoItem = true;
        public bool CanAddNewToDoItem
        {
            get
            {
                return _canAddNewToDoItem;
            }

            set
            {
                if (_canAddNewToDoItem == value)
                {
                    return;
                }

                _canAddNewToDoItem = value;

                RaisePropertyChanged("CanAddNewToDoItem");
                AddNewCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region EditCommand
        private RelayCommand _editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand =
                        new RelayCommand(
                            () =>
                            {
                                EditToDoItemExecute();
                            },
                            () => CanEditToDoItem
                        );
                }
                return _editCommand;
            }
            set
            {
                _editCommand = value;
            }
        }

        public void EditToDoItemExecute()
        {
            if (SelectedToDoItem == null)
            {
                return;
            }
            string uriAddress = string.Format("/Views/ToDoItemEditViewPage.xaml?id={0}", SelectedToDoItem.ToDoItemId);
            NavigationController.Instance.Navigate(new Uri(uriAddress, UriKind.Relative));
        }

        private bool _canEditToDoItem = true;
        public bool CanEditToDoItem
        {
            get
            {
                return _canEditToDoItem;
            }

            set
            {
                if (_canEditToDoItem == value)
                {
                    return;
                }
                _canEditToDoItem = value;

                RaisePropertyChanged("CanEditToDoItem");

                EditCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region ViewDetailsCommand
        private RelayCommand _viewDetailsCommand;
        public RelayCommand ViewDetailsCommand
        {
            get
            {
                if (_viewDetailsCommand == null)
                {
                    _viewDetailsCommand =
                        new RelayCommand(
                            () =>
                            {
                                ViewExecute();
                            },
                            () => CanView
                        );
                }
                return _viewDetailsCommand;
            }
            set
            {
                _viewDetailsCommand = value;
            }
        }

        public void ViewExecute()
        {
            if (SelectedToDoItem == null)
            {
                return;
            }

            string uriAddress = string.Format("/Views/ToDoItemDetailsViewPage.xaml?id={0}", SelectedToDoItem.ToDoItemId);
            NavigationController.Instance.Navigate(new Uri(uriAddress, UriKind.Relative));
        }

        private bool _canView = true;
        public bool CanView
        {
            get
            {
                return _canView;
            }

            set
            {
                if (_canView == value)
                {
                    return;
                }

                _canView = value;

                RaisePropertyChanged("CanView");
                ViewDetailsCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region LoadIncompletedCommand
        private RelayCommand _loadCommand;
        public RelayCommand LoadIncompletedCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand =
                        new RelayCommand(
                            () =>
                            {
                                LoadExecute();
                            },
                            () => CanLoad
                        );
                }
                return _loadCommand;
            }
            set
            {
                _loadCommand = value;
            }
        }

        public void LoadExecute()
        {
            this.LoadIncomplete();
        }

        public const string CanLoadPropertyName = "CanLoad";
        private bool _canLoad = false;
        public bool CanLoad
        {
            get
            {
                return _canLoad;
            }
            set
            {
                if (_canLoad == value)
                {
                    return;
                }
                _canLoad = value;

                RaisePropertyChanged(CanLoadPropertyName);
                LoadIncompletedCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        private void LoadCompleted()
        {
            var toDoItems = _repository.GetCompletedToDoItems();
            ToDoItems = new ObservableCollection<ToDoItem>(toDoItems);
        }

        #region LoadCompletedCommand
        private RelayCommand _loadCompletedCommand;
        public RelayCommand LoadCompletedCommand
        {
            get
            {
                if (_loadCompletedCommand == null)
                {
                    _loadCompletedCommand =
                        new RelayCommand(
                            () =>
                            {
                                LoadCompletedExecute();
                            },
                            () => CanLoadCompleted
                        );
                }
                return _loadCompletedCommand;
            }
            set
            {
                _loadCompletedCommand = value;
            }
        }

        public void LoadCompletedExecute()
        {
            this.LoadCompleted();
        }

        public const string CanLoadCompletedPropertyName = "CanLoadCompleted";
        private bool _canLoadCompleted = false;
        public bool CanLoadCompleted
        {
            get
            {
                return _canLoadCompleted;
            }
            set
            {
                if (_canLoadCompleted == value)
                {
                    return;
                }
                _canLoadCompleted = value;

                RaisePropertyChanged(CanLoadCompletedPropertyName);
                LoadCompletedCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion



        public void Cleanup()
        {
            ToDoItems = new ObservableCollection<ToDoItem>();
            SelectedToDoItem = null;
        }

    }
}
