using MetroGrocer.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MetroGrocer.Pages {
    public sealed partial class ListPage : Page {
        ViewModel viewModel;

        public ListPage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            viewModel = (ViewModel)e.Parameter;                

            ItemDetailFrame.Navigate(typeof(NoItemSelected));
            viewModel.PropertyChanged += (sender, args) => {
                if (args.PropertyName == "SelectedItemIndex") {
                    if (viewModel.SelectedItemIndex == -1) {
                        ItemDetailFrame.Navigate(typeof(NoItemSelected));
                        AppBarDoneButton.IsEnabled = false;
                    } else {
                        ItemDetailFrame.Navigate(typeof(ItemDetail), viewModel);
                        AppBarDoneButton.IsEnabled = true;
                    }
                }
            };
        }

        private void ListSelectionChanged(object sender, SelectionChangedEventArgs e) {
            viewModel.SelectedItemIndex = groceryList.SelectedIndex;
        }

        private void AppBarButtonClick(object sender, RoutedEventArgs e) {
            if (e.OriginalSource == AppBarDoneButton
                    && viewModel.SelectedItemIndex > -1) {

                viewModel.GroceryList.RemoveAt(viewModel.SelectedItemIndex);
                viewModel.SelectedItemIndex = -1;

            } else if (e.OriginalSource == AppBarZipButton) {
                HomeZipFlyout.Show(this, this.BottomAppBar, (Button)e.OriginalSource);
            } else if (e.OriginalSource == AppBarAddButton) {
                AddItemFlyout.Show(this, this.BottomAppBar, (Button)e.OriginalSource);
            }
        }
    }
}
