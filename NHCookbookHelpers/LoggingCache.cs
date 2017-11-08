using System.Collections;
using System.Collections.Generic;
using System.Linq;
using log4net;
using NHibernate.Cache;

namespace NH4CookbookHelpers
{
    public class LoggingCache : ICache
    {
        private static readonly ILog Log = LogManager.GetLogger("LoggingCache");
        private readonly ICache _innerCache;

        public LoggingCache(ICache innerCache)
        {
            _innerCache = innerCache;
        }

        public object Get(object key)
        {
            Log.Debug("Get key:" + key);
            return _innerCache.Get(key);
        }

        public void Put(object key, object value)
        {
            var list = value as IList<object>;
            var item = value as NHibernate.Cache.Entry.CacheEntry;
            if (list != null)
            {
                Log.Debug("Put key:" + key + ",value:" + string.Join("", list.Select((x, i) => string.Format("[{0}]={1}\r\n", i, x))));
            }
            else
            {
                Log.Debug("Put key:" + key + ",value:" + value);
            }
            _innerCache.Put(key,value);
        }

        public void Remove(object key)
        {
            Log.Debug("Remove key:" + key);
            _innerCache.Remove(key);
        }

        public void Clear()
        {
            Log.Debug("Clear");
            _innerCache.Clear();
        }

        public void Destroy()
        {
            Log.Debug("Destroy");
            _innerCache.Destroy();
        }

        public void Lock(object key)
        {
            _innerCache.Lock(key);
        }

        public void Unlock(object key)
        {
            _innerCache.Unlock(key);
        }

        public long NextTimestamp()
        {
            return _innerCache.NextTimestamp();
        }

        public int Timeout
        {
            get { return _innerCache.Timeout; }
        }

        public string RegionName {
            get { return _innerCache.RegionName; }
        }
    }
}