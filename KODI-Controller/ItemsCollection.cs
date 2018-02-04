using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace KODI_Controller
{
    internal class ItemsCollection<T> : IList<T>, ICollection<T>, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
    {

        private List<T> _list;


        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //private void OnCollectionReset()
        //{
        //    this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        //}

        //private void OnCollectionItemChanged(NotifyCollectionChangedAction action, T item)
        //{
        //    this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, item));
        //}

        //private void OnCollectionItemsChanged(NotifyCollectionChangedAction action, IList changedItems)
        //{
        //    this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItems));
        //}


        public ItemsCollection() { _list = new List<T>(); }
        public ItemsCollection(int capacity) { _list = new List<T>(capacity); }
        public ItemsCollection(IEnumerable<T> collection) { _list = new List<T>(collection); }


        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;


        public T this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        public int Count { get { return _list.Count; } }

        public bool IsReadOnly { get { return ((IList<T>)_list).IsReadOnly; } }

        public int IndexOf(T item) { return _list.IndexOf(item); }

        public bool Contains(T item) { return _list.Contains(item); }

        public void CopyTo(T[] array, int arrayIndex) { _list.CopyTo(array, arrayIndex); }

        public IEnumerator<T> GetEnumerator() { return _list.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return _list.GetEnumerator(); }


        public void Insert(int index, T item)
        {
            if (item != null)
            {
                int priorCount = _list.Count;
                _list.Insert(index, item);
                if (priorCount != _list.Count)
                {
                    OnPropertyChanged(nameof(this.Count));
                    this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
                }
            }
        }

        public void RemoveAt(int index)
        {
            int priorCount = _list.Count;
            T item = _list[index];
            _list.RemoveAt(index);
            if (priorCount != _list.Count)
            {
                OnPropertyChanged(nameof(this.Count));
                this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            }
        }

        public void Add(T item)
        {
            if (item != null)
            {
                int priorCount = _list.Count;
                _list.Add(item);
                if (priorCount != _list.Count)
                {
                    OnPropertyChanged(nameof(this.Count));
                    this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, priorCount));
                }
            }
        }

        public void Clear()
        {
            int priorCount = _list.Count;
            _list.Clear();
            if (priorCount != _list.Count)
            {
                OnPropertyChanged(nameof(this.Count));
                this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public bool Remove(T item)
        {
            if (item != null)
            {
                int priorCount = _list.Count;
                int removedIndex = _list.IndexOf(item);
                bool removed = _list.Remove(item);
                if (priorCount != _list.Count)
                {
                    OnPropertyChanged(nameof(this.Count));
                    this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, removedIndex));
                }
                return removed;
            }
            return false;
        }

        public void AddRange(IEnumerable<T> items)
        {
            if (items != null)
            {
                List<T> listItems = new List<T>(items);
                int priorCount = _list.Count;
                _list.AddRange(listItems);
                if (priorCount != _list.Count)
                {
                    OnPropertyChanged(nameof(this.Count));
                    this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, listItems, priorCount));
                }
            }
        }

        public void InsertRange(int index, IEnumerable<T> items)
        {
            if (items != null)
            {
                int priorCount = _list.Count;
                _list.InsertRange(index, items);
                if (priorCount != _list.Count)
                {
                    OnPropertyChanged(nameof(this.Count));
                    this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (items as IList) ?? new List<T>(items), index));
                }
            }
        }

        public void RemoveRange(int index, int count)
        {
            int priorCount = _list.Count;
            T[] items = new T[count];
            _list.CopyTo(index, items, 0, count);
            _list.RemoveRange(index, count);
            if (priorCount != _list.Count)
            {
                OnPropertyChanged(nameof(this.Count));
                this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items, index));
            }
        }

    }

    internal class ItemsCollection : ItemsCollection<object>
    {
        public ItemsCollection() : base() { }
        public ItemsCollection(int capacity) : base(capacity) { }
        public ItemsCollection(IEnumerable<object> collection) : base(collection) { }
    }

}
