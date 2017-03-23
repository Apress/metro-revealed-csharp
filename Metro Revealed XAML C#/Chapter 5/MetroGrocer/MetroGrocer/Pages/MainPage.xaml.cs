using System;
using MetroGrocer.Data;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;
using System.Collections.Generic;

namespace MetroGrocer.Pages {

    public sealed partial class MainPage : Page {
        private ViewModel viewModel;

        public MainPage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            viewModel = (ViewModel)e.Parameter;

            this.DataContext = viewModel;

            MainFrame.Navigate(typeof(ListPage), viewModel);

            //viewModel.GroceryList.CollectionChanged += (sender, args) => {
            //    UpdateTile();
            //    UpdateBadge();
            //};

            //UpdateTile();
            //UpdateBadge();
        }

        private void UpdateBadge() {

            int itemCount = viewModel.GroceryList.Count;

            BadgeTemplateType templateType = itemCount > 3 
                ? BadgeTemplateType.BadgeGlyph : BadgeTemplateType.BadgeNumber;

            XmlDocument badgeXml = BadgeUpdateManager.GetTemplateContent(templateType);
            ((XmlElement)badgeXml.GetElementsByTagName("badge")[0]).SetAttribute("value",
                (itemCount > 3) ? "alert" : itemCount.ToString());

            for (int i = 0; i < 5; i++) {
                BadgeUpdateManager.CreateBadgeUpdaterForApplication()
                    .Update(new BadgeNotification(badgeXml));
            }
        }

        private void UpdateTile() {

            int storeCount = 0;
            List<string> storeNames = new List<string>();

            for (int i = 0; i < viewModel.GroceryList.Count; i++) {
                if (!storeNames.Contains(viewModel.GroceryList[i].Store)) {
                    storeCount++;
                    storeNames.Add(viewModel.GroceryList[i].Store);
                }
            }

            XmlDocument narrowTileXml = TileUpdateManager
                .GetTemplateContent(TileTemplateType.TileSquareText03);
            XmlDocument wideTileXml = TileUpdateManager
                .GetTemplateContent(TileTemplateType.TileWideBlockAndText01);

            XmlNodeList narrowTextNodes = narrowTileXml.GetElementsByTagName("text");
            XmlNodeList wideTextNodes = wideTileXml.GetElementsByTagName("text");

            for (int i = 0; i < narrowTextNodes.Length 
                && i < viewModel.GroceryList.Count; i++) {

                GroceryItem item = viewModel.GroceryList[i];

                narrowTextNodes[i].InnerText = item.Name;
                wideTextNodes[i].InnerText = String.Format("{0} ({1})", item.Name, 
                    item.Store);
            }

            wideTextNodes[4].InnerText = storeCount.ToString();
            wideTextNodes[5].InnerText = "Stores";

            var wideBindingElement = wideTileXml.GetElementsByTagName("binding")[0];
            var importedNode = narrowTileXml.ImportNode(wideBindingElement, true);
            narrowTileXml.GetElementsByTagName("visual")[0].AppendChild(importedNode);
            
            for (int i = 0; i < 5; i++) {
                TileUpdateManager.CreateTileUpdaterForApplication()
                    .Update(new TileNotification(narrowTileXml));
            }
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
