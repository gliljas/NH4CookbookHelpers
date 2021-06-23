using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using NHibernate.Cache;

namespace NH4CookbookHelpers
{
    public class LoggingCache : CacheBase
    {
        private static readonly ILog Log = LogManager.GetLogger("LoggingCache");
        private readonly CacheBase _innerCache;

        public LoggingCache(CacheBase innerCache)
        {
            _innerCache = innerCache;
        }

        public override object Get(object key)
        {
            Log.Debug("Get key:" + key);
            return _innerCache.Get(key);
        }

        public override void Put(object key, object value)
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

        public override void Remove(object key)
        {
            Log.Debug("Remove key:" + key);
            _innerCache.Remove(key);
        }

        public override void Clear()
        {
            Log.Debug("Clear");
            _innerCache.Clear();
        }

        public override void Destroy()
        {
            Log.Debug("Destroy");
            _innerCache.Destroy();
        }

        public override object Lock(object key)
        {
            return _innerCache.Lock(key);
        }

        public override void Unlock(object key, object lockValue)
        {
            _innerCache.Unlock(key, lockValue);
        }

        public override long NextTimestamp()
        {
            return _innerCache.NextTimestamp();
        }

        public override int Timeout => _innerCache.Timeout;

        public override string RegionName => _innerCache.RegionName;

        public override Task<object> GetAsync(object key, CancellationToken cancellationToken)
        {
            Log.Debug("GetAsync key:" + key);
            return _innerCache.GetAsync(key, cancellationToken);
        }

        public override Task ClearAsync(CancellationToken cancellationToken)
        {
            Log.Debug("ClearAsync");
            return _innerCache.ClearAsync(cancellationToken);
        }

        public override object[] GetMany(object[] keys)
        {
            Log.Debug("GetMany");
            return _innerCache.GetMany(keys);
        }

        public override Task<object[]> GetManyAsync(object[] keys, CancellationToken cancellationToken)
        {
            Log.Debug("GetManyAsync");
            return _innerCache.GetManyAsync(keys, cancellationToken);
        }

        public override Task<object> LockAsync(object key, CancellationToken cancellationToken)
        {
            Log.Debug("LockAsync");
            return _innerCache.LockAsync(key, cancellationToken);
        }

        public override Task PutAsync(object key, object value, CancellationToken cancellationToken)
        {
            Log.Debug("PutAsync");
            return _innerCache.PutAsync(key, value, cancellationToken);
        }

        public override void PutMany(object[] keys, object[] values)
        {
            Log.Debug("PutMany");
            _innerCache.PutMany(keys, values);
        }

        public override Task PutManyAsync(object[] keys, object[] values, CancellationToken cancellationToken)
        {
            Log.Debug("PutManyAsync");
            return _innerCache.PutManyAsync(keys, values, cancellationToken);
        }

        public override object LockMany(object[] keys)
        {
            Log.Debug("LockMany");
            return _innerCache.LockMany(keys);
        }

        public override Task<object> LockManyAsync(object[] keys, CancellationToken cancellationToken)
        {
            Log.Debug("LockManyAsync");
            return _innerCache.LockManyAsync(keys, cancellationToken);
        }

        public override Task UnlockAsync(object key, object lockValue, CancellationToken cancellationToken)
        {
            Log.Debug("UnlockAsync");
            return _innerCache.UnlockAsync(key, lockValue, cancellationToken);
        }

        public override void UnlockMany(object[] keys, object lockValue)
        {
            Log.Debug("UnlockMany");
            _innerCache.UnlockMany(keys, lockValue);
        }

        public override Task UnlockManyAsync(object[] keys, object lockValue, CancellationToken cancellationToken)
        {
            Log.Debug("UnlockManyAsync");
            return _innerCache.UnlockManyAsync(keys, lockValue, cancellationToken);
        }

        public override Task RemoveAsync(object key, CancellationToken cancellationToken)
        {
            Log.Debug("RemoveAsync");
            return _innerCache.RemoveAsync(key, cancellationToken);
        }

        public override bool PreferMultipleGet => _innerCache.PreferMultipleGet;
    }
}