namespace NH4CookbookHelpers.Model
{
    public class Book : Product
    {
        public virtual string ISBN { get; set; }
        public virtual string Author { get; set; }
        public virtual Publisher Publisher { get; set; }

    }
}
