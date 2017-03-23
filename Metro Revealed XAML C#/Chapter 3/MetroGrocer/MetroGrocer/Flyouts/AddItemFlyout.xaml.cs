using System;
using System.Collections.Generic;
using MetroGrocer.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MetroGrocer.Flyouts {
    public sealed partial class AddItemFlyout : UserControl {

        public AddItemFlyout() {
            this.InitializeComponent();
        }

        public void Show(Page page, AppBar appbar, Button button) {
            AddItemPopup.IsOpen = true;
            FlyoutHelper.ShowRelativeToAppBar(AddItemPopup, page, appbar, button);
        }

        public ICollection<GroceryItem> Groceries {
            get;
            set;
        }

        private void AddButtonClick(object sender, RoutedEventArgs e) {

            ((ViewModel)DataContext).GroceryList.Add(new GroceryItem {
                Name = ItemName.Text,
                Quantity = Int32.Parse(ItemQuantity.Text),
                Store = ItemStore.SelectedItem.ToString()
            });

            AddItemPopup.IsOpen = false;
        }
    }
}
