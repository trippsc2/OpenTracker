using OpenTracker.Models.Enums;
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
        IDictionary<RequirementNodeID, RequirementNode>
    {
        private static readonly ConcurrentDictionary<RequirementNodeID, RequirementNode> _dictionary =
            new ConcurrentDictionary<RequirementNodeID, RequirementNode>();

        public ICollection<RequirementNodeID> Keys =>
            ((IDictionary<RequirementNodeID, RequirementNode>)_dictionary).Keys;

        public ICollection<RequirementNode> Values =>
            ((IDictionary<RequirementNodeID, RequirementNode>)_dictionary).Values;

        public int Count =>
            ((ICollection<KeyValuePair<RequirementNodeID, RequirementNode>>)_dictionary).Count;

        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<RequirementNodeID, RequirementNode>>)_dictionary).IsReadOnly;

        public RequirementNode this[RequirementNodeID key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return ((IDictionary<RequirementNodeID, RequirementNode>)_dictionary)[key];
            }
            set => ((IDictionary<RequirementNodeID, RequirementNode>)_dictionary)[key] = value;
        }

        public event EventHandler<RequirementNodeID> NodeCreated;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data parent class.
        /// </param>
        public RequirementNodeDictionary() : base()
        {
        }

        private void Create(RequirementNodeID key)
        {
            Add(key, new RequirementNode(key));
            NodeCreated?.Invoke(this, key);
        }

        public void Add(RequirementNodeID key, RequirementNode value)
        {
            ((IDictionary<RequirementNodeID, RequirementNode>)_dictionary).Add(key, value);
        }

        public bool ContainsKey(RequirementNodeID key)
        {
            return ((IDictionary<RequirementNodeID, RequirementNode>)_dictionary).ContainsKey(key);
        }

        public bool Remove(RequirementNodeID key)
        {
            return ((IDictionary<RequirementNodeID, RequirementNode>)_dictionary).Remove(key);
        }

        public bool TryGetValue(RequirementNodeID key, out RequirementNode value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return ((IDictionary<RequirementNodeID, RequirementNode>)_dictionary).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<RequirementNodeID, RequirementNode> item)
        {
            ((ICollection<KeyValuePair<RequirementNodeID, RequirementNode>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<RequirementNodeID, RequirementNode>>)_dictionary).Clear();
        }

        public bool Contains(KeyValuePair<RequirementNodeID, RequirementNode> item)
        {
            return ((ICollection<KeyValuePair<RequirementNodeID, RequirementNode>>)_dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<RequirementNodeID, RequirementNode>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<RequirementNodeID, RequirementNode>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<RequirementNodeID, RequirementNode> item)
        {
            return ((ICollection<KeyValuePair<RequirementNodeID, RequirementNode>>)_dictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<RequirementNodeID, RequirementNode>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<RequirementNodeID, RequirementNode>>)_dictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
