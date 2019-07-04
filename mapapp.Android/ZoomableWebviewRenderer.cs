using System;
using Android.Content;
using mapapp.Droid;
using mapapp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ZoomableWebview), typeof(ZoomableWebviewRenderer))]
namespace mapapp.Droid {
	public class ZoomableWebviewRenderer : WebViewRenderer {

		public ZoomableWebviewRenderer (Context context) : base(context) {
		}

		protected override void OnElementChanged (ElementChangedEventArgs<WebView> e) {
			base.OnElementChanged(e);
			Control.Settings.BuiltInZoomControls = true;
			Control.Settings.DisplayZoomControls = false;
		}
	}
}