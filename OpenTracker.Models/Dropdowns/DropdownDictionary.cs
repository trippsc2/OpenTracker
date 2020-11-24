using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Utils;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace OpenTracker.Models.Dropdowns
{
    public class DropdownDictionary : Singleton<DropdownDictionary>, 
        IDictionary<DropdownID, IDropdown>, ISaveable<Dictionary<DropdownID, DropdownSaveData>>
    {
        private readonly ConcurrentDictionary<DropdownID, IDropdown> _dictionary =
            new ConcurrentDictionary<DropdownID, IDropdown>();

        public IDropdown this[DropdownID key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return _dictionary[key];
            }
            set => _dictionary[key] = value;
        }

        public ICollection<DropdownID> Keys =>
            _dictionary.Keys;
        public ICollection<IDropdown> Values =>
            _dictionary.Values;
        public int Count =>
            ((ICollection<KeyValuePair<DropdownID, IDropdown>>)_dictionary).Count;
        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<DropdownID, IDropdown>>)_dictionary).IsReadOnly;

        private void Create(DropdownID key)
        {
            Add(key, DropdownFactory.GetDropdown(key));
        }

        public void Add(DropdownID key, IDropdown value)
        {
            ((IDictionary<DropdownID, IDropdown>)_dictionary).Add(key, value);
        }

        public void Add(KeyValuePair<DropdownID, IDropdown> item)
        {
            ((ICollection<KeyValuePair<DropdownID, IDropdown>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<DropdownID, IDropdown>>)_dictionary).Clear();
        }

        public bool Contains(KeyValuePair<DropdownID, IDropdown> item)
        {
            return ((ICollection<KeyValuePair<DropdownID, IDropdown>>)_dictionary).Contains(item);
        }

        public bool ContainsKey(DropdownID key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<DropdownID, IDropdown>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<DropdownID, IDropdown>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<DropdownID, IDropdown>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<DropdownID, IDropdown>>)_dictionary).GetEnumerator();
        }

        public bool Remove(DropdownID key)
        {
            return ((IDictionary<DropdownID, IDropdown>)_dictionary).Remove(key);
        }

        public bool Remove(KeyValuePair<DropdownID, IDropdown> item)
        {
            return ((ICollection<KeyValuePair<DropdownID, IDropdown>>)_dictionary).Remove(item);
        }

        public bool TryGetValue(DropdownID key, [MaybeNullWhen(false)] out IDropdown value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }

        /// <summary>
        /// Returns a dictionary of dropdown save data.
        /// </summary>
        /// <returns>
        /// A dictionary of dropdown save data.
        /// </returns>
        public Dictionary<DropdownID, DropdownSaveData> Save()
        {
            var items = new Dictionary<DropdownID, DropdownSaveData>();

            foreach (var type in Keys)
            {
                items.Add(type, this[type].Save());
            }

            return items;
        }

        /// <summary>
        /// Loads a dictionary of dropdown save data.
        /// </summary>
        /// <param name="saveData">
        /// The save data to be loaded.
        /// </param>
        public void Load(Dictionary<DropdownID, DropdownSaveData> saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            foreach (var item in saveData.Keys)
            {
                this[item].Load(saveData[item]);
            }
        }
    }
}
