using OpenTracker.Models.Enums;
using OpenTracker.Models.Utils;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTracker.Models.SequenceBreaks
{
    public class SequenceBreakDictionary : Singleton<SequenceBreakDictionary>,
        IDictionary<SequenceBreakType, SequenceBreak>
    {
        private static readonly ConcurrentDictionary<SequenceBreakType, SequenceBreak> _dictionary =
            new ConcurrentDictionary<SequenceBreakType, SequenceBreak>();

        public ICollection<SequenceBreakType> Keys =>
            ((IDictionary<SequenceBreakType, SequenceBreak>)_dictionary).Keys;

        public ICollection<SequenceBreak> Values =>
            ((IDictionary<SequenceBreakType, SequenceBreak>)_dictionary).Values;

        public int Count =>
            ((ICollection<KeyValuePair<SequenceBreakType, SequenceBreak>>)_dictionary).Count;

        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<SequenceBreakType, SequenceBreak>>)_dictionary).IsReadOnly;

        public SequenceBreak this[SequenceBreakType key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return ((IDictionary<SequenceBreakType, SequenceBreak>)_dictionary)[key];
            }
            set => ((IDictionary<SequenceBreakType, SequenceBreak>)_dictionary)[key] = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SequenceBreakDictionary()
        {
        }

        private void Create(SequenceBreakType key)
        {
            Add(key, new SequenceBreak());
        }

        public void Add(SequenceBreakType key, SequenceBreak value)
        {
            ((IDictionary<SequenceBreakType, SequenceBreak>)_dictionary).Add(key, value);
        }

        public bool ContainsKey(SequenceBreakType key)
        {
            return ((IDictionary<SequenceBreakType, SequenceBreak>)_dictionary).ContainsKey(key);
        }

        public bool Remove(SequenceBreakType key)
        {
            return ((IDictionary<SequenceBreakType, SequenceBreak>)_dictionary).Remove(key);
        }

        public bool TryGetValue(SequenceBreakType key, out SequenceBreak value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return ((IDictionary<SequenceBreakType, SequenceBreak>)_dictionary).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<SequenceBreakType, SequenceBreak> item)
        {
            ((ICollection<KeyValuePair<SequenceBreakType, SequenceBreak>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<SequenceBreakType, SequenceBreak>>)_dictionary).Clear();
        }

        public bool Contains(KeyValuePair<SequenceBreakType, SequenceBreak> item)
        {
            return ((ICollection<KeyValuePair<SequenceBreakType, SequenceBreak>>)_dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<SequenceBreakType, SequenceBreak>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<SequenceBreakType, SequenceBreak>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<SequenceBreakType, SequenceBreak> item)
        {
            return ((ICollection<KeyValuePair<SequenceBreakType, SequenceBreak>>)_dictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<SequenceBreakType, SequenceBreak>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<SequenceBreakType, SequenceBreak>>)_dictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
