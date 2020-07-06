namespace OpenTracker.Models.Utils
{
    public class Singleton<T> where T : class, new()
    {
        private static readonly object _syncLock = new object();
        private static volatile T _instance = null;

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
