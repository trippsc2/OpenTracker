using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace OpenTracker.Utils;

/// <summary>
/// This is the generic class for a dictionary that implements INotifyCollectionChanged.
/// </summary>
/// <typeparam name="TKey">
/// The type of the key of the dictionary.
/// </typeparam>
/// <typeparam name="TValue">
/// The type of the value of the dictionary.
/// </typeparam>
[Serializable()]
public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged,
    INotifyPropertyChanged
    where TKey : notnull
{
    private readonly IDictionary<TKey, TValue> _dictionary;

    public ICollection<TKey> Keys => _dictionary.Keys;
    public ICollection<TValue> Values => _dictionary.Values;
    public int Count => _dictionary.Count;
    public bool IsReadOnly => _dictionary.IsReadOnly;

    public TValue this[TKey key]
    {
        get => _dictionary[key];
        set => UpdateWithNotification(key, value);
    }

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Constructor
    /// </summary>
    public ObservableDictionary() : this(new Dictionary<TKey, TValue>())
    {
    }

    /// <summary>
    /// Constructor that copies an existing dictionary.
    /// </summary>
    /// <param name="dictionary">
    /// The existing dictionary to be copied.
    /// </param>
    public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
    {
        _dictionary = dictionary;
    }

    /// <summary>
    /// Adds an item to the dictionary and raises the CollectionChanged event.
    /// </summary>
    /// <param name="key">
    /// The key of the new item.
    /// </param>
    /// <param name="value">
    /// The value of the new item.
    /// </param>
    private void AddWithNotification(TKey key, TValue value)
    {
        _dictionary.Add(key, value);

        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,
            new KeyValuePair<TKey, TValue>(key, value)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Keys)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Values)));
    }

    /// <summary>
    /// Adds an item to the dictionary and raises the CollectionChanged event.
    /// </summary>
    /// <param name="item">
    /// The item to be added.
    /// </param>
    private void AddWithNotification(KeyValuePair<TKey, TValue> item)
    {
        AddWithNotification(item.Key, item.Value);
    }

    /// <summary>
    /// Adds an item, if the key does not exist, and updates the value, if it does.
    /// </summary>
    /// <param name="key">
    /// The key of the item.
    /// </param>
    /// <param name="value">
    /// The value of the item.
    /// </param>
    private void UpdateWithNotification(TKey key, TValue value)
    {
        if (_dictionary.TryGetValue(key, out var existing))
        {
            _dictionary[key] = value;

            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Replace,
                new KeyValuePair<TKey, TValue>(key, value),
                new KeyValuePair<TKey, TValue>(key, existing)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Values)));
        }
        else
        {
            AddWithNotification(key, value);
        }
    }

    /// <summary>
    /// Removes an item from the dictionary, if the key exists, and returns whether
    /// the key existed.
    /// </summary>
    /// <param name="key">The key of the item.</param>
    /// <returns></returns>
    private bool RemoveWithNotification(TKey key)
    {
        if (_dictionary.TryGetValue(key, out var value) && _dictionary.Remove(key))
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Remove,
                new KeyValuePair<TKey, TValue>(key, value)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Keys)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Values)));

            return true;
        }

        return false;
    }

    /// <summary>
    /// Adds an item to the dictionary and raises the CollectionChanged event.
    /// </summary>
    /// <param name="key">
    /// The key of the new item.
    /// </param>
    /// <param name="value">
    /// The value of the new item.
    /// </param>
    public void Add(TKey key, TValue value)
    {
        AddWithNotification(key, value);
    }

    /// <summary>
    /// Returns whether the key exists in the dictionary.
    /// </summary>
    /// <param name="key">
    /// The key to be checked.
    /// </param>
    /// <returns>
    /// A boolean representing whether the key exists in the dictionary.
    /// </returns>
    public bool ContainsKey(TKey key)
    {
        return _dictionary.ContainsKey(key);
    }

    /// <summary>
    /// Removes an item from the dictionary, if the key exists, and returns whether
    /// the key existed.
    /// </summary>
    /// <param name="key">
    /// The key of the item to be removed.
    /// </param>
    /// <returns>
    /// A boolean representing whether the key existed.
    /// </returns>
    public bool Remove(TKey key)
    {
        return RemoveWithNotification(key);
    }

    /// <summary>
    /// Returns whether the key exists in the dictionary.  If the key exists, the value is
    /// output, as well.
    /// </summary>
    /// <param name="key">
    /// The key to be checked.
    /// </param>
    /// <param name="value">
    /// The output value, if the key exists.
    /// </param>
    /// <returns>
    /// A boolean representing whether the key exists.
    /// </returns>
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        return _dictionary.TryGetValue(key, out value);
    }

    /// <summary>
    /// Adds an item to the dictionary.
    /// </summary>
    /// <param name="item">
    /// The item to be added.
    /// </param>
    public void Add(KeyValuePair<TKey, TValue> item)
    {
        AddWithNotification(item);
    }

    /// <summary>
    /// Clears all values from the dictionary.
    /// </summary>
    public void Clear()
    {
        _dictionary.Clear();
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Keys)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Values)));
    }

    /// <summary>
    /// Returns whether the item exists in the dictionary.
    /// </summary>
    /// <param name="item">
    /// The item to be checked for in the dictionary.
    /// </param>
    /// <returns>
    /// A boolean representing whether the item exists in the dictionary.
    /// </returns>
    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return _dictionary.Contains(item);
    }

    /// <summary>
    /// Copies the values of the dictionary to a specified array starting
    /// at a specified index.
    /// </summary>
    /// <param name="array">
    /// The array to receive the values.
    /// </param>
    /// <param name="arrayIndex">
    /// The index at which to start copying.
    /// </param>
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        _dictionary.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Removes an item from the dictionary, if it exists, and returns whether
    /// the item existed.
    /// </summary>
    /// <param name="item">
    /// The item to be removed.
    /// </param>
    /// <returns>
    /// A boolean representing whether the item existed.
    /// </returns>
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        return RemoveWithNotification(item.Key);
    }

    /// <summary>
    /// Returns an enumerator of the dictionary.
    /// </summary>
    /// <returns>
    /// An enumerator of the dictionary.
    /// </returns>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator of the dictionary.
    /// </summary>
    /// <returns>
    /// An enumerator of the dictionary.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }
}