using NHibernate;

namespace NH4CookbookHelpers.Mapping
{
    public abstract class BaseMappingRecipe : RecipeWithDatabase
    {
        private IRecipeLogger _logger;


        /// <summary>
        /// Add initial data. The session has a transaction which is committed after this method.
        /// </summary>
        /// <param name="session">The session.</param>
        protected virtual void AddInitialData(ISessionFactory sessionFactory)
        {
        }

        /// <summary>
        /// Add initial data. The session has a transaction which is committed after this method.
        /// </summary>
        /// <param name="session">The session.</param>
        protected virtual void AddInitialData(ISession session)
        {
        }

        public virtual void RunQueries(ISession session)
        {
        }

        public virtual void RunQueries(ISessionFactory sessionFactory)
        {
        }

        protected override void LogMessage(string message)
        {
            if (_logger != null)
            {
                _logger.LogMessage(message);
            }
        }

        //internal void Execute(Configuration nhConfig, IRecipeLogger logger)
        //{
        //    _logger = logger;
        //    Configure(nhConfig);
        //    var sessionFactory = nhConfig.BuildSessionFactory();

        //    using (var sharedSession = sessionFactory.OpenSession())
        //    {
        //        new SchemaExport(nhConfig).Execute(false, true, false, sharedSession.Connection, null);


        //        using (var session = sessionFactory.OpenSession())
        //        {
        //            using (var trans = session.BeginTransaction())
        //            {
        //                AddInitialData(session);
        //                trans.Commit();
        //            }
        //        }
        //        using (var session = sessionFactory.OpenSession())
        //        {

        //            using (var trans = session.BeginTransaction())
        //            {
        //                RunQueries(session);

        //                if (trans.IsActive)
        //                {
        //                    trans.Commit();
        //                }
        //            }
        //        }

        //        var tables = nhConfig.ClassMappings.Select(x => x.Table).Union(nhConfig.CollectionMappings.Select(x => x.CollectionTable)).Distinct().ToArray();
        //        foreach (var table in tables)
        //        {
        //            //var data = sharedSession.CreateSQLQuery($"select * from {table.Name}").List();
        //            var cmd = sharedSession.Connection.CreateCommand();
        //            cmd.CommandText = $"select * from {table.Name}";
        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                var schema = reader.GetSchemaTable();
        //                var dataTable = new DataTable();
        //                dataTable.Load(reader);
        //                logger.ShowTable(table.Name, schema, dataTable);
        //            }
        //        }
        //    }
        //}

        internal override void RunRecipe()
        {
            AddInitialData(SessionFactory);
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    AddInitialData(session);
                    trans.Commit();
                }
            }

            RunQueries(SessionFactory);

            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    RunQueries(session);
                    if (trans.IsActive)
                    {
                        trans.Commit();
                    }
                }
            }
        }
    }
}