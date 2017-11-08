using System.Collections;
using Newtonsoft.Json.Linq;
using NHibernate.Transform;

namespace QueryRecipes.ResultTransformers
{
    public class AliasToJObjectTransformer : IResultTransformer
    {
        public object TransformTuple(object[] tuple, string[] aliases)
        {
            var returnVal=new JObject();
            for (var i = 0; i < aliases.Length; i++)
            {
                if (aliases[i] != null)
                {
                    returnVal.Add(aliases[i], tuple[i] == null ? null : JToken.FromObject(tuple[i]));
                }
            }
            return returnVal;
        }

        public IList TransformList(IList collection)
        {
            return collection;
        }
    }
}
