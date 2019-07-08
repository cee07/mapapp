using System;
using mapapp.Droid;
using mapapp.Utils.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl))]
namespace mapapp.Droid {
	public class BaseUrl : IBaseUrl {
		public string Get () {
			return "file:///android_asset/";
		}
	}
}