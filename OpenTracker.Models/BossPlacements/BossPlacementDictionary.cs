using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Utils;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This is the dictionary container for boss placements.
    /// </summary>
    public class BossPlacementDictionary : Singleton<BossPlacementDictionary>,
        IDictionary<BossPlacementID, IBossPlacement>
    {
        private static readonly ConcurrentDictionary<BossPlacementID, IBossPlacement> _dictionary =
            new ConcurrentDictionary<BossPlacementID, IBossPlacement>();

        public ICollection<BossPlacementID> Keys =>
            _dictionary.Keys;
        public ICollection<IBossPlacement> Values =>
            _dictionary.Values;
        public int Count =>
            _dictionary.Count;
        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<BossPlacementID, IBossPlacement>>)_dictionary).IsReadOnly;

        public IBossPlacement this[BossPlacementID key]
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

        public event EventHandler<BossPlacementID> BossPlacementCreated;

        /// <summary>
        /// Constructor
        /// </summary>
        public BossPlacementDictionary()
        {
        }

        /// <summary>
        /// Creates a new boss placement at the specified key.
        /// </summary>
        /// <param name="key">
        /// The boss placement ID to be created.
        /// </param>
        private void Create(BossPlacementID key)
        {
            Add(key, BossPlacementFactory.GetBossPlacement(key));
            BossPlacementCreated?.Invoke(this, key);
        }

        /// <summary>
        /// Resets the contained boss placements to their starting values.
        /// </summary>
        public void Reset()
        {
            foreach (var placement in Values)
            {
                placement.Reset();
            }
        }

        public void Add(BossPlacementID key, IBossPlacement value)
        {
            ((IDictionary<BossPlacementID, IBossPlacement>)_dictionary).Add(key, value);
        }

        public void Add(KeyValuePair<BossPlacementID, IBossPlacement> item)
        {
            ((ICollection<KeyValuePair<BossPlacementID, IBossPlacement>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<BossPlacementID, IBossPlacement> item)
        {
            return ((ICollection<KeyValuePair<BossPlacementID, IBossPlacement>>)_dictionary).Contains(
                item);
        }

        public bool ContainsKey(BossPlacementID key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<BossPlacementID, IBossPlacement>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<BossPlacementID, IBossPlacement>>)_dictionary).CopyTo(
                array, arrayIndex);
        }

        public bool Remove(BossPlacementID key)
        {
            return ((IDictionary<BossPlacementID, IBossPlacement>)_dictionary).Remove(key);
        }

        public bool Remove(KeyValuePair<BossPlacementID, IBossPlacement> item)
        {
            return ((ICollection<KeyValuePair<BossPlacementID, IBossPlacement>>)_dictionary).Remove(item);
        }

        public bool TryGetValue(BossPlacementID key, out IBossPlacement value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return _dictionary.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<BossPlacementID, IBossPlacement>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        /// <summary>
        /// Returns a dictionary of boss placement save data.
        /// </summary>
        /// <returns>
        /// A dictionary of boss placement save data.
        /// </returns>
        public Dictionary<BossPlacementID, BossPlacementSaveData> Save()
        {
            Dictionary<BossPlacementID, BossPlacementSaveData> bossPlacements =
                new Dictionary<BossPlacementID, BossPlacementSaveData>();

            foreach (var bossPlacement in Keys)
            {
                bossPlacements.Add(bossPlacement, this[bossPlacement].Save());
            }

            return bossPlacements;
        }

        /// <summary>
        /// Loads a dictionary of boss placement save data.
        /// </summary>
        public void Load(Dictionary<BossPlacementID, BossPlacementSaveData> saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            foreach (var bossPlacement in saveData.Keys)
            {
                this[bossPlacement].Load(saveData[bossPlacement]);
            }
        }
    }
}
