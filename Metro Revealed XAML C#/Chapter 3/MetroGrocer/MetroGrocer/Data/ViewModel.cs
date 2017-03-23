using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MetroGrocer.Data {
    public class ViewModel : INotifyPropertyChanged {
        private ObservableCollection<GroceryItem> groceryList;
        private List<string> storeList;
        private int selectedItemIndex;
        private string homeZipCode;

        public ViewModel() {
            groceryList = new ObservableCollection<GroceryItem>();
            storeList = new List<string>();
            selectedItemIndex = -1;
            homeZipCode = "NY 10118";
        }

        public string HomeZipCode {
            get { return homeZipCode; }
            set { homeZipCode = value; NotifyPropertyChanged("HomeZipCode"); }
        }

        public int SelectedItemIndex {
            get { return selectedItemIndex; }
            set {
                selectedItemIndex = value; NotifyPropertyChanged("SelectedItemIndex");
            }
        }

        public ObservableCollection<GroceryItem> GroceryList {
            get {
                return groceryList;
            }
        }

        public List<string> StoreList {
            get {
                return storeList;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}