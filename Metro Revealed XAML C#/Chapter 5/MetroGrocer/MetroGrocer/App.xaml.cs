using System.Threading;
using System.Threading.Tasks;
using MetroGrocer.Data;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MetroGrocer {    
    sealed partial class App : Application {
        private ViewModel viewModel;
        private Task locationTask;
        private CancellationTokenSource locationTokenSource;
        private Frame rootFrame;

        public App() {
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

            this.Suspending += OnSuspending;
            this.Resuming += OnResuming;

            StartLocationTracking();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args) {
            // Create a Frame to act navigation context and navigate to the first page
            rootFrame = new Frame();
            rootFrame.Navigate(typeof(Pages.MainPage), viewModel);

            // Place the frame in the current Window and ensure that it is active
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }

        protected override void OnSearchActivated(SearchActivatedEventArgs args) {
            viewModel.SearchAndSelect(args.QueryText);
        }

        private void OnResuming(object sender, object e) {
            viewModel.Location = "Unknown";
            StartLocationTracking();
        }

        void OnSuspending(object sender, SuspendingEventArgs e) {
            StopLocationTracking();
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
            locationTask.Wait();
            deferral.Complete();
        }

        private void StartLocationTracking() {
            locationTokenSource = new CancellationTokenSource();
            CancellationToken token = locationTokenSource.Token;

            locationTask = new Task(async () => {
                while (!token.IsCancellationRequested) {
                    string locationMsg = await Location.TrackLocation();

                    rootFrame.Dispatcher.Invoke(CoreDispatcherPriority.Normal,
                        (sender, context) => {
                            viewModel.Location = locationMsg;
                        }, this, locationMsg);   
                    token.WaitHandle.WaitOne(5000);
                }
            });
            locationTask.Start();
        }

        private void StopLocationTracking() {
            locationTokenSource.Cancel();
        }
    }
}
