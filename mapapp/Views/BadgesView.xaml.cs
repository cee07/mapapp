using System;
using System.Collections.Generic;
using mapapp.Models;
using mapapp.ViewModels;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class BadgesView : ContentView {

		private BadgesViewModel badgesViewModel;

		public BadgesView () {
			InitializeComponent();
			BindingContext = badgesViewModel = new BadgesViewModel();
			badgesViewModel.OnBadgesRequested += OnInitializedBadges;
		}

		public void Initialize () {
			badgesViewModel.RequestBadgesCommand.Execute(null);
		}

		void OnInitializedBadges (List<BadgeModel> obj) {
			badgesPanel.Children.Clear();
			for (int index = 0 ; index < obj.Count ; index++) {
				var badgeModel = obj[index];
				if (badgeModel != null) {
					BadgeValueView badgeValueView = new BadgeValueView(badgeModel);
					badgesPanel.Children.Add(badgeValueView);
				}
			}
		}
	}
}
