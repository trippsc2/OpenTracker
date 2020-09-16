using OpenTracker.Models.Utils;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This is the dictionary containing all requirement node data
    /// </summary>
    public class RequirementNodeDictionary : Singleton<RequirementNodeDictionary>,
        IDictionary<RequirementNodeID, IRequirementNode>
    {
        private static readonly ConcurrentDictionary<RequirementNodeID, IRequirementNode> _dictionary =
            new ConcurrentDictionary<RequirementNodeID, IRequirementNode>();

        public ICollection<RequirementNodeID> Keys =>
            ((IDictionary<RequirementNodeID, IRequirementNode>)_dictionary).Keys;
        public ICollection<IRequirementNode> Values =>
            ((IDictionary<RequirementNodeID, IRequirementNode>)_dictionary).Values;
        public int Count =>
            ((ICollection<KeyValuePair<RequirementNodeID, IRequirementNode>>)_dictionary).Count;
        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<RequirementNodeID, IRequirementNode>>)_dictionary).IsReadOnly;

        public IRequirementNode this[RequirementNodeID key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return ((IDictionary<RequirementNodeID, IRequirementNode>)_dictionary)[key];
            }
            set => ((IDictionary<RequirementNodeID, IRequirementNode>)_dictionary)[key] = value;
        }

        public event EventHandler<KeyValuePair<RequirementNodeID, IRequirementNode>> NodeCreated;

        /// <summary>
        /// Creates a new requirement node for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        private void Create(RequirementNodeID key)
        {
            Add(key, RequirementNodeFactory.GetRequirementNode(key));
            NodeCreated?.Invoke(
                this, new KeyValuePair<RequirementNodeID, IRequirementNode>(key, this[key]));
        }

        /// <summary>
        /// Resets all nodes' AlwaysAccessible property for testing purposes.
        /// </summary>
        public void Reset()
        {
            foreach (var node in Values)
            {
                node.Reset();
            }
        }

        public void Add(RequirementNodeID key, IRequirementNode value)
        {
            ((IDictionary<RequirementNodeID, IRequirementNode>)_dictionary).Add(key, value);
        }

        public void Add(KeyValuePair<RequirementNodeID, IRequirementNode> item)
        {
            ((ICollection<KeyValuePair<RequirementNodeID, IRequirementNode>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<RequirementNodeID, IRequirementNode> item)
        {
            return ((ICollection<KeyValuePair<RequirementNodeID, IRequirementNode>>)_dictionary).Contains(item);
        }

        public bool ContainsKey(RequirementNodeID key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<RequirementNodeID, IRequirementNode>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<RequirementNodeID, IRequirementNode>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(RequirementNodeID key)
        {
            return ((IDictionary<RequirementNodeID, IRequirementNode>)_dictionary).Remove(key);
        }

        public bool Remove(KeyValuePair<RequirementNodeID, IRequirementNode> item)
        {
            return ((ICollection<KeyValuePair<RequirementNodeID, IRequirementNode>>)_dictionary).Remove(item);
        }

        public bool TryGetValue(RequirementNodeID key, out IRequirementNode value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return _dictionary.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<RequirementNodeID, IRequirementNode>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }
    }
}
