using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Utils;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    /// This is the dictionary container for prize placements.
    /// </summary>
    public class PrizePlacementDictionary : Singleton<PrizePlacementDictionary>,
        IDictionary<PrizePlacementID, IPrizePlacement>
    {
        private static readonly ConcurrentDictionary<PrizePlacementID, IPrizePlacement> _dictionary =
            new ConcurrentDictionary<PrizePlacementID, IPrizePlacement>();

        public ICollection<PrizePlacementID> Keys =>
            ((IDictionary<PrizePlacementID, IPrizePlacement>)_dictionary).Keys;
        public ICollection<IPrizePlacement> Values =>
            ((IDictionary<PrizePlacementID, IPrizePlacement>)_dictionary).Values;
        public int Count =>
            ((ICollection<KeyValuePair<PrizePlacementID, IPrizePlacement>>)_dictionary).Count;
        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<PrizePlacementID, IPrizePlacement>>)_dictionary).IsReadOnly;

        public IPrizePlacement this[PrizePlacementID key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return ((IDictionary<PrizePlacementID, IPrizePlacement>)_dictionary)[key];
            }
            set => ((IDictionary<PrizePlacementID, IPrizePlacement>)_dictionary)[key] = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public PrizePlacementDictionary() : base()
        {
        }

        /// <summary>
        /// Creates a new prize placement for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        private void Create(PrizePlacementID key)
        {
            Add(key, PrizePlacementFactory.GetPrizePlacement(key));
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

        public void Add(PrizePlacementID key, IPrizePlacement value)
        {
            ((IDictionary<PrizePlacementID, IPrizePlacement>)_dictionary).Add(key, value);
        }

        public void Add(KeyValuePair<PrizePlacementID, IPrizePlacement> item)
        {
            ((ICollection<KeyValuePair<PrizePlacementID, IPrizePlacement>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<PrizePlacementID, IPrizePlacement> item)
        {
            return ((ICollection<KeyValuePair<PrizePlacementID, IPrizePlacement>>)_dictionary).Contains(item);
        }

        public bool ContainsKey(PrizePlacementID key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<PrizePlacementID, IPrizePlacement>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<PrizePlacementID, IPrizePlacement>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(PrizePlacementID key)
        {
            return ((IDictionary<PrizePlacementID, IPrizePlacement>)_dictionary).Remove(key);
        }

        public bool Remove(KeyValuePair<PrizePlacementID, IPrizePlacement> item)
        {
            return ((ICollection<KeyValuePair<PrizePlacementID, IPrizePlacement>>)_dictionary).Remove(item);
        }

        public bool TryGetValue(PrizePlacementID key, out IPrizePlacement value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return _dictionary.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<PrizePlacementID, IPrizePlacement>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        /// <summary>
        /// Returns a dictionary of prize placement save data.
        /// </summary>
        /// <returns>
        /// A dictionary of prize placement save data.
        /// </returns>
        public Dictionary<PrizePlacementID, PrizePlacementSaveData> Save()
        {
            Dictionary<PrizePlacementID, PrizePlacementSaveData> prizePlacements =
                new Dictionary<PrizePlacementID, PrizePlacementSaveData>();

            foreach (var prizePlacement in Keys)
            {
                prizePlacements.Add(prizePlacement, this[prizePlacement].Save());
            }

            return prizePlacements;
        }

        /// <summary>
        /// Loads a dictionary of prize placement save data.
        /// </summary>
        public void Load(Dictionary<PrizePlacementID, PrizePlacementSaveData> saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            foreach (var prizePlacement in saveData.Keys)
            {
                this[prizePlacement].Load(saveData[prizePlacement]);
            }
        }
    }
}
