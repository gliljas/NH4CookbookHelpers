using System;

namespace Eg.Core
{
    public abstract class Entity
    {
        public virtual Guid Id { get; protected set; }
    }
}