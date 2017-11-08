using System;
using NHibernate;

namespace SessionPerPresenter.Data
{
    public class SessionProvider : ISessionProvider
    {
        private readonly ISessionFactory _sessionFactory;
        private ISession _currentSession;
        public SessionProvider(ISessionFactory sessionFactory)
        {
            Console.WriteLine("Building session provider");
            _sessionFactory = sessionFactory;
        }
        public ISession GetCurrentSession()
        {
            if (null == _currentSession)
            {
                Console.WriteLine("Opening session");
                _currentSession = _sessionFactory.OpenSession();
            }
            return _currentSession;
        }
        public void DisposeCurrentSession()
        {
            _currentSession.Dispose();
            _currentSession = null;
        }
        public void Dispose()
        {
            if (_currentSession != null)
            {
                Console.WriteLine("Disposing session");
                _currentSession.Dispose();
            }
            _currentSession = null;
        }
    }
}
