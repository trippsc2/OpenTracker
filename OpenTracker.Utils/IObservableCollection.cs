using System.Collections.Generic;
using System.Collections.Specialized;

namespace OpenTracker.Utils;

public interface IObservableCollection<T> : IList<T>, INotifyCollectionChanged
{
}