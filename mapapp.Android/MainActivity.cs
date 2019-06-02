﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.FacebookClient;
using Android.Content;

namespace mapapp.Droid {
	[Activity(Label = "mapapp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
		protected override void OnCreate (Bundle savedInstanceState) {
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;
			FacebookClientManager.Initialize(this);
			base.OnCreate(savedInstanceState);
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			//ImageCircleRenderer.Init();

			LoadApplication(new App());
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent intent) {
			base.OnActivityResult(requestCode, resultCode, intent);
			FacebookClientManager.OnActivityResult(requestCode, resultCode, intent);
		}
	}
}