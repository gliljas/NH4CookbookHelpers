using System.Collections.Generic;
using System.Linq;
using NHibernate.Shards;
using NHibernate.Shards.Engine;
using NHibernate.Shards.LoadBalance;
using NHibernate.Shards.Strategy;
using NHibernate.Shards.Strategy.Access;
using NHibernate.Shards.Strategy.Resolution;
using NHibernate.Shards.Strategy.Selection;

namespace QueryRecipes.Sharding
{
    public class ShardStrategyFactory : IShardStrategyFactory
    {
        public IShardStrategy NewShardStrategy(
          IEnumerable<ShardId> shardIds)
        {
            var loadBalancer = new RoundRobinShardLoadBalancer(shardIds);
            return new ShardStrategyImpl(
              new RoundRobinShardSelectionStrategy(loadBalancer),
              new AllShardsShardResolutionStrategy(shardIds),
              new SequentialShardAccessStrategy());
        }
    }
}
