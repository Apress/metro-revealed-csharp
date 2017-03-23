using System;
using MetroGrocer.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MetroGrocer.Pages {

    public sealed partial class ItemDetail : Page {
        private ViewModel viewModel;

        public ItemDetail() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            viewModel = e.Parameter as ViewModel;
            this.DataContext = viewModel;

            viewModel.PropertyChanged += (sender, eventArgs) => {
                if (eventArgs.PropertyName == "SelectedItemIndex") {
                    if (viewModel.SelectedItemIndex == -1) {
                        SetItemDetail(null);
                    } else {
                        SetItemDetail(viewModel.GroceryList
                            [viewModel.SelectedItemIndex]);
                    }
                }
            };
            SetItemDetail(viewModel.GroceryList
                [viewModel.SelectedItemIndex]);
        }

        private void SetItemDetail(GroceryItem item) {
            ItemDetailName.Text = (item == null) ? "" : item.Name;
            ItemDetailQuantity.Text = (item == null) ? "" 
                : item.Quantity.ToString();

            if (item != null) {
                ItemDetailStore.SelectedItem = item.Store;
            } else {
                ItemDetailStore.SelectedIndex = -1;
            }
        }

        private void HandleItemChange(object sender, RoutedEventArgs e) {
            if (viewModel.SelectedItemIndex > -1) {

                GroceryItem selectedItem = viewModel.GroceryList
                    [viewModel.SelectedItemIndex];

                if (sender == ItemDetailName) {
                    selectedItem.Name = ItemDetailName.Text;

                } else if (sender == ItemDetailQuantity) {
                    int intVal;
                    bool parsed = Int32.TryParse(ItemDetailQuantity.Text, 
                        out intVal);
                    if (parsed) {
                        selectedItem.Quantity = intVal;
                    }
                } else if (sender == ItemDetailStore) {
                    string store = (String)((ComboBox)sender).SelectedItem;

                    if (store != null) {
                        viewModel.GroceryList
                            [viewModel.SelectedItemIndex].Store = store;
                    }
                }
            }
        }
    }
}
