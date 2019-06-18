using System;
using Foundation;
using mapapp.Helpers;
using mapapp.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(SettingsService))]
namespace mapapp.iOS {
	public class SettingsService : ISettingsService {

		public void OpenSettings () {
			UIApplication.SharedApplication.OpenUrl(new NSUrl("app-settings:"));
		}

	}
}