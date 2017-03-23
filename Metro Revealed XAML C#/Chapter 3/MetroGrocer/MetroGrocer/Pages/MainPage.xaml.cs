using System;
using MetroGrocer.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace MetroGrocer.Pages {

    public sealed partial class MainPage : Page {
        private ViewModel viewModel;

        public MainPage() {
            this.InitializeComponent();

            viewModel = new ViewModel();

            viewModel.StoreList.Add("Whole Foods");
            viewModel.StoreList.Add("Kroger");
            viewModel.StoreList.Add("Costco");
            viewModel.StoreList.Add("Walmart");

            viewModel.GroceryList.Add(new GroceryItem {
                Name = "Apples",
                Quantity = 4, Store = "Whole Foods"
            });
            viewModel.GroceryList.Add(new GroceryItem {
                Name = "Hotdogs",
                Quantity = 12, Store = "Costco"
            });
            viewModel.GroceryList.Add(new GroceryItem {
                Name = "Soda",
                Quantity = 2, Store = "Costco"
            });
            viewModel.GroceryList.Add(new GroceryItem {
                Name = "Eggs",
                Quantity = 12, Store = "Kroger"
            });

            this.DataContext = viewModel;

            MainFrame.Navigate(typeof(ListPage), viewModel);
        }



        protected override void OnNavigatedTo(NavigationEventArgs e) {
        }

        private void NavBarButtonPress(object sender, RoutedEventArgs e) {
            Boolean isListView = (ToggleButton)sender == ListViewButton;
            MainFrame.Navigate(isListView ? typeof(ListPage)
                : typeof(DetailPage), viewModel);
            ListViewButton.IsChecked = isListView;
            DetailViewButton.IsChecked = !isListView;
        }
    }
}
