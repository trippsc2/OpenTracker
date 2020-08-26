using OpenTracker.Models.Markings;
using OpenTracker.Models.Utils;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.Markings.Images
{
    /// <summary>
    /// This is the dictionary container for marking image control ViewModel instances.
    /// </summary>
    public class MarkingImageDictionary : Singleton<MarkingImageDictionary>,
        IDictionary<MarkType, MarkingImageVMBase>
    {
        private static readonly ConcurrentDictionary<MarkType, MarkingImageVMBase> _dictionary =
            new ConcurrentDictionary<MarkType, MarkingImageVMBase>();

        public ICollection<MarkType> Keys =>
            ((IDictionary<MarkType, MarkingImageVMBase>)_dictionary).Keys;
        public ICollection<MarkingImageVMBase> Values =>
            ((IDictionary<MarkType, MarkingImageVMBase>)_dictionary).Values;
        public int Count =>
            ((ICollection<KeyValuePair<MarkType, MarkingImageVMBase>>)_dictionary).Count;
        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<MarkType, MarkingImageVMBase>>)_dictionary).IsReadOnly;

        public MarkingImageVMBase this[MarkType key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return ((IDictionary<MarkType, MarkingImageVMBase>)_dictionary)[key];
            }
            set => ((IDictionary<MarkType, MarkingImageVMBase>)_dictionary)[key] = value;
        }

        public event EventHandler<MarkType> MarkingImageCreated;

        /// <summary>
        /// Creates a new marking image at the specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        private void Create(MarkType key)
        {
            Add(key, MarkingImageFactory.GetMarkingImageVM(key));
            MarkingImageCreated?.Invoke(this, key);
        }

        public void Add(MarkType key, MarkingImageVMBase value)
        {
            ((IDictionary<MarkType, MarkingImageVMBase>)_dictionary).Add(key, value);
        }

        public bool ContainsKey(MarkType key)
        {
            return ((IDictionary<MarkType, MarkingImageVMBase>)_dictionary).ContainsKey(key);
        }

        public bool Remove(MarkType key)
        {
            return ((IDictionary<MarkType, MarkingImageVMBase>)_dictionary).Remove(key);
        }

        public bool TryGetValue(MarkType key, out MarkingImageVMBase value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return ((IDictionary<MarkType, MarkingImageVMBase>)_dictionary).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<MarkType, MarkingImageVMBase> item)
        {
            ((ICollection<KeyValuePair<MarkType, MarkingImageVMBase>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<MarkType, MarkingImageVMBase>>)_dictionary).Clear();
        }

        public bool Contains(KeyValuePair<MarkType, MarkingImageVMBase> item)
        {
            return ((ICollection<KeyValuePair<MarkType, MarkingImageVMBase>>)_dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<MarkType, MarkingImageVMBase>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<MarkType, MarkingImageVMBase>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<MarkType, MarkingImageVMBase> item)
        {
            return ((ICollection<KeyValuePair<MarkType, MarkingImageVMBase>>)_dictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<MarkType, MarkingImageVMBase>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<MarkType, MarkingImageVMBase>>)_dictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
