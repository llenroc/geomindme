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
using GeomindMe.Helpers;
using Microsoft.Phone.Tasks;

namespace GeomindMe.ViewModels
{
    public class BingMapsLocationSearchViewModel : ViewModelBase
    {
        private string _location;
        public string Location
        {
            get
            {
                return _location;
            }

            set
            {
                if (_location == value)
                {
                    return;
                }
                _location = value;
                RaisePropertyChanged("Location");
            }
        }

        #region SearchCommand
        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand =
                        new RelayCommand(
                            () =>
                            {
                                SearchExecute();
                            },
                            () => CanSearch
                        );
                }
                return _searchCommand;
            }
            set
            {
                _searchCommand = value;
            }
        }

        public void SearchExecute()
        {
            
        }

        public const string CanSearchPropertyName = "CanSearch";
        private bool _canSearch = false;
        public bool CanSearch
        {
            get
            {
                return _canSearch;
            }
            set
            {
                if (_canSearch == value)
                {
                    return;
                }
                _canSearch = value;

                RaisePropertyChanged(CanSearchPropertyName);
                SearchCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion
    }
}
