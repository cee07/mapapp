using System;
using System.Collections.Generic;
using mapapp.Models;
using MvvmHelpers;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mapapp.ViewModels {
	public class RecentPinViewModel {

		public ObservableRangeCollection<PinModel> RecentPins { get; private set; }

		public RecentPinViewModel () {
			RecentPins = new ObservableRangeCollection<PinModel>();
			var pins = GetSavedPins();
			if (pins != null) {
				RecentPins.Clear();
				RecentPins.AddRange(pins);
			} else {
				Application.Current.MainPage.DisplayAlert("Info",
														  "There are no recent pins.",
														  "OK");
			}
		}

		private List<PinModel> GetSavedPins () {
			var pinsRawData = Preferences.Get("RecentPins", null);
			if (string.IsNullOrEmpty(pinsRawData))
				return null;
			var savedPins = JsonConvert.DeserializeObject<List<PinModel>>(pinsRawData);
			return savedPins;
		}
	}
}
