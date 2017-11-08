using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cache;

namespace NH4CookbookHelpers
{
    public class LoggingCacheProvider<T> : ICacheProvider where T:ICacheProvider
    {
        private readonly T _innerProvider;

        public LoggingCacheProvider()
        {
            _innerProvider = Activator.CreateInstance<T>();
        }

        public ICache BuildCache(string regionName, IDictionary<string, string> properties)
        {
            return new LoggingCache(_innerProvider.BuildCache(regionName,properties));
        }

        public long NextTimestamp()
        {
            return _innerProvider.NextTimestamp();
        }

        public void Start(IDictionary<string, string> properties)
        {
            _innerProvider.Start(properties);
        }

        public void Stop()
        {
            _innerProvider.Stop();
        }
    }
}
