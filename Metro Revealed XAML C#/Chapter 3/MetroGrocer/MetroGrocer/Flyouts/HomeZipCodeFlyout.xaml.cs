using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MetroGrocer.Flyouts {

    public sealed partial class HomeZipCodeFlyout : UserControl {

        public HomeZipCodeFlyout() {
            this.InitializeComponent();
        }

        public void Show(Page page, AppBar appbar, Button button) {
            HomeZipCodePopup.IsOpen = true;
            FlyoutHelper.ShowRelativeToAppBar(HomeZipCodePopup, page, appbar, button);
        }
    
        private void OKButtonClick(object sender, RoutedEventArgs e) {

            var x = DataContext;

            System.Diagnostics.Debugger.Break();

            HomeZipCodePopup.IsOpen = false;
        }
    }
}
