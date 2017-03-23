using System.ComponentModel;

namespace MetroGrocer.Data {

    public class GroceryItem : INotifyPropertyChanged {
        private string name, store;
        private int quantity;

        public string Name {
            get { return name; }
            set { name = value; NotifyPropertyChanged("Name"); }
        }

        public int Quantity {
            get { return quantity; }
            set { quantity = value; NotifyPropertyChanged("Quantity"); }
        }

        public string Store {
            get { return store; }
            set { store = value; NotifyPropertyChanged("Store"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
