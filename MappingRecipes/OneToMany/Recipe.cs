using System;
using System.Collections.Generic;
using Eg.Core;
using NH4CookbookHelpers.Mapping;
using NHibernate;
using NHibernate.Cfg;

namespace MappingRecipes.OneToMany
{
    public class Recipe : BaseMappingRecipe
    {
        protected override void Configure(Configuration cfg)
        {
            cfg.AddAssembly(typeof(Product).Assembly);
        }

        protected override void AddInitialData(ISession session)
        {
            var movie = new Movie
            {
                Name = "Hibernation",
                Description = "The countdown for the lift-off has begun",
                UnitPrice = 300,
                Actors = new List<ActorRole>
                {
                    new ActorRole
                    {
                        Actor = "Adam Quintero",
                        Role = "Joseph Wood"
                    },
                    new ActorRole
                    {
                        Actor = "Adam sQuintero",
                        Role = "Joseph Wood"
                    }
                }
            };
            session.Save(movie);
            session.Flush();
            session.Clear();
            movie = session.Get<Movie>(movie.Id);
            movie.Actors.RemoveAt(0);
            movie.Actors.Add(
                new ActorRole
                {
                    Actor = "Addam sQuintero",
                    Role = "Joseph Wood"
                });
            session.Flush();
            session.Clear();
            movie = session.Get<Movie>(movie.Id);
            Console.WriteLine(movie.Actors.Count);
        }
    }
}