using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace OpenTracker.Utils
{
    public interface IObservableCollection<T> : IList<T>, INotifyCollectionChanged
    {
    }
}
