using System;
using mapapp.Droid;
using mapapp.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(SettingsService))]
namespace mapapp.Droid {
	public class SettingsService : ISettingsService {

		public void OpenSettings () {
			Android.App.Application.Context.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionLocat‌​ionSourceSettings));
		}

	}
}