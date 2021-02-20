using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace OpenTracker.Utils
{
    /// <summary>
    /// This class wraps a dictionary with creation logic.
    /// </summary>
    /// <typeparam name="TKey">
    /// The type of the dictionary key.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// The type of the dictionary value.
    /// </typeparam>
    public abstract class LazyDictionary<TKey, TValue> : IDictionary<TKey, TValue>,
        ICollection<KeyValuePair<TKey, TValue>>
        where TKey : notnull
    {
        private readonly IDictionary<TKey, TValue> _dictionary;

        public TValue this[TKey key]
        {
            get
            {
                if (ContainsKey(key))
                {
                    return _dictionary[key];
                }

                return CreateAndNotify(key);
            }
            set => _dictionary[key] = value;
        }

        public ICollection<TKey> Keys =>
            _dictionary.Keys;
        public ICollection<TValue> Values =>
            _dictionary.Values;
        public int Count =>
            _dictionary.Count;
        public bool IsReadOnly =>
            _dictionary.IsReadOnly;

        public event EventHandler<KeyValuePair<TKey, TValue>>? ItemCreated;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary to be wrapped.
        /// </param>
        public LazyDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        /// <summary>
        /// Creates a new value for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key to be created and returned.</param>
        /// <returns>
        /// The newly created value.
        /// </returns>
        protected abstract TValue Create(TKey key);

        /// <summary>
        /// Creates and invokes the ItemCreated event.
        /// </summary>
        /// <param name="key">
        /// The key to be created and returned.</param>
        /// <returns>
        /// The newly created value.
        /// </returns>
        protected TValue CreateAndNotify(TKey key)
        {
            var newValue = Create(key);
            Add(key, newValue);
            ItemCreated?.Invoke(this, new KeyValuePair<TKey, TValue>(key, newValue));

            return newValue;
        }

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _dictionary.Add(item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            return _dictionary.Remove(key);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Remove(item);
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (!_dictionary.TryGetValue(key, out value))
            {
                value = CreateAndNotify(key);
            }

            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
