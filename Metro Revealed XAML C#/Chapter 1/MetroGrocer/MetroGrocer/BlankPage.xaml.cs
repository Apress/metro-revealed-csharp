using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MetroGrocer {
    
    public sealed partial class BlankPage : Page {
        public BlankPage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
        }

        private void ButtonClick(object sender, RoutedEventArgs e) {

            FirstButton.Content = "Pressed";
            FirstButton.FontSize = 50;

            System.Diagnostics.Debug.WriteLine("Button Clicked: " 
                + ((Button)e.OriginalSource).Content);

        }
    }
}
