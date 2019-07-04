using System;
using mapapp.Helpers;
using mapapp.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ZoomableWebview), typeof(ZoomableWebviewRenderer))]
namespace mapapp.iOS {
	public class ZoomableWebviewRenderer : WebViewRenderer {

		public ZoomableWebviewRenderer () {
		}

		protected override void OnElementChanged (VisualElementChangedEventArgs e) {
			base.OnElementChanged(e);
			var view = (UIWebView) NativeView;
			view.ScrollView.ScrollEnabled = true;
			view.ScalesPageToFit = false;
		}
	}
}
