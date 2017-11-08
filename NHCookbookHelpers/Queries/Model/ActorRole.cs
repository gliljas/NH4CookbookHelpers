namespace NH4CookbookHelpers.Model
{
    public class ActorRole : Entity
    {
        public virtual string Actor { get; set; }
        public virtual string Role { get; set; }
        public virtual Movie Movie { get; set; }
        
    }
}