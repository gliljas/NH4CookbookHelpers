using System.Collections.Generic;

namespace MappingRecipes.PropertyRefs
{
    public class Customer
    {
        public Customer()
        {
            ContactPersons = new HashSet<ContactPerson>();
        }

        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual ISet<ContactPerson> ContactPersons { get; set; }
        public virtual int CompanyId { get; set; }
    }
}