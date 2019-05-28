using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using mapapp.Helpers;
using mapapp.Models;
using MvvmHelpers;
using Plugin.Messaging;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace mapapp.ViewModels {
	public class PinDetailPageViewModel : BaseDataViewModel {

		public ObservableRangeCollection<CustomPin> Pins { get; private set; }

		private PinModel pinModel;
		public PinModel PinModelData {
			get { return pinModel; }
			set { SetProperty(ref pinModel, value); }
		}

		public Position CurrentPosition { get {
				return new Position(Convert.ToDouble(PinModelData.Latitude),
									Convert.ToDouble(PinModelData.Longitude)); }
		}

		public PinDetailPageViewModel(PinModel pin) {
			Pins = new ObservableRangeCollection<CustomPin>();
			PinModelData = pin;
			CustomPin customPin = new CustomPin() {
				Label = pin.EstablishmentName,
				Address = pin.Address,
				Position = new Position(Convert.ToDouble(pin.Latitude), Convert.ToDouble(pin.Longitude)),
				Type = Xamarin.Forms.Maps.PinType.Place,
				PinType = pin.PinModelType
			};
			Pins.AddRange(new List<CustomPin>() { customPin });
			CallCommand = new Command(ExecuteCallCommand);
		}

		public ICommand CallCommand { get; private set; }

		void ExecuteCallCommand() {
			var phoneDialer = CrossMessaging.Current.PhoneDialer;
			if (phoneDialer.CanMakePhoneCall)
				phoneDialer.MakePhoneCall(PinModelData.Contact);
		}

	}
}