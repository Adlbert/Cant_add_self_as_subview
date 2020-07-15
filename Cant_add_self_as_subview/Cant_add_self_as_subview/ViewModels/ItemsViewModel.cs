using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Cant_add_self_as_subview.Models;
using Cant_add_self_as_subview.Views;
using System.Collections.Generic;
using Cant_add_self_as_subview.Helpers;
using System.Threading;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Cant_add_self_as_subview.ViewModels {
    public class ItemsViewModel : BaseViewModel {
        public CustomObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel() {
            Title = "Browse";
            Items = new CustomObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) => {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand() {
            IsBusy = true;

            try {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items) {
                    Items.Add(item);
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
            }
            finally {
                IsBusy = false;
            }
        }

        private ICommand _loadNext;
        public ICommand LoadNext {
            get {
                return _loadNext
                       ?? (_loadNext = new RelayCommand(
                           async () => {
                               await Task.Run(async () => {
                                   var newCollection = new CustomObservableCollection<Item>(Items);
                                   var newItems = new List<Item>() {
                                        new Item(){Id = Guid.NewGuid().ToString(), Text=(Items.Count+1).ToString() , Description = "This is an item description."},
                                        new Item(){Id = Guid.NewGuid().ToString(), Text=(Items.Count+2).ToString() , Description = "This is an item description."},
                                        new Item(){Id = Guid.NewGuid().ToString(), Text=(Items.Count+3).ToString() , Description = "This is an item description."},
                                        new Item(){Id = Guid.NewGuid().ToString(), Text=(Items.Count+4).ToString() , Description = "This is an item description."},
                                    };
                                   await Task.Delay(500);
                                   Items.AddRange(newItems);
                               });
                           }));
            }
        }
    }
}