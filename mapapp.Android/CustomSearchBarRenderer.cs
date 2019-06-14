using System;
using Android.Content;
using Android.Support.V4.Content;
using mapapp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace mapapp.Droid {
	public class CustomSearchBarRenderer : SearchBarRenderer {

		public CustomSearchBarRenderer (Context context) : base(context) {
		}

		protected override void OnElementChanged (ElementChangedEventArgs<SearchBar> e) {
			base.OnElementChanged(e);
			if (Control != null)
				Control.Background = ContextCompat.GetDrawable(Forms.Context, Resource.Drawable.custom_searchbar);
		}
	}
}
