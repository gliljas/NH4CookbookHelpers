using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Steps;
using FluentNHibernate.Conventions;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Utils;
using NH4CookbookHelpers.Mapping.Model;

namespace MappingRecipes.FluentAutomapping
{
    public class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == typeof(Product).Namespace;
        }

        public override bool IsDiscriminated(Type type)
        {
            return type == typeof(Product);
        }

        public override string GetDiscriminatorColumn(Type type)
        {
            return type.Name + "Type";
        }

        public override IEnumerable<IAutomappingStep> GetMappingSteps(AutoMapper mapper, IConventionFinder conventionFinder)
        {
            return base.GetMappingSteps(mapper, conventionFinder).Select(x => x.GetType() == typeof(HasManyStep) ? new MyMa(x) : x);
        }
    }

    public class MyMa : IAutomappingStep
    {
        private readonly IAutomappingStep _wrapped;

        public MyMa(IAutomappingStep wrapped)
        {
            _wrapped = wrapped;
        }


        public bool ShouldMap(Member member)
        {
            var type = member.PropertyType;
            if (type.Namespace != "Iesi.Collections.Generic" &&
                type.Namespace != "System.Collections.Generic")
                return false;
            if (type.HasInterface(typeof(IDictionary)) || type.ClosesInterface(typeof(IDictionary<,>)) || type.Closes(typeof(IDictionary<,>)))
                return false;

            var hasInverse = GetInverseProperty(member) != null;
            return hasInverse;
        }


        public void Map(ClassMappingBase classMap, Member member)
        {
            _wrapped.Map(classMap, member);
        }

        private static Member GetInverseProperty(Member member)
        {
            var type = member.PropertyType;
            var expectedInversePropertyType = type.GetGenericTypeDefinition()
                .MakeGenericType(member.DeclaringType);

            var argument = type.GetGenericArguments()[0];
            return argument.GetProperties()
                .Select(x => x.ToMember())
                .Where(x => x.PropertyType == expectedInversePropertyType && x != member)
                .FirstOrDefault();
        }
    }
}