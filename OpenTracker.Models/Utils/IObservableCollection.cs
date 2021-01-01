using System.Collections.Generic;
using System.Collections.Specialized;

namespace OpenTracker.Models.Utils
{
    public interface IObservableCollection<T> : IList<T>, INotifyCollectionChanged
    {
    }
}
