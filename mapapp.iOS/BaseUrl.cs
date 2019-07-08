using System;
using Foundation;
using mapapp.iOS;
using mapapp.Utils.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl))]
namespace mapapp.iOS {
	public class BaseUrl : IBaseUrl {
		public string Get () {
			return NSBundle.MainBundle.BundlePath;
		}
	}
}
