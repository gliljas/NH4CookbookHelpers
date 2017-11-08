using System.Collections.Generic;

namespace NH4CookbookHelpers.Model
{
    public class Movie : Product
    {
        private ISet<ActorRole> _actors;

        public Movie()
        {
            _actors=new HashSet<ActorRole>();
        }
        public virtual string Director { get; set; }

        public virtual IEnumerable<ActorRole> Actors
        {
            get { return _actors; }
        }

        public virtual void AddActor(string actor, string role)
        {
            _actors.Add(new ActorRole {Actor = actor, Role = role, Movie = this});
        }
    }
}
