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
using System.Windows.Navigation;
using GeomindMe.Services;
using GeomindMe.Helpers;

namespace GeomindMe.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region AboutCommand

        private RelayCommand _aboutCommand;
        public RelayCommand AboutCommand
        {
            get
            {
                if (_aboutCommand == null)
                {
                    _aboutCommand =
                        new RelayCommand(
                            () =>
                            {
                                AboutExecute();
                            },
                            () => CanAbout
                        );
                }
                return _aboutCommand;
            }
            set
            {
                _aboutCommand = value;
            }
        }

        public void AboutExecute()
        {
            string aboutPageUri = "/YourLastAboutDialog;component/AboutPage.xaml";
            NavigationController.Instance.Navigate(new Uri(aboutPageUri, UriKind.Relative));
        }

        public const string CanAboutPropertyName = "CanAbout";
        private bool _canAbout = false;
        public bool CanAbout
        {
            get
            {
                return _canAbout;
            }
            set
            {
                if (_canAbout == value)
                {
                    return;
                }
                _canAbout = value;

                RaisePropertyChanged(CanAboutPropertyName);
                AboutCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region SettingsCommand

        private RelayCommand _settingsCommand;
        public RelayCommand SettingsCommand
        {
            get
            {
                if (_settingsCommand == null)
                {
                    _settingsCommand =
                        new RelayCommand(
                            () =>
                            {
                                SettingsExecute();
                            },
                            () => CanSettings
                        );
                }
                return _settingsCommand;
            }
            set
            {
                _settingsCommand = value;
            }
        }

        public void SettingsExecute()
        {
            string aboutPageUri = "/Views/SettingsPage.xaml";
            NavigationController.Instance.Navigate(new Uri(aboutPageUri, UriKind.Relative));
        }

        public const string CanSettingsPropertyName = "CanSettings";
        private bool _canSettings = false;
        public bool CanSettings
        {
            get
            {
                return _canSettings;
            }
            set
            {
                if (_canSettings == value)
                {
                    return;
                }
                _canSettings = value;

                RaisePropertyChanged(CanSettingsPropertyName);
                SettingsCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        internal void Cleanup()
        {
            throw new NotImplementedException();
        }
    }
}
