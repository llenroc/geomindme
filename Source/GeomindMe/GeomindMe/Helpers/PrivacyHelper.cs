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

namespace GeomindMe.Helpers
{
	public static class PrivacyHelper
	{
		/// <summary>
		/// Shows Enable Location Services privacy prompt if priavcy setting is not set.
		/// Shows the prompt if user is has not responsed yet
		/// </summary>
		public static void ShowLocationServicesPrivacyPromptIfNotSet()
		{
			if (!SettingsHelper.IsLocationServicesSettingSet())
			{
				ShowLocationServicesPrivacyPrompt();
			}
		}

		/// <summary>
		/// Shows LocationServices enable privacy prompt
		/// </summary>
		public static void ShowLocationServicesPrivacyPrompt()
		{
			string caption = "Enable Location Services";
			string message = string.Concat("  Do you allow GeomindMe to access your location?\n",
											"  If you do not enable location services GeomindMe will not be able to notify you for your tasks.\n",
											"\n",
											"Privacy\n",
											"   Your privacy is important to us. We use your location information to Provide requested location services. Your information is not stored on server.\n" ,
											"   If you have any question contact me at tbmihailov@gmail.com\n", 
											"You can enable/disable Location Services in Settings page.\n");
			var messageResult = MessageBox.Show(message, caption, MessageBoxButton.OKCancel);
			
			bool enableLocationServices = false;
			if(messageResult == MessageBoxResult.OK)
			{
				enableLocationServices = true;
			}
			else
			{
				enableLocationServices = false;
			}
			SettingsHelper.SetIsLocationServicesEnabled(enableLocationServices);
		}

		/// <summary>
		/// Checks if all privacy settings are set
		/// </summary>
		public  static void PrivacyCheck()
		{
			ShowLocationServicesPrivacyPromptIfNotSet();
		}
	}
}
