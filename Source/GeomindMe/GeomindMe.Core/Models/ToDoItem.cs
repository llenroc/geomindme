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
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;
using GeomindMe.ViewModels;
using System.Device.Location;

namespace GeomindMe.Models
{
	[Table(Name = "ToDoItems")]
	public class ToDoItem : ViewModelBase
	{
		private int _toDoItemId;
		[Column(IsPrimaryKey = true,
				IsDbGenerated = true)]
		public int ToDoItemId
		{
			get
			{
				return _toDoItemId;
			}

			set
			{
				if (_toDoItemId == value)
				{
					return;
				}
				_toDoItemId = value;
				RaisePropertyChanged("ToDoItemId");
			}
		}

		private string _text;
		[Column(CanBeNull = false)]
		public string Text
		{
			get
			{
				return _text;
			}

			set
			{
				if (_text == value)
				{
					return;
				}
				_text = value;
				RaisePropertyChanged("Text");
			}
		}

		//private int _priority;
		//[Column]
		//public int Priority
		//{
		//    get
		//    {
		//        return _priority;
		//    }

		//    set
		//    {
		//        if (_priority == value)
		//        {
		//            return;
		//        }
		//        _priority = value;
		//        RaisePropertyChanged("Priority");
		//    }
		//}

		private string _locationAddress;
		[Column(DbType = "NVarChar(255) NOT NULL")]
		public string LocationAddress
		{
			get
			{
				return _locationAddress;
			}

			set
			{
				if (_locationAddress == value)
				{
					return;
				}
				_locationAddress = value;
				RaisePropertyChanged("Location");
			}
		}

		private double _locationLongitude;
		[Column]
		public double LocationLongitude
		{
			get
			{
				return _locationLongitude;
			}

			set
			{
				if (_locationLongitude == value)
				{
					return;
				}
				_locationLongitude = value;
				RaisePropertyChanged("LocationLongitude");
			}
		}

		private double _locationLatitude;
		[Column]
		public double LocationLatitude
		{
			get
			{
				return _locationLatitude;
			}

			set
			{
				if (_locationLatitude == value)
				{
					return;
				}
				_locationLatitude = value;
				RaisePropertyChanged("LocationLatitude");
			}
		}

		private double _reminderRadius;
		[Column]
		public double ReminderRadius
		{
			get
			{
				return _reminderRadius;
			}

			set
			{
				if (_reminderRadius == value)
				{
					return;
				}
				_reminderRadius = value;
				RaisePropertyChanged("ReminderRadius");
			}
		}

		private bool _reminderIsEnabled;
		[Column]
		public bool ReminderIsEnabled
		{
			get
			{
				return _reminderIsEnabled;
			}

			set
			{
				if (_reminderIsEnabled == value)
				{
					return;
				}
				_reminderIsEnabled = value;
				RaisePropertyChanged("ReminderIsEnabled");
			}
		}

		private bool _isCompleted;
		[Column]
		public bool IsCompleted
		{
			get
			{
				return _isCompleted;
			}

			set
			{
				if (_isCompleted == value)
				{
					return;
				}
				_isCompleted = value;
				RaisePropertyChanged("IsCompleted");
			}
		}

		public GeoCoordinate Location
		{
			get
			{
				return new GeoCoordinate(LocationLatitude, LocationLongitude);
			}
		}

		//private DateTime _completedDate;
		//[Column(CanBeNull = true)]
		//public DateTime CompletedDate
		//{
		//    get
		//    {
		//        return _completedDate;
		//    }

		//    set
		//    {
		//        if (_completedDate == value)
		//        {
		//            return;
		//        }
		//        _completedDate = value;
		//        RaisePropertyChanged("CompletedDate");
		//    }
		//}
	}


}
