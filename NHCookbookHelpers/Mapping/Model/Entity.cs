using System;

namespace NH4CookbookHelpers.Mapping.Model
{
    public abstract class Entity
    {
            
        public virtual Guid Id { get; protected set; }

    }

}
