using System;
using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Utils;

namespace MappingRecipes.FluentAutomapping
{
    public class Conventions
    {
        public class IdConvention : IIdConvention
        {
            public void Apply(IIdentityInstance instance)
            {
                if (instance.Property.PropertyType == typeof(Guid))
                    instance.GeneratedBy.Guid();
                else if (instance.Property.PropertyType == typeof(int))
                    instance.GeneratedBy.Native();
            }
        }

        public class ForeignKeyConvention : FluentNHibernate.Conventions.ForeignKeyConvention
        {
            protected override string GetKeyName(Member property, Type type)
            {
                if (property == null)
                    return type.Name + "Id";

                return property.Name + "Id";
            }
        }

        public class DiscriminatorConvention : ISubclassConvention
        {
            public void Apply(ISubclassInstance instance)
            {
                instance.DiscriminatorValue(instance.Type.Name);
            }
        }

        public class HasManyConvention : IHasManyConvention
        {
            public void Apply(IOneToManyCollectionInstance instance)
            {
                var prop = instance.Member as PropertyInfo;
                if (prop != null && prop.PropertyType.Closes(typeof(IList<>)))
                {
                    instance.AsList();
                    instance.Key.Column(instance.Relationship.EntityType.Name + "Id");
                    instance.Index.Column("Ordinal");
                }
                instance.Cascade.AllDeleteOrphan();
            }
        }
    }
}