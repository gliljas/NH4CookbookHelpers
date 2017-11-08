using System;

namespace MappingRecipes.SerializableValues
{
    public class Error
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime ErrorDateTime { get; set; }
        public virtual Exception Exception { get; set; }
    }
}