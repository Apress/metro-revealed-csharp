using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MetroGrocer.Pages {

    public sealed partial class DetailPage : Page {
        public DetailPage() {
            this.InitializeComponent();

            ApplicationView.GetForCurrentView().ViewStateChanged
                += (sender, args) => {
                    string stateName = args.ViewState ==
                        ApplicationViewState.Snapped ? "Snapped" : "Others";
                    VisualStateManager.GoToState(this, stateName, false);
                };
        }

        private void HandleButtonClick(object sender, RoutedEventArgs e) {
            Windows.UI.ViewManagement.ApplicationView.TryUnsnap();
        }

    }
}
