using System;
using CoreGraphics;
using mapapp.Helpers;
using mapapp.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomProgressBar), typeof(CustomProgressBarRenderer))]
namespace mapapp.iOS {
	public class CustomProgressBarRenderer  : ProgressBarRenderer {

		protected override void OnElementChanged (ElementChangedEventArgs<ProgressBar> e) {
			base.OnElementChanged(e);
		}

		public override void LayoutSubviews () {
			base.LayoutSubviews();

			var x = 100f;
			var y = 30f;

			CGAffineTransform transform = CGAffineTransform.MakeScale(x, y);
			Control.Transform = transform;
			Control.ClipsToBounds = true;
			Control.Layer.MasksToBounds = true;
		}

	}
}
