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
using Microsoft.Phone.Tasks;

namespace GeomindMe.Views
{
	public partial class PrivacyPolicy : PhoneApplicationPage
	{
		public PrivacyPolicy()
		{
			InitializeComponent();
		}

		private void Contact_Click(object sender, RoutedEventArgs e)
		{
			EmailComposeTask email = new EmailComposeTask()
			{
				To = "tbmihailov@gmail.com",
				Subject = "Geomindme Privacy Policy",
				Body = "I have a question about GeomindMe Privacy Policy:\n",
			};
		}

		private void CloseMenuItem_Click(object sender, EventArgs e)
		{
			NavigationService.GoBack();
		}
	}
}