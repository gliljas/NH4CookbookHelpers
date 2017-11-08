using System.Linq;
using NHibernate.Cfg;

namespace NH4CookbookHelpers.Mapping
{
    public abstract class HbmMappingRecipe : BaseMappingRecipe
    {
        protected override void Configure(Configuration cfg)
        {
           
        }

        protected internal override void AddBaseMappings(Configuration cfg)
        {
            var type = GetType();
            var resourceNames = type.Assembly.GetManifestResourceNames().Where(x => x.StartsWith(type.Namespace) && x.EndsWith(".hbm.xml"));
            foreach (string resourceName in resourceNames)
            {
                cfg.AddResource(resourceName, type.Assembly);
            }
        }
    }



}
