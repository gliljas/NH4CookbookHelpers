using System;
using System.Collections.Generic;

namespace MappingRecipes.ComponentCollections
{
    public class Customer
    {
        public Customer()
        {
            Addresses = new List<Address>();
            Tags = new HashSet<string>();
        }

        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual IList<Address> Addresses { get; set; }
        public virtual ISet<string> Tags { get; set; }
    }
}