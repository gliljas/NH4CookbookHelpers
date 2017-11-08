namespace MappingRecipes.PropertyRefs
{
    public class ContactPerson
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual Customer Customer { get; set; }
    }
}