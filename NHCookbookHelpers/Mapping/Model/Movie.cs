using System.Collections.Generic;

namespace NH4CookbookHelpers.Mapping.Model
{
    public class Movie : Product
    {
        public Movie()
        {
            Actors=new List<ActorRole>();
        }
        public virtual string Director { get; set; }
        public virtual IList<ActorRole> Actors { get; set; }
    }
}

