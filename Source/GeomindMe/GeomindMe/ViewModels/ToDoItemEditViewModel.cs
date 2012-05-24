using System;
using System.Windows;
using GeomindMe.Models;
using GeomindMe.Helpers;
using GeomindMe.Services;

namespace GeomindMe.ViewModels
{
    public class ToDoItemEditViewModel : ViewModelBase
    {
        IToDoItemRepository _repository;

        public ToDoItemEditViewModel(IToDoItemRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository must not be null");
            }

            this._repository = repository;

            this.ToDoItem = new ToDoItem() 
                            {
                                    IsCompleted = false,
                                    ReminderIsEnabled = true,
                            };
        }

        public ToDoItemEditViewModel(IToDoItemRepository repository, int id)
            :this(repository)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id must be greater than 0!");
            }

            this.Load(id);
        }

        private ToDoItem _toDoItem;
        public ToDoItem ToDoItem
        {
            get
            {
                return _toDoItem;
            }

            set
            {
                if (_toDoItem == value)
                {
                    return;
                }
                _toDoItem = value;
                RaisePropertyChanged("ToDoItem");
            }
        }

        public void CreateNew()
        {
            this.ToDoItem = new ToDoItem();
            this.ToDoItem.ReminderIsEnabled = true;
            this.ToDoItem.IsCompleted = false;
        }

        public void Load(int id)
        {
            var toDoItem = _repository.Find(id);
            if (toDoItem == null)
            {
                throw new InvalidOperationException(string.Format("ToDoItem with id {0} could not be found!", id));
            }

            ToDoItem = toDoItem;
        }

        private void ValidateData(out bool isDataValid)
        {
            string errorMessage = string.Empty;
            bool hasError = false;
            isDataValid = true;

            //property validation
            bool isTextValid = IsTextValid();
            if (!isTextValid)
            {
                errorMessage += "Text is empty or invalid!\n";
                hasError = true;
            }

            if (hasError)
            {
                DisplayMessage(errorMessage, "Data error");
            }

            isDataValid = !hasError;

            return;
        }

        private void DisplayMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);
        }

        private bool IsTextValid()
        {
            bool isTextValid = !((string.IsNullOrEmpty(ToDoItem.Text)
                                    || string.IsNullOrWhiteSpace(ToDoItem.Text)));
            return isTextValid;
        }

        #region SaveCommand
        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = 
                        new RelayCommand(
                            () =>
                            {
                                SaveExecute();
                            },
                            () => CanSave
                        );
                }
                return _saveCommand;
            }
            set
            {
                _saveCommand = value;
            }
        }

        public void SaveExecute()
        {
            if(ToDoItem == null)
            {
                throw new NullReferenceException("ToDoItem must not be null!");
            }

            bool isDataValid = false;
            ValidateData(out isDataValid);
            if (!isDataValid)
            {
                return;
            }

            _repository.InsertOrUpdate(this.ToDoItem);
            _repository.Save();

            this.GoBack();
        }

        private void GoBack()
        {
            NavigationController.Instance.GoBack();
        }

        private bool _canSave = false;
        public bool CanSave
        {
            get
            {
                return _canSave;
            }

            set
            {
                if (_canSave == value)
                {
                    return;
                }
                _canSave = value;

                RaisePropertyChanged("CanSave");
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region CancelCommand
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand =
                        new RelayCommand(
                            () =>
                            {
                                CancelExecute();
                            },
                            () => CanCancel
                        );
                }
                return _cancelCommand;
            }
            set
            {
                _cancelCommand = value;
            }
        }

        public void CancelExecute()
        {
            this.Cleanup();
            this.GoBack();
        }

        public const string CanCancelPropertyName = "CanCancel";
        private bool _canCancel = false;
        public bool CanCancel
        {
            get
            {
                return _canCancel;
            }
            set
            {
                if (_canCancel == value)
                {
                    return;
                }
                _canCancel = value;

                RaisePropertyChanged(CanCancelPropertyName);
                CancelCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        public void Cleanup()
        {
            this.ToDoItem = null;
        }
    }
}
