using OpenTracker.Models.Enums;
using OpenTracker.Models.Utils;
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

        private void Create(PrizePlacementID key)
        {
            Add(key, PrizePlacementFactory.GetPrizePlacement());
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

        public bool ContainsKey(PrizePlacementID key)
        {
            return ((IDictionary<PrizePlacementID, IPrizePlacement>)_dictionary).ContainsKey(key);
        }

        public bool Remove(PrizePlacementID key)
        {
            return ((IDictionary<PrizePlacementID, IPrizePlacement>)_dictionary).Remove(key);
        }

        public bool TryGetValue(PrizePlacementID key, out IPrizePlacement value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return ((IDictionary<PrizePlacementID, IPrizePlacement>)_dictionary).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<PrizePlacementID, IPrizePlacement> item)
        {
            ((ICollection<KeyValuePair<PrizePlacementID, IPrizePlacement>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<PrizePlacementID, IPrizePlacement>>)_dictionary).Clear();
        }

        public bool Contains(KeyValuePair<PrizePlacementID, IPrizePlacement> item)
        {
            return ((ICollection<KeyValuePair<PrizePlacementID, IPrizePlacement>>)_dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<PrizePlacementID, IPrizePlacement>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<PrizePlacementID, IPrizePlacement>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<PrizePlacementID, IPrizePlacement> item)
        {
            return ((ICollection<KeyValuePair<PrizePlacementID, IPrizePlacement>>)_dictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<PrizePlacementID, IPrizePlacement>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<PrizePlacementID, IPrizePlacement>>)_dictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
