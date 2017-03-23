using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MetroGrocer.Data {
    public class ViewModel : INotifyPropertyChanged {
        private ObservableCollection<GroceryItem> groceryList;
        private List<string> storeList;
        private int selectedItemIndex;
        private string homeZipCode;
        private string location;

        public ViewModel() {
            groceryList = new ObservableCollection<GroceryItem>();
            storeList = new List<string>();
            selectedItemIndex = -1;
            homeZipCode = "NY 10118";
            location = "Unknown";
        }

        public void SearchAndSelect(string searchTerm) {
            int selIndex = -1;
            for (int i = 0; i < GroceryList.Count; i++) {
                if (GroceryList[i].Name.ToLower().Contains(searchTerm.ToLower())) {
                    selIndex = i;
                    break;
                }
            }
            SelectedItemIndex = selIndex;
        }

        public string Location {
            get { return location; }
            set { location = value; NotifyPropertyChanged("Location"); }
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