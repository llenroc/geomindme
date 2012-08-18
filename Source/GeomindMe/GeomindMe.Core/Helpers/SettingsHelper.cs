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
using System.IO.IsolatedStorage;

namespace GeomindMe.Helpers
{
	public static class SettingsHelper
	{
		public const string SETTING_ENABLELOCATIONSERVICES = "IsLocationServicesEnabled";
		public const string SETTING_ENABLEBACKGROUNDSERVICES = "IsBackgroundServicesEnabled";

		#region LocationServices
		/// <summary>
		/// Indicates if setting is already set.
		/// </summary>
		/// <returns></returns>
		public static bool IsLocationServicesSettingSet()
		{
			var appSettings = IsolatedStorageSettings.ApplicationSettings;
			
			bool isSettingSet = appSettings.Contains(SETTING_ENABLELOCATIONSERVICES);
			return isSettingSet;
		}
	
		/// <summary>
		/// Get IsLocationServicesEnabled setting
		/// </summary>
		/// <returns></returns>
		public static bool IsLocationServicesEnabled()
		{
			var appSettings = IsolatedStorageSettings.ApplicationSettings;

			bool areEnabled = appSettings.Contains(SETTING_ENABLELOCATIONSERVICES) ? (bool)appSettings[SETTING_ENABLELOCATIONSERVICES] : false;
			return areEnabled;
		}

		/// <summary>
		/// Sets IsLcoationservicesEnabled setting
		/// </summary>
		/// <param name="isEnabled"></param>
		public static void SetIsLocationServicesEnabled(bool isEnabled)
		{
			var appSettings = IsolatedStorageSettings.ApplicationSettings;
			if (appSettings.Contains(SETTING_ENABLELOCATIONSERVICES))
			{
				bool oldValue = (bool)appSettings[SETTING_ENABLELOCATIONSERVICES];
				if (oldValue == isEnabled)//we dont need to save setting as they are already set
				{
					return;
				}
				appSettings[SETTING_ENABLELOCATIONSERVICES] = isEnabled;
			}
			else
			{
				appSettings.Add(SETTING_ENABLELOCATIONSERVICES, isEnabled);
			}
			appSettings.Save();
		}
		#endregion

		#region BackgroundServices - Setting is not currently used
		///// <summary>
		///// Indicates if setting is already set.
		///// </summary>
		///// <returns></returns>
		//public static bool IsBackgroundServicesSettingSet()
		//{
		//    var appSettings = IsolatedStorageSettings.ApplicationSettings;

		//    bool isSettingSet = appSettings.Contains(SETTING_ENABLEBACKGROUNDSERVICES);
		//    return isSettingSet;
		//}

		///// <summary>
		///// Get IsLocationServicesEnabled setting
		///// </summary>
		///// <returns></returns>
		//public static bool IsBackgroundServicesEnabled()
		//{
		//    var appSettings = IsolatedStorageSettings.ApplicationSettings;

		//    bool areEnabled = appSettings.Contains(SETTING_ENABLEBACKGROUNDSERVICES) ? (bool)appSettings[SETTING_ENABLEBACKGROUNDSERVICES] : false;
		//    return areEnabled;
		//}

		///// <summary>
		///// Sets IsLcoationservicesEnabled setting
		///// </summary>
		///// <param name="isEnabled"></param>
		//public static void SetIsBackgroundServicesEnabled(bool isEnabled)
		//{
		//    var appSettings = IsolatedStorageSettings.ApplicationSettings;
		//    if (appSettings.Contains(SETTING_ENABLEBACKGROUNDSERVICES))
		//    {
		//        appSettings[SETTING_ENABLEBACKGROUNDSERVICES] = isEnabled;
		//    }
		//    else
		//    {
		//        appSettings.Add(SETTING_ENABLEBACKGROUNDSERVICES, isEnabled);
		//    }
		//    appSettings.Save();
		//}
		#endregion
	}
}
