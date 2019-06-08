using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.FacebookClient;
using Android.Content;
using Plugin.GoogleClient;

namespace mapapp.Droid {
	[Activity(Label = "mapapp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
		protected override void OnCreate (Bundle savedInstanceState) {
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;
			Xamarin.FormsMaps.Init(this, savedInstanceState);
			Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
			base.OnCreate(savedInstanceState);
			FacebookClientManager.Initialize(this);
			GoogleClientManager.Initialize(this);
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			//ImageCircleRenderer.Init();

			LoadApplication(new App());
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent intent) {
			base.OnActivityResult(requestCode, resultCode, intent);
			FacebookClientManager.OnActivityResult(requestCode, resultCode, intent);
			GoogleClientManager.OnAuthCompleted(requestCode, resultCode, intent);
		}

		public override void OnRequestPermissionsResult (int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults) {
			Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}