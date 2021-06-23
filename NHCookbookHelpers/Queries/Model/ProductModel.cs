using System;
using System.Data;
using System.Data.Common;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace NH4CookbookHelpers.Model
{
    public static class ProductModel
    {
        public static ISessionFactory CreateExampleSessionFactory(bool addData, Action<Configuration> configureAction=null, bool forceSqlLiteDatabase=false)
        {
            var nhConfig = new Configuration();
            try
            {
                if (!forceSqlLiteDatabase)
                {
                    nhConfig.Configure();
                }
            }
            catch (HibernateConfigException ex)
            {
                var inner = ex.InnerException as FileNotFoundException;
                if (inner==null || !inner.FileName.Contains("hibernate.cfg.xml"))
                {
                    throw;
                }
            }
            catch (FileNotFoundException ex)
            {
                if (!ex.FileName.Contains("hibernate.cfg.xml"))
                {
                    throw;
                }
            }


            if (configureAction != null)
            {
                configureAction.Invoke(nhConfig);
            }

            nhConfig.DataBaseIntegration(db =>
             {
                 if (!nhConfig.Properties.ContainsKey(NHibernate.Cfg.Environment.Dialect) || string.IsNullOrEmpty(nhConfig.Properties[NHibernate.Cfg.Environment.Dialect]))
                 {
                     db.Dialect<SQLiteDialect>();
                     db.Driver<SQLite20Driver>();
                     db.ConnectionString = "FullUri=file:memorydb.db?mode=memory&cache=shared";
                     db.ConnectionProvider<KeepOneConnectionAliveConnectionProvider>();
                 }
             });


            AddBaseMappings(nhConfig);

            var sessionFactory = nhConfig.BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                new SchemaExport(nhConfig).Execute(false, true, false, session.Connection, null);
            }

            AddBaseData(sessionFactory);

            return sessionFactory;
        }

        public class KeepOneConnectionAliveConnectionProvider : DriverConnectionProvider
        {
            private DbConnection _sharedConnection = null;
            public override DbConnection GetConnection()
            {
                if (_sharedConnection == null)
                {
                    _sharedConnection=base.GetConnection();
                }
                return base.GetConnection();
            }
        }

        public static void AddBaseData(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    CreateMovies(session);
                    CreateBook(session);
                    tx.Commit();
                }
            }
        }

        private static void CreateMovies(ISession session)
        {
            var movie1 = new Movie()
            {
                Name = "Raiders of the Lost Ark",
                Description = "Awesome",
                UnitPrice = 9.59M,
                Director = "Steven Spielberg"
            };
            movie1.AddActor("Harrison Ford", "Indiana Jones");
            movie1.AddActor("Karen Allen", "Marion Ravenwood");

            session.Save(movie1);


            var movie2 = new Movie
            {
                Name = "The Bucket List",
                Description = "Good",
                UnitPrice = 15M,
                Director = "Rob Reiner"
            };
            movie2.AddActor("Jack Nicholson", "Edward Cole");
            movie2.AddActor("Morgan Freeman", "Carter Chambers");

            session.Save(movie2);
        }

        private static void CreateBook(ISession session)
        {
            var publisher = new Publisher { Name = "Packt Publishing" };
            session.Save(publisher);

            session.Save(
                new Book
                {
                    Name = "NHibernate 3.0 Cookbook",
                    Description = "NHibernate examples",
                    UnitPrice = 50M,
                    Author = "Jason Dentler",
                    ISBN = "978-1-849513-04-3",
                    Publisher = publisher
                });
        }

        public static void AddBaseMappings(Configuration cfg)
        {
            var modelMapper = new ModelMapper();
            modelMapper.AddMapping<BookMapping>();
            modelMapper.AddMapping<MovieMapping>();
            modelMapper.AddMapping<ProductMapping>();
            modelMapper.AddMapping<PublisherMapping>();
            modelMapper.AddMapping<ActorRoleMapping>();
            var mapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(mapping);
        }
    }

    //public class InMemorySessionFactory : ISessionFactory
    //{
    //    private readonly ISessionFactory _inner;
    //    private ISession _sharedSession;

    //    public InMemorySessionFactory(ISessionFactory inner)
    //    {
    //        _inner = inner;
    //        _sharedSession = _inner.OpenSession();
    //        if (_sharedSession.Connection.State != ConnectionState.Open)
    //        {
    //            _sharedSession.Connection.Open();
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        _sharedSession.Close();
    //        _inner.Dispose();
    //    }

    //    public ISession OpenSession(IDbConnection conn)
    //    {
    //        return _inner.OpenSession(conn);
    //    }

    //    public ISession OpenSession(IInterceptor sessionLocalInterceptor)
    //    {
    //        return _inner.OpenSession(sessionLocalInterceptor);
    //    }

    //    public ISession OpenSession(IDbConnection conn, IInterceptor sessionLocalInterceptor)
    //    {
    //        return _inner.OpenSession(conn, sessionLocalInterceptor);
    //    }

    //    public ISession OpenSession()
    //    {
    //        return _inner.OpenSession();
    //    }

    //    public IClassMetadata GetClassMetadata(Type persistentClass)
    //    {
    //        return _inner.GetClassMetadata(persistentClass);
    //    }

    //    public IClassMetadata GetClassMetadata(string entityName)
    //    {
    //        return _inner.GetClassMetadata(entityName);
    //    }

    //    public ICollectionMetadata GetCollectionMetadata(string roleName)
    //    {
    //        return _inner.GetCollectionMetadata(roleName);
    //    }

    //    public IDictionary<string, IClassMetadata> GetAllClassMetadata()
    //    {
    //        return _inner.GetAllClassMetadata();
    //    }

    //    public IDictionary<string, ICollectionMetadata> GetAllCollectionMetadata()
    //    {
    //        return _inner.GetAllCollectionMetadata();
    //    }

    //    public void Close()
    //    {
    //        _inner.Close();
    //    }

    //    public void Evict(Type persistentClass)
    //    {
    //        _inner.Evict(persistentClass);
    //    }

    //    public void Evict(Type persistentClass, object id)
    //    {
    //        _inner.Evict(persistentClass,id);
    //    }

    //    public void EvictEntity(string entityName)
    //    {
    //        _inner.EvictEntity(entityName);
    //    }

    //    public void EvictEntity(string entityName, object id)
    //    {
    //        _inner.EvictEntity(entityName,id);
    //    }

    //    public void EvictCollection(string roleName)
    //    {
    //        _inner.EvictCollection(roleName);
    //    }

    //    public void EvictCollection(string roleName, object id)
    //    {
    //        _inner.EvictCollection(roleName,id);
    //    }

    //    public void EvictQueries()
    //    {
    //        _inner.EvictQueries();
    //    }

    //    public void EvictQueries(string cacheRegion)
    //    {
    //        _inner.EvictQueries(cacheRegion);
    //    }

    //    public IStatelessSession OpenStatelessSession()
    //    {
    //        return _inner.OpenStatelessSession();
    //    }

    //    public IStatelessSession OpenStatelessSession(IDbConnection connection)
    //    {
    //        return _inner.OpenStatelessSession(connection);
    //    }

    //    public FilterDefinition GetFilterDefinition(string filterName)
    //    {
    //        return _inner.GetFilterDefinition(filterName);
    //    }

    //    public ISession GetCurrentSession()
    //    {
    //        return _inner.GetCurrentSession();
    //    }

    //    public IStatistics Statistics {
    //        get { return _inner.Statistics; }
    //    }

    //    public bool IsClosed
    //    {
    //        get { return _inner.IsClosed; }
    //    }

    //    public ICollection<string> DefinedFilterNames {
    //        get { return _inner.DefinedFilterNames; }
    //    }
    //}
}
