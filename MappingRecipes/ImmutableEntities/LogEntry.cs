using System;

namespace MappingRecipes.ImmutableEntities
{
    public class LogEntry
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Message { get; set; }
    }
}