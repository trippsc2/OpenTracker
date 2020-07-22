using OpenTracker.Models.Utils;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the dictionary containing requirement data
    /// </summary>
    public class RequirementDictionary : Singleton<RequirementDictionary>,
        IDictionary<RequirementType, IRequirement>
    {
        private static readonly ConcurrentDictionary<RequirementType, IRequirement> _dictionary =
            new ConcurrentDictionary<RequirementType, IRequirement>();

        public ICollection<RequirementType> Keys =>
            ((IDictionary<RequirementType, IRequirement>)_dictionary).Keys;
        public ICollection<IRequirement> Values =>
            ((IDictionary<RequirementType, IRequirement>)_dictionary).Values;
        public int Count =>
            ((ICollection<KeyValuePair<RequirementType, IRequirement>>)_dictionary).Count;
        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<RequirementType, IRequirement>>)_dictionary).IsReadOnly;

        public IRequirement this[RequirementType key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return ((IDictionary<RequirementType, IRequirement>)_dictionary)[key];
            }
            set => ((IDictionary<RequirementType, IRequirement>)_dictionary)[key] = value;
        }

        public event EventHandler<RequirementType> RequirementCreated;

        /// <summary>
        /// Constructor
        /// </summary>
        public RequirementDictionary() : base()
        {
        }

        /// <summary>
        /// Create a new requirement for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        private void Create(RequirementType key)
        {
            Add(key, RequirementFactory.GetRequirement(key));
            RequirementCreated?.Invoke(this, key);
        }

        public void Add(RequirementType key, IRequirement value)
        {
            ((IDictionary<RequirementType, IRequirement>)_dictionary).Add(key, value);
        }

        public void Add(KeyValuePair<RequirementType, IRequirement> item)
        {
            ((ICollection<KeyValuePair<RequirementType, IRequirement>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<RequirementType, IRequirement> item)
        {
            return ((ICollection<KeyValuePair<RequirementType, IRequirement>>)_dictionary).Contains(item);
        }

        public bool ContainsKey(RequirementType key)
        {
            return ((IDictionary<RequirementType, IRequirement>)_dictionary).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<RequirementType, IRequirement>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<RequirementType, IRequirement>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(RequirementType key)
        {
            return ((IDictionary<RequirementType, IRequirement>)_dictionary).Remove(key);
        }

        public bool Remove(KeyValuePair<RequirementType, IRequirement> item)
        {
            return ((ICollection<KeyValuePair<RequirementType, IRequirement>>)_dictionary).Remove(item);
        }

        public bool TryGetValue(RequirementType key, out IRequirement value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return ((IDictionary<RequirementType, IRequirement>)_dictionary).TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<RequirementType, IRequirement>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }
    }
}
