namespace MappingRecipes.Versioning
{
    public class VersionedProduct
    {
        public virtual int Id { get; protected set; }
        public virtual int Version { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}