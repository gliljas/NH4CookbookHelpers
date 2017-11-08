using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace NH4CookbookHelpers.Mapping
{
    public abstract class RecipeWithDatabase : IRecipe
    {
        private Configuration _cfg;
        protected ISessionFactory SessionFactory;
        private IRecipeLogger _logger;
      

        protected Configuration GetConfiguration()
        {
            Configuration nhConfig=null;
            if (RecipeLoader.DefaultConfiguration != null)
            {
                nhConfig = RecipeLoader.DefaultConfiguration();
            }
            nhConfig = nhConfig ?? new Configuration().DataBaseIntegration(db =>
            {
                db.Dialect<SQLiteDialect>();
                db.Driver<SQLite20Driver>();
                db.ConnectionProvider<SharedConnectionConnectionProvider>();
                db.ConnectionString = "FullUri=file:memorydb.db";
            });
            nhConfig.DataBaseIntegration(db =>
            {
                db.LogFormattedSql = true;
                db.LogSqlInConsole = false;
            });
            nhConfig.SetProperty("generate_statistics", "true");
            return nhConfig;
        }
        void IRecipe.Initialize(IRecipeLogger logger)
        {
            _logger = logger;
            _cfg = GetConfiguration();
            AddBaseMappings(_cfg);
            var modelMapper = new ModelMapper();
            //AddMappings(modelMapper);
            Configure(_cfg);
            _cfg.AddMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities());
            Type cacheProviderType;
            if (_cfg.Properties.ContainsKey("cache.provider_class") && (cacheProviderType = Type.GetType(_cfg.Properties["cache.provider_class"])) != null)
            {
                cacheProviderType = typeof(LoggingCacheProvider<>).GetGenericTypeDefinition().MakeGenericType(cacheProviderType);
                _cfg.SetProperty("cache.provider_class", cacheProviderType.AssemblyQualifiedName);
            }
        }

        

        protected internal virtual void AddBaseMappings(Configuration nhConfig)
        {

        }

        protected virtual void Configure(Configuration nhConfig)
        {

        }

        //protected virtual void AddMappings(ModelMapper modelMapper)
        //{

        //}

        protected virtual void LogMessage(string message)
        {
            if (_logger != null && LogEnabled)
            {
                _logger.LogMessage(message);
            }
        }

        public void Run()
        {
            try
            {
                SessionFactory = _cfg.BuildSessionFactory();
                var dialect= Activator.CreateInstance(Type.GetType(_cfg.Properties[NHibernate.Cfg.Environment.Dialect])) as Dialect;
               
                using (var sharedSession = SessionFactory.OpenSession())
                {
                    using (var writer = new StringWriter())
                    {
                        new SchemaExport(_cfg).Execute(false, true, false, sharedSession.Connection, writer);
                        writer.Flush();
                        //       Console.WriteLine(writer.ToString());
                    }
                    RunRecipe();
                    foreach (var table in _cfg.ClassMappings.Select(x => x.Table).Union(_cfg.CollectionMappings.Select(x => x.CollectionTable)).Distinct())
                    {
                        var schema = (sharedSession.Connection as DbConnection).GetSchema();
                        var tb = new DataTable();
                        using (var cmd = sharedSession.Connection.CreateCommand())
                        {

                            cmd.CommandText = "SELECT * FROM " + table.GetQuotedName(dialect);
                            using (var dr = cmd.ExecuteReader())
                            {

                                tb.Load(dr);
                            }
                        }
                        _logger.ShowTable(table.Name, schema, tb);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
            finally
            {
                SharedConnectionConnectionProvider.CloseDatabase();
            }

        }

        public bool LogEnabled { get; set; } = true;

        internal virtual void RunRecipe()
        {
        }

        public void Dispose()
        {
        }
    }

    public class SharedConnectionConnectionProvider : DriverConnectionProvider
    {
        [ThreadStatic]
        private static IDbConnection _sharedConnection = null;
        public override IDbConnection GetConnection()
        {
            if (_sharedConnection == null)
            {
                _sharedConnection = base.GetConnection();
            }
            return _sharedConnection;
        }

        public override void CloseConnection(IDbConnection conn)
        {
           
        }

        
        public static void CloseDatabase()
        {
            if (_sharedConnection != null)
                _sharedConnection.Dispose();
            _sharedConnection = null;
        }
    }
}