using OpenTracker.Models.Dungeons;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTracker.Models.DungeonNodes
{
    /// <summary>
    /// This is the class containing the dictionary of dungeon nodes.
    /// </summary>
    public class DungeonNodeDictionary : IDictionary<DungeonNodeID, IDungeonNode>
    {
        private readonly MutableDungeon _dungeonData;
        private readonly IDungeon _dungeon;
        private readonly ConcurrentDictionary<DungeonNodeID, IDungeonNode> _dictionary =
            new ConcurrentDictionary<DungeonNodeID, IDungeonNode>();

        public ICollection<DungeonNodeID> Keys =>
            ((IDictionary<DungeonNodeID, IDungeonNode>)_dictionary).Keys;
        public ICollection<IDungeonNode> Values =>
            ((IDictionary<DungeonNodeID, IDungeonNode>)_dictionary).Values;
        public int Count =>
            ((ICollection<KeyValuePair<DungeonNodeID, IDungeonNode>>)_dictionary).Count;
        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<DungeonNodeID, IDungeonNode>>)_dictionary).IsReadOnly;

        public IDungeonNode this[DungeonNodeID key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return ((IDictionary<DungeonNodeID, IDungeonNode>)_dictionary)[key];
            }
            set => ((IDictionary<DungeonNodeID, IDungeonNode>)_dictionary)[key] = value;
        }

        public event EventHandler<KeyValuePair<DungeonNodeID, IDungeonNode>> NodeCreated;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeonData">
        /// The dungeon data parent class.
        /// </param>
        /// <param name="dungeon">
        /// The dungeon parent class.
        /// </param>
        public DungeonNodeDictionary(MutableDungeon dungeonData, IDungeon dungeon)
        {
            _dungeonData = dungeonData ?? throw new ArgumentNullException(nameof(dungeonData));
            _dungeon = dungeon ?? throw new ArgumentNullException(nameof(dungeon));
        }

        /// <summary>
        /// Creates a dungeon node for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        private void Create(DungeonNodeID key)
        {
            Add(key, DungeonNodeFactory.GetDungeonNode(key, _dungeonData, _dungeon));
            NodeCreated?.Invoke(
                this, new KeyValuePair<DungeonNodeID, IDungeonNode>(key, this[key]));
        }

        public void Add(DungeonNodeID key, IDungeonNode value)
        {
            ((IDictionary<DungeonNodeID, IDungeonNode>)_dictionary).Add(key, value);
        }

        public void Add(KeyValuePair<DungeonNodeID, IDungeonNode> item)
        {
            ((ICollection<KeyValuePair<DungeonNodeID, IDungeonNode>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<DungeonNodeID, IDungeonNode>>)_dictionary).Clear();
        }

        public bool Contains(KeyValuePair<DungeonNodeID, IDungeonNode> item)
        {
            return ((ICollection<KeyValuePair<DungeonNodeID, IDungeonNode>>)_dictionary).Contains(item);
        }

        public bool ContainsKey(DungeonNodeID key)
        {
            return ((IDictionary<DungeonNodeID, IDungeonNode>)_dictionary).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<DungeonNodeID, IDungeonNode>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<DungeonNodeID, IDungeonNode>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<DungeonNodeID, IDungeonNode>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<DungeonNodeID, IDungeonNode>>)_dictionary).GetEnumerator();
        }

        public bool Remove(DungeonNodeID key)
        {
            return ((IDictionary<DungeonNodeID, IDungeonNode>)_dictionary).Remove(key);
        }

        public bool Remove(KeyValuePair<DungeonNodeID, IDungeonNode> item)
        {
            return ((ICollection<KeyValuePair<DungeonNodeID, IDungeonNode>>)_dictionary).Remove(item);
        }

        public bool TryGetValue(DungeonNodeID key, out IDungeonNode value)
        {
            return ((IDictionary<DungeonNodeID, IDungeonNode>)_dictionary).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
