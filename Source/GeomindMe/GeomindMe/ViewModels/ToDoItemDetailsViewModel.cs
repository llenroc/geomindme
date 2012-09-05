using System;
using System.Linq;
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
using GeomindMe.Models;
using GeomindMe.Services;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Device.Location;
using System.Text;
using JediNinja.Controls.WP;

namespace GeomindMe.ViewModels
{
	public class ToDoItemDetailsViewModel : ViewModelBase
	{
		IToDoItemRepository _repository;

		public ToDoItemDetailsViewModel(IToDoItemRepository repository)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository must not be null");
			}

			this._repository = repository;

			this.ToDoItem = new ToDoItem();
		}

		public ToDoItemDetailsViewModel(IToDoItemRepository repository, int id)
			: this(repository)
		{
			if (id <= 0)
			{
				throw new ArgumentException("id must be greater than 0!");
			}

			this.Load(id);
		}

		string _currentNavigateUri = string.Empty;
		public void Load(int id, string currentNavigateUri)
		{
			this.Load(id);
			this._currentNavigateUri = currentNavigateUri;
		}

		private void Load(int id)
		{
			var toDoItem = _repository.Find(id);
			if (toDoItem == null)
			{
				throw new InvalidOperationException(string.Format("ToDoItem with id {0} could not be found!", id));
			}

			ToDoItem = toDoItem;
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
				if (OnToDoItemChanged != null)
				{
					OnToDoItemChanged(ToDoItem.Location);
				}
			}
		}

		public delegate void LocationEventHandler(GeoCoordinate location);

		public LocationEventHandler OnToDoItemChanged;

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
								EditExecute();
							},
							() => CanEdit
						);
				}
				return _editCommand;
			}
			set
			{
				_editCommand = value;
			}
		}

		public void EditExecute()
		{
			if (ToDoItem == null)
			{
				throw new Exception("Fatal error:ToDoItem is null!");
			}

			string uriAddress = string.Format("/Views/ToDoItemEditViewPage.xaml?id={0}", ToDoItem.ToDoItemId);
			NavigationController.Instance.Navigate(new Uri(uriAddress, UriKind.Relative));
		}

		private bool _canEdit = false;
		public bool CanEdit
		{
			get
			{
				return _canEdit;
			}

			set
			{
				if (_canEdit == value)
				{
					return;
				}
				_canEdit = value;

				RaisePropertyChanged("CanEdit");
				EditCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

		#region DeleteCommand
		private RelayCommand _deleteCommand;
		public RelayCommand DeleteCommand
		{
			get
			{
				if (_deleteCommand == null)
				{
					_deleteCommand = new RelayCommand(
							() =>
							{
								DeleteExecute();
							},
							() => CanDelete
						);
				}
				return _deleteCommand;
			}
			set
			{
				_deleteCommand = value;
			}
		}

		public void DeleteExecute()
		{
			if (ToDoItem == null)
			{
				throw new NullReferenceException("ToDoItem must not be null!");
			}

			if (ToDoItem.ToDoItemId == 0)
			{
				return;
			}

			if (MessageBox.Show("Do you realy want to delete that ToDoItem?", "Confirm", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
			{
				return;
			}

			int toDoId = ToDoItem.ToDoItemId;
			_repository.Delete(toDoId);
			_repository.Save();

			this.GoBack();
		}

		private void GoBack()
		{
			NavigationController.Instance.GoBack();
		}

		private bool _canDelete = true;
		public bool CanDelete
		{
			get
			{
				return _canDelete;
			}

			set
			{
				if (_canDelete == value)
				{
					return;
				}
				_canDelete = value;

				RaisePropertyChanged("CanDelete");
				DeleteCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

		#region PinToStartCommand
		private RelayCommand _pinToStartCommand;
		public RelayCommand PinToStartCommand
		{
			get
			{
				if (_pinToStartCommand == null)
				{
					_pinToStartCommand =
						new RelayCommand(
							() =>
							{
								PinToStartExecute();
							},
							() => CanPinToStart
						);
				}
				return _pinToStartCommand;
			}
			set
			{
				_pinToStartCommand = value;
			}
		}

		public void PinToStartExecute()
		{
			string currentNavigateUri = _currentNavigateUri;
			if (string.IsNullOrEmpty(currentNavigateUri))
			{
				throw new NullReferenceException("currentNavigateUri must not be null!");
			}

			ShellTile tileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(currentNavigateUri));

			//test if Tile was created
			if (tileToFind == null)
			{
				string locationAddress = ToDoItem.LocationAddress;
				string title = locationAddress.Length<=20?locationAddress:string.Format("{0}...",locationAddress.Substring(0, 17));
				StandardTileData newTileData = new StandardTileData
				{
					BackgroundImage = new Uri("/Images/tile-icon.png", UriKind.Relative),
					Title = title,
					Count = null,
					BackTitle = ToDoItem.LocationAddress,
					BackContent = ToDoItem.Text,
					//BackBackgroundImage = new Uri("/Images/tile-icon.png", UriKind.Relative)
				};
				ShellTile.Create(new Uri(currentNavigateUri, UriKind.Relative), newTileData); //exits application
			}
		}

		public const string CanPinToStartPropertyName = "CanPinToStart";
		private bool _canPinToStart = false;
		public bool CanPinToStart
		{
			get
			{
				return _canPinToStart;
			}
			set
			{
				if (_canPinToStart == value)
				{
					return;
				}
				_canPinToStart = value;

				RaisePropertyChanged(CanPinToStartPropertyName);
				PinToStartCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

		#region GetDirectionsCommand
		private RelayCommand _getDirectionsCommand;
		public RelayCommand GetDirectionsCommand
		{
			get
			{
				if (_getDirectionsCommand == null)
				{
					_getDirectionsCommand =
						new RelayCommand(
							() =>
							{
								GetDirectionsExecute();
							},
							() => CanGetDirections
						);
				}
				return _getDirectionsCommand;
			}
			set
			{
				_getDirectionsCommand = value;
			}
		}

		public void GetDirectionsExecute()
		{
			if (ToDoItem == null)
			{
				return;
			}
			string locationAddressToNavigate = ToDoItem.LocationAddress;
			double latitude = ToDoItem.LocationLatitude;
			double longitude = ToDoItem.LocationLongitude;

			GeoCoordinate geoCoordinate = new GeoCoordinate(latitude, longitude);
			BingMapsDirectionsTask directionsTask = new BingMapsDirectionsTask();

			LabeledMapLocation labeledMapLocation = new LabeledMapLocation(locationAddressToNavigate, geoCoordinate);
			directionsTask.End = labeledMapLocation;
			directionsTask.Show();
		}

		public const string CanGetDirectionsPropertyName = "CanGetDirections";
		private bool _canGetDirections = true;
		public bool CanGetDirections
		{
			get
			{
				return _canGetDirections;
			}
			set
			{
				if (_canGetDirections == value)
				{
					return;
				}
				_canGetDirections = value;

				RaisePropertyChanged(CanGetDirectionsPropertyName);
				GetDirectionsCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

		#region SendAsSmsCommand

		private RelayCommand _sendAsSmsCommand;
		public RelayCommand SendAsSmsCommand
		{
			get
			{
				if (_sendAsSmsCommand == null)
				{
					_sendAsSmsCommand =
						new RelayCommand(
							() =>
							{
								SendAsSmsExecute();
							},
							() => CanSendAsSms
						);
				}
				return _sendAsSmsCommand;
			}
			set
			{
				_sendAsSmsCommand = value;
			}
		}

		public void SendAsSmsExecute()
		{
			if (ToDoItem == null)
			{
				return;
			}

			var toDoItem = ToDoItem;

			string text = toDoItem.Text;
			double latitude = toDoItem.LocationLatitude;
			double longitude = toDoItem.LocationLongitude;
			string address = toDoItem.LocationAddress;
			string mapUrlAddress = string.Format(@"http://www.bing.com/maps/?v=2&cp={0}~{1}&lvl=10", latitude, longitude);

			StringBuilder messsageSb = new StringBuilder();
			messsageSb.Append(string.Format("TO DO:\n"));
			messsageSb.Append(text);
			messsageSb.Append(string.Format(", at {0}", address));
			messsageSb.Append(string.Format(", ({0}, {1})", latitude, longitude));

			string messageText = messsageSb.ToString();

			SmsComposeTask smsComposeTask = new SmsComposeTask();
			smsComposeTask.Body = messageText;
			smsComposeTask.Show();
		}

		public const string CanSendAsSmsPropertyName = "CanSendAsSms";
		private bool _canSendAsSms = false;
		public bool CanSendAsSms
		{
			get
			{
				return _canSendAsSms;
			}
			set
			{
				if (_canSendAsSms == value)
				{
					return;
				}
				_canSendAsSms = value;

				RaisePropertyChanged(CanSendAsSmsPropertyName);
				SendAsSmsCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

		#region SendAsEmailCommand

		private RelayCommand _sendAsEmailCommand;
		public RelayCommand SendAsEmailCommand
		{
			get
			{
				if (_sendAsEmailCommand == null)
				{
					_sendAsEmailCommand =
						new RelayCommand(
							() =>
							{
								SendAsEmailExecute();
							},
							() => CanSendAsEmail
						);
				}
				return _sendAsEmailCommand;
			}
			set
			{
				_sendAsEmailCommand = value;
			}
		}

		public void SendAsEmailExecute()
		{
			if (ToDoItem == null)
			{
				return;
			}

			var toDoItem = ToDoItem;

			string text = toDoItem.Text;
			double latitude = toDoItem.LocationLatitude;
			double longitude = toDoItem.LocationLongitude;
			string address = toDoItem.LocationAddress;
			string mapUrlAddress = string.Format(@"http://www.bing.com/maps/?v=2&cp={0}~{1}&lvl=10", latitude, longitude);

			StringBuilder messsageSb = new StringBuilder();
			messsageSb.AppendLine(string.Format("TO DO:\n"));
			messsageSb.AppendLine(text);
			messsageSb.AppendLine(string.Format("Address:{0}", address));
			messsageSb.AppendLine(string.Format("Url: {0}", mapUrlAddress));
			messsageSb.AppendLine(string.Format("Location gps: {0}, {1}", latitude, longitude));

			string messageText = messsageSb.ToString();

			EmailComposeTask emailComposeTask = new EmailComposeTask();
			emailComposeTask.Body = messageText;
			emailComposeTask.Show();
		}

		public const string CanSendAsEmailPropertyName = "CanSendAsEmail";
		private bool _canSendAsEmail = false;
		public bool CanSendAsEmail
		{
			get
			{
				return _canSendAsEmail;
			}
			set
			{
				if (_canSendAsEmail == value)
				{
					return;
				}
				_canSendAsEmail = value;

				RaisePropertyChanged(CanSendAsEmailPropertyName);
				SendAsEmailCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

		#region CompleteCommand

		private RelayCommand _completeCommand;
		public RelayCommand CompleteCommand
		{
			get
			{
				if (_completeCommand == null)
				{
					_completeCommand =
						new RelayCommand(
							() =>
							{
								CompleteExecute();
							},
							() => CanComplete
						);
				}
				return _completeCommand;
			}
			set
			{
				_completeCommand = value;
			}
		}

		public void CompleteExecute()
		{
			if (ToDoItem == null)
			{
				throw new NullReferenceException("ToDoItem must not be null!");
			}

			var toDoItem = ToDoItem;

			if (toDoItem.IsCompleted)
			{
				toDoItem.IsCompleted = false;
			}
			else
			{
				toDoItem.IsCompleted = true;
			}

			_repository.InsertOrUpdate(this.ToDoItem);
			_repository.Save();

			this.GoBack();
		}

		public const string CanCompletePropertyName = "CanComplete";
		private bool _canComplete = false;
		public bool CanComplete
		{
			get
			{
				return _canComplete;
			}
			set
			{
				if (_canComplete == value)
				{
					return;
				}
				_canComplete = value;

				RaisePropertyChanged(CanCompletePropertyName);
				CompleteCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

		public void Cleanup()
		{
			this.ToDoItem = new ToDoItem();
			this._currentNavigateUri = string.Empty;
		}
	}
}
