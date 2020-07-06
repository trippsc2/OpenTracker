using OpenTracker.Models.Enums;
using OpenTracker.Models.Utils;
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
            ((IDictionary<BossPlacementID, IBossPlacement>)_dictionary).Keys;

        public ICollection<IBossPlacement> Values =>
            ((IDictionary<BossPlacementID, IBossPlacement>)_dictionary).Values;

        public int Count =>
            ((ICollection<KeyValuePair<BossPlacementID, IBossPlacement>>)_dictionary).Count;

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

                return ((IDictionary<BossPlacementID, IBossPlacement>)_dictionary)[key];
            }
            set => ((IDictionary<BossPlacementID, IBossPlacement>)_dictionary)[key] = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data parent class.
        /// </param>
        public BossPlacementDictionary() : base()
        {
        }

        private void Create(BossPlacementID key)
        {
            Add(key, BossPlacementFactory.GetBossPlacement(key));
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

        public bool ContainsKey(BossPlacementID key)
        {
            return ((IDictionary<BossPlacementID, IBossPlacement>)_dictionary).ContainsKey(key);
        }

        public bool Remove(BossPlacementID key)
        {
            return ((IDictionary<BossPlacementID, IBossPlacement>)_dictionary).Remove(key);
        }

        public bool TryGetValue(BossPlacementID key, out IBossPlacement value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return ((IDictionary<BossPlacementID, IBossPlacement>)_dictionary).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<BossPlacementID, IBossPlacement> item)
        {
            ((ICollection<KeyValuePair<BossPlacementID, IBossPlacement>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<BossPlacementID, IBossPlacement>>)_dictionary).Clear();
        }

        public bool Contains(KeyValuePair<BossPlacementID, IBossPlacement> item)
        {
            return ((ICollection<KeyValuePair<BossPlacementID, IBossPlacement>>)_dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<BossPlacementID, IBossPlacement>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<BossPlacementID, IBossPlacement>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<BossPlacementID, IBossPlacement> item)
        {
            return ((ICollection<KeyValuePair<BossPlacementID, IBossPlacement>>)_dictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<BossPlacementID, IBossPlacement>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<BossPlacementID, IBossPlacement>>)_dictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
