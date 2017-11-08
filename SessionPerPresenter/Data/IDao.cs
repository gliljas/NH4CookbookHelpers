using System;
using System.Collections.Generic;

namespace SessionPerPresenter.Data
{
    public interface IDao<TEntity> : IDisposable
  where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
    }
}
