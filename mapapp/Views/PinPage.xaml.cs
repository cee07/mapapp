using System;
using System.Collections.Generic;
using mapapp.ViewModels;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class PinPage : ContentPage {

		private SavedPinView savedPinView;
		private RecenPinView recenPinView;
		private SubmitPinsListView submitPinsListView;

		private ToolbarItem addToolbarItem;

		private PinPageState pageState;

		private bool hasInitialized = false;
		private PinsPageViewModel pinsPageViewModel;

		private SubmitPinView submitPinView;

		public PinPage () {
			InitializeComponent();
			BindingContext = pinsPageViewModel = new PinsPageViewModel();
			submitPinView = new SubmitPinView();
			pinsPageViewModel.OnClickAddItem += PinsPageViewModel_OnClickAddItem;
			addToolbarItem = new ToolbarItem() {
				Text = "Add Pin",
				Command = pinsPageViewModel.AddItemCommand
			};
			InitializeViews();
		}

		void PinsPageViewModel_OnClickAddItem (object sender, System.EventArgs e) {
			Navigation.PushAsync(new SubmitNewPin());
		}

		protected override void OnAppearing () {
			base.OnAppearing();
			if (!hasInitialized) {
				hasInitialized = true;
				SetPageState(PinPageState.RecentPins);
			} else
				SetPageState(pageState);
		}

		void InitializeViews() {
			savedPinView = new SavedPinView();
			recenPinView = new RecenPinView();
			submitPinsListView = new SubmitPinsListView();
			pinDetailPanel.Children.Clear();
			pinDetailPanel.Children.Add(savedPinView);
			pinDetailPanel.Children.Add(recenPinView);
			pinDetailPanel.Children.Add(submitPinsListView);
		}

		void OnClickedButtonState (object sender, System.EventArgs e) {
			var imageButton = (Button) sender;
			SetPageState((PinPageState) imageButton.CommandParameter);
		}

		void SetPageState (PinPageState pinPageState) {
			ResetPageState();
			switch (pinPageState) {
				case PinPageState.SavedPins:
					Title = "Saved Pins";
					pindiv.IsVisible = true;
					savedPinView.IsVisible = true;
					savedPinView.InitializeSavedPins();
					break;
				case PinPageState.RecentPins:
					Title = "Recent Pins";
					pindiv2.IsVisible = true;
					recenPinView.IsVisible = true;
					recenPinView.InitializeRecentPins();
					break;
				case PinPageState.SubmittedPins:
					Title = "Submitted Pins";
					pindiv3.IsVisible = true;
					ToolbarItems.Add(addToolbarItem);
					submitPinsListView.IsVisible = true;
					submitPinsListView.InitializeSubmittedPins();
					break;
			}
			pageState = pinPageState;
		}

		void ResetPageState() {
			savedPinView.IsVisible = false;
			recenPinView.IsVisible = false;
			submitPinsListView.IsVisible = false;
			toolBarAdd.IsEnabled = false;
			ToolbarItems.Clear();
			pindiv.IsVisible = false;
			pindiv2.IsVisible = false;
			pindiv3.IsVisible = false;
		}


	}

	public enum PinPageState {
		SavedPins,
		RecentPins,
		SubmittedPins
	}
}
