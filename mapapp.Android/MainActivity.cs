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
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Permission = Plugin.Permissions.Abstractions.Permission;

namespace mapapp.Droid {
	[Activity(Label = "MApp", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
			 ScreenOrientation = ScreenOrientation.Portrait)]
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

			CheckPermissions();

			LoadApplication(new App());
		}

		public static int DefaultMarker { get { return Resource.Drawable.marker; }}
		public static int CouponMarker { get { return Resource.Drawable.marker_coupon; } }

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent intent) {
			base.OnActivityResult(requestCode, resultCode, intent);
			FacebookClientManager.OnActivityResult(requestCode, resultCode, intent);
			GoogleClientManager.OnAuthCompleted(requestCode, resultCode, intent);
		}

		public override void OnRequestPermissionsResult (int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults) {
			Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		async void CheckPermissions () {
			var locationPermission = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
			if (locationPermission != PermissionStatus.Granted) {
				var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
				if (results.ContainsKey(Permission.Location))
					locationPermission = results[Permission.Location];
			}

		}
	}
}