using System;

namespace MappingRecipes.Enumerations
{
    public class Account
    {
        public virtual Guid Id { get; set; }
        public virtual AccountTypes AcctType { get; set; }
        public virtual string Number { get; set; }
        public virtual string Name { get; set; }
    }
}