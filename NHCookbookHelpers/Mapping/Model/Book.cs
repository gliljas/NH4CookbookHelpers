namespace NH4CookbookHelpers.Mapping.Model
{
    public class Book : Product
    {

        public virtual string ISBN { get; set; }
        public virtual string Author { get; set; }

    }
}
