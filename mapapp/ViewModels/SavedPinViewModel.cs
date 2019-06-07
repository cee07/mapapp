using System;
using System.Collections.Generic;
using mapapp.Models;
using MvvmHelpers;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mapapp.ViewModels {
	public class SavedPinViewModel {

		public ObservableRangeCollection<PinModel> SavedPins { get; private set; }

		public SavedPinViewModel () {
			SavedPins = new ObservableRangeCollection<PinModel>();
			var pins = GetSavedPins();
			if (pins != null) {
				SavedPins.Clear();
				SavedPins.AddRange(pins);
			} 
		}

		private List<PinModel> GetSavedPins() {
			var pinsRawData = Preferences.Get("SavedPins", null);
			if (string.IsNullOrEmpty(pinsRawData))
				return null;
			var savedPins = JsonConvert.DeserializeObject<List<PinModel>>(pinsRawData);
			return savedPins;
		}
	}
}
