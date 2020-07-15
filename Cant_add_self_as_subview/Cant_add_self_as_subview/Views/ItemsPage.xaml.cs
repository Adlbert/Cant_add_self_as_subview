using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Cant_add_self_as_subview.Models;
using Cant_add_self_as_subview.Views;
using Cant_add_self_as_subview.ViewModels;

namespace Cant_add_self_as_subview.Views {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage {
        ItemsViewModel viewModel;

        public ItemsPage() {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
            ItemsCollectionView.ItemAppearing += ItemsCollectionView_ItemAppearing;
        }

        private void ItemsCollectionView_ItemAppearing(object sender, ItemVisibilityEventArgs e) {
            Item lastItem = viewModel.Items[viewModel.Items.Count - 1];
            if (e.Item is Item item && item.Id == lastItem.Id)
                viewModel.LoadNext.Execute(null);
        }

        async void OnItemSelected(object sender, EventArgs args) {
            var layout = (BindableObject)sender;
            var item = (Item)layout.BindingContext;
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        }

        async void AddItem_Clicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }

        private void lvDetayButton_Clicked(object sender, EventArgs e) {

        }
    }
}