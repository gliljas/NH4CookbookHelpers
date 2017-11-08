using System;
using System.Collections.Generic;
using System.Linq;
using NH4CookbookHelpers;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Shards;
using NHibernate.Shards.Cfg;
using NHibernate.Shards.Session;
using NHibernate.Shards.Tool;

namespace QueryRecipes.Sharding
{
    public class Recipe : BaseRecipe
    {
        private IShardedSessionFactory _sessionFactory;


        public override void Initialize()
        {
            var connStrNames = new List<string> { "Shard1", "Shard2", "Shard3" };

            var shardConfigs = connStrNames.Select((x, index) => new ShardConfiguration
            {
                ShardId = (short)index,
                ConnectionStringName = x
            });

            var protoConfig = new Configuration()
                .DataBaseIntegration(
                    x =>
                    {
                        x.Dialect<MsSql2012Dialect>();
                        x.Driver<Sql2008ClientDriver>();
                    })
                .AddResource("QueryRecipes.Sharding.ShardedProduct.hbm.xml", GetType().Assembly);

            var shardedConfig = new ShardedConfiguration(protoConfig, shardConfigs, new ShardStrategyFactory());

            CreateSchema(shardedConfig);

            try
            {
                _sessionFactory = shardedConfig.BuildShardedSessionFactory();
            }
            catch
            {
                DropSchema(shardedConfig);
                throw;
            }
        }

        private void CreateSchema(ShardedConfiguration shardedConfiguration)
        {
            new ShardedSchemaExport(shardedConfiguration).Create(false, true);
        }

        private void DropSchema(ShardedConfiguration shardedConfiguration)
        {
            new ShardedSchemaExport(shardedConfiguration).Drop(false, true);
        }
        public override void Run()
        {
            string lastId="";
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    for (var i = 0; i < 99; i++)
                    {
                        var product = new ShardedProduct()
                        {
                            Name = "Product" + i,
                        };
                        session.Save(product);
                        lastId = product.Id;
                    }
                    tx.Commit();
                }
            }

            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var p = session.Get<ShardedProduct>(lastId);
                    Console.WriteLine("Product Id: {0}, Name: {1}", p.Id, p.Name);
                    p.Name = "Test";

                    //var query = @"from ShardedProduct p 
                    //        where upper(p.Name)
                    //        like '%1%'";
                    //var products = session.CreateQuery(query)
                    //    .List<ShardedProduct>();

                    //foreach (var p in products)
                    //{
                    //    Console.WriteLine("Product Id: {0}, Name: {1}", p.Id, p.Name);
                    //}
                    tx.Commit();
                }
                session.Close();
            }
        }
    }
}

