using System;

namespace NH4CookbookHelpers.Model
{
    public class Publisher 
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; set; }

    }
}