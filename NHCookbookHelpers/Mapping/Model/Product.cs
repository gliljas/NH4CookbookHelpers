namespace NH4CookbookHelpers.Mapping.Model
{
    public class Product : Entity
    {

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual int Version { get; set; }
    }
}
