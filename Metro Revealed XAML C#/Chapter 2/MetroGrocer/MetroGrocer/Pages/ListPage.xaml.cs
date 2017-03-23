using MetroGrocer.Data;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MetroGrocer.Pages {
    public sealed partial class ListPage : Page {
        ViewModel viewModel;

        public ListPage() {

            viewModel = new ViewModel();

            viewModel.StoreList.Add("Whole Foods");
            viewModel.StoreList.Add("Kroger");
            viewModel.StoreList.Add("Costco");
            viewModel.StoreList.Add("Walmart");

            viewModel.GroceryList.Add(new GroceryItem { Name = "Apples", 
                Quantity = 4, Store = "Whole Foods" });
            viewModel.GroceryList.Add(new GroceryItem { Name = "Hotdogs", 
                Quantity = 12, Store = "Costco" });
            viewModel.GroceryList.Add(new GroceryItem { Name = "Soda", 
                Quantity = 2, Store = "Costco" });
            viewModel.GroceryList.Add(new GroceryItem { Name = "Eggs", 
                Quantity = 12, Store = "Kroger" });

            this.InitializeComponent();

            this.DataContext = viewModel;

            ItemDetailFrame.Navigate(typeof(NoItemSelected));
            viewModel.PropertyChanged += (sender, args) => {
                if (args.PropertyName == "SelectedItemIndex") {
                    if (viewModel.SelectedItemIndex == -1) {
                        ItemDetailFrame.Navigate(typeof(NoItemSelected));
                    } else {
                        ItemDetailFrame.Navigate(typeof(ItemDetail), viewModel);
                    }
                }
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
        }

        private void ListSelectionChanged(object sender, SelectionChangedEventArgs e) {
            viewModel.SelectedItemIndex = groceryList.SelectedIndex;
        }
    }
}
