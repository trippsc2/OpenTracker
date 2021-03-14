using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace OpenTracker.Utils
{
    public abstract class ViewModelCollection<TViewModel, TModel> : IObservableCollection<TViewModel>
        where TViewModel : IModelWrapper
    {
        private readonly List<TViewModel> _list;
        private readonly IObservableCollection<TModel> _model;

        public int Count => _list.Count;
        public bool IsReadOnly => ((ICollection<TViewModel>)_list).IsReadOnly;

        public TViewModel this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public ViewModelCollection(IObservableCollection<TModel> model)
        {
            _model = model;
            _list = new List<TViewModel>(from m in _model select CreateViewModel(m));
            _model.CollectionChanged += OnModelCollectionChanged;
        }

        private void OnCollectionChanged(
            NotifyCollectionChangedAction action, object? item, int index)
        {
            CollectionChanged?.Invoke(
                this, new NotifyCollectionChangedEventArgs(action, item, index));
        }

        private void OnModelCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        foreach (var item in e.NewItems)
                        {
                            var vmItem = CreateViewModel((TModel)item!);

                            if (e.NewStartingIndex != _list.Count)
                            {
                                _list.Insert(_model.IndexOf((TModel)item!), vmItem);
                            }
                            else
                            {
                                _list.Add(vmItem);
                            }

                            OnCollectionChanged(e.Action, vmItem, e.NewStartingIndex);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        foreach (var item in e.OldItems)
                        {
                            IEnumerable<TViewModel> query;

                            while ((query = from vm in _list where vm.Model == item select vm).Count() > 0)
                            {
                                var vmItem = query.First();
                                int index = _list.IndexOf(vmItem);
                                _list.Remove(vmItem);
                                OnCollectionChanged(e.Action, vmItem, index);
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    {
                        _list.Clear();
                        OnCollectionChanged(e.Action, null, e.NewStartingIndex);
                    }
                    break;
            }
        }

        public void Add(TViewModel item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(TViewModel item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(TViewModel[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TViewModel> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(TViewModel item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, TViewModel item)
        {
            _list.Insert(index, item);
        }

        public bool Remove(TViewModel item)
        {
            return _list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        protected abstract TViewModel CreateViewModel(TModel model);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
