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
			for (int index = 0 ; index < 7 ; index++) {
				BadgeModel badgeModel = null;
				if (index < obj.Count) {
					badgeModel = obj[index];
					if (badgeModel != null) {
						int count = Convert.ToInt32(badgeModel.Count);
						badgeModel.Badge = (string.IsNullOrEmpty(badgeModel.Badge)) ? string.Format("badge{0}.png", index.ToString()) : badgeModel.Badge;
						badgeModel.Badge = (count > 0) ? badgeModel.Badge : string.Format("badge{0}.png", index.ToString());
					}
				} else {
					badgeModel = new BadgeModel();
					badgeModel.Badge = string.Format("badge{0}.png", index.ToString());
				}
				System.Diagnostics.Debug.WriteLine("Badges: " + badgeModel.Badge);
				BadgeValueView badgeValueView = new BadgeValueView(badgeModel);
				badgesPanel.Children.Add(badgeValueView);
			}
		}
	}
}
