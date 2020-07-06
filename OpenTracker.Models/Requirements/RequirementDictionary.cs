using OpenTracker.Models.Enums;
using OpenTracker.Models.Utils;
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data parent class.
        /// </param>
        public RequirementDictionary() : base()
        {
        }

        private void Create(RequirementType key)
        {
            Add(key, RequirementFactory.GetRequirement(key));
        }

        public void Add(RequirementType key, IRequirement value)
        {
            ((IDictionary<RequirementType, IRequirement>)_dictionary).Add(key, value);
        }

        public bool ContainsKey(RequirementType key)
        {
            return ((IDictionary<RequirementType, IRequirement>)_dictionary).ContainsKey(key);
        }

        public bool Remove(RequirementType key)
        {
            return ((IDictionary<RequirementType, IRequirement>)_dictionary).Remove(key);
        }

        public bool TryGetValue(RequirementType key, out IRequirement value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return ((IDictionary<RequirementType, IRequirement>)_dictionary).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<RequirementType, IRequirement> item)
        {
            ((ICollection<KeyValuePair<RequirementType, IRequirement>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<RequirementType, IRequirement>>)_dictionary).Clear();
        }

        public bool Contains(KeyValuePair<RequirementType, IRequirement> item)
        {
            return ((ICollection<KeyValuePair<RequirementType, IRequirement>>)_dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<RequirementType, IRequirement>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<RequirementType, IRequirement>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<RequirementType, IRequirement> item)
        {
            return ((ICollection<KeyValuePair<RequirementType, IRequirement>>)_dictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<RequirementType, IRequirement>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<RequirementType, IRequirement>>)_dictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
