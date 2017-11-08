namespace MappingRecipes.MappingJoins
{
    public class Article
    {
        public virtual int Id { get; protected set; }
        public virtual string Title { get; set; }
        public virtual string Abstract { get; set; }
        public virtual string Author { get; set; }
        public virtual string FullText { get; set; }
    }
}