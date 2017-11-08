using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NH4CookbookHelpers.Mapping.Model;
using NHibernate.Mapping.ByCode;

namespace MappingRecipes.MappingByConvention
{
    public class MyModelMapper : ConventionModelMapper
    {
        public MyModelMapper()
        {
            IsEntity((t, declared) => typeof(Entity).IsAssignableFrom(t) && typeof(Entity) != t);

            IsRootEntity((t, declared) => t.BaseType == typeof(Entity));

            IsList((member, declared) =>
                member
                    .GetPropertyOrFieldType()
                    .IsGenericType &&
                member
                    .GetPropertyOrFieldType()
                    .GetGenericInterfaceTypeDefinitions()
                    .Contains(typeof(IList<>)));


            IsVersion((member, declared) =>
                member.Name == "Version" &&
                member.MemberType == MemberTypes.Property &&
                member.GetPropertyOrFieldType() == typeof(int));

            IsTablePerClassHierarchy((t, declared) => typeof(Product).IsAssignableFrom(t));

            BeforeMapSubclass += ConfigureDiscriminatorValue;
            BeforeMapClass += ConfigureDiscriminatorColumn;
            BeforeMapClass += ConfigurePoidGenerator;
            BeforeMapList += ConfigureListCascading;
        }


        private void ConfigureListCascading(IModelInspector modelInspector, PropertyPath member, IListPropertiesMapper propertyCustomizer)
        {
            propertyCustomizer.Cascade(Cascade.All | Cascade.DeleteOrphans);
        }

        private void ConfigurePoidGenerator(IModelInspector modelInspector, Type type, IClassAttributesMapper classCustomizer)
        {
            classCustomizer.Id(id => id.Generator(Generators.GuidComb));
        }

        private void ConfigureDiscriminatorColumn(IModelInspector modelInspector, Type type, IClassAttributesMapper classCustomizer)
        {
            if (modelInspector.IsTablePerClassHierarchy(type))
                classCustomizer.Discriminator(x => x.Column(type.Name + "Type"));
        }

        private void ConfigureDiscriminatorValue(IModelInspector modelInspector, Type type, ISubclassAttributesMapper subclassCustomizer)
        {
            subclassCustomizer.DiscriminatorValue(type.Name);
        }
    }
}