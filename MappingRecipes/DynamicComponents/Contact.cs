using System;
using System.Collections;

namespace MappingRecipes.DynamicComponents
{
    public class Contact
    {
        public Contact()
        {
            Attributes = new Hashtable();
        }

        public virtual Guid Id { get; protected set; }
        public virtual IDictionary Attributes { get; set; }
    }
}