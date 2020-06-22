using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace OpenTracker.Models.Utils
{
    /// <summary>
    /// A stack that implements INotifyCollectionChanged.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the contained objects.
    /// </typeparam>
    public class ObservableStack<T> : Stack<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public virtual event NotifyCollectionChangedEventHandler CollectionChanged;
        protected virtual event PropertyChangedEventHandler PropertyChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { PropertyChanged += value; }
            remove { PropertyChanged -= value; }
        }

        /// <summary>
        /// Basic constructor
        /// </summary>
        public ObservableStack()
        {
        }

        /// <summary>
        /// Constructor that copies an existing collection.
        /// </summary>
        /// <param name="collection">
        /// An existing collection that implements IEnumerable.
        /// </param>
        public ObservableStack(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (var item in collection)
                base.Push(item);
        }

        /// <summary>
        /// Raises the CollectionChanged event.
        /// </summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Clears all members of the stack.
        /// </summary>
        public new virtual void Clear()
        {
            base.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Returns the newest member of the stack and removes it.
        /// </summary>
        /// <returns>
        /// The newest member of stack.
        /// </returns>
        public new virtual T Pop()
        {
            var item = base.Pop();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            return item;
        }

        /// <summary>
        /// Adds a new item to the stack.
        /// </summary>
        /// <param name="item">A new item to be added to the stack.</param>
        public new virtual void Push(T item)
        {
            base.Push(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }
    }
}
