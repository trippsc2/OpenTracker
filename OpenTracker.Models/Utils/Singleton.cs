namespace OpenTracker.Models.Utils
{
    /// <summary>
    /// This is a generic Singleton class.
    /// </summary>
    /// <typeparam name="T">
    /// The type of new Singleton class.
    /// </typeparam>
    public class Singleton<T> where T : class, new()
    {
        private static readonly object _syncLock = new object();
        private static volatile T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
