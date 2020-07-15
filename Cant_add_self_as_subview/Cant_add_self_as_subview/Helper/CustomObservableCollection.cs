using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Cant_add_self_as_subview.Helpers {
    public class CustomObservableCollection<T> : ObservableCollection<T> {
        public CustomObservableCollection() {
        }

        public CustomObservableCollection(IEnumerable<T> items) : this() {
            foreach (var item in items)
                this.Add(item);
        }
        public void ReportItemChange(T item) {
            NotifyCollectionChangedEventArgs args =
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Replace,
                    item,
                    item,
                    IndexOf(item));
            OnCollectionChanged(args);
        }

        public void AddRange(IEnumerable<T> range) {
            if (range == null)
                throw new ArgumentNullException("range");

            var items = range.ToList();
            int index = Items.Count;
            foreach (T item in items)
                Items.Add(item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items, index));
        }

        public void Replace(int index, T item) {
            RemoveAt(index);
            Insert(index, item);
        }
    }
}
