﻿using System;
using System.Collections.Generic;
using System.Linq;
using Facebook.CoreKit;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace mapapp.iOS {
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options) {
			global::Xamarin.Forms.Forms.Init();
			ImageCircleRenderer.Init();
			Xamarin.FormsMaps.Init();
			FacebookClientManager.Initialize(app, options);
			GoogleClientManager.Initialize();
			UITabBar.Appearance.TintColor = Xamarin.Forms.Color.FromHex("#D35453").ToUIColor();
			LoadApplication(new App());
			return base.FinishedLaunching(app, options);
		}

		public override void OnActivated (UIApplication uiApplication) {
			base.OnActivated(uiApplication);
			AppEvents.ActivateApp();
			FacebookClientManager.OnActivated();
		}

		public override bool OpenUrl (UIApplication app, NSUrl url, NSDictionary options) {
			return FacebookClientManager.OpenUrl(app, url, options);
			return GoogleClientManager.OnOpenUrl(app, url, options);
		}

		public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation) {
			return FacebookClientManager.OpenUrl(application, url, sourceApplication, annotation);
		}
	}
}