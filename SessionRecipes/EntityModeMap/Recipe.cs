using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Mapping;
using NHibernate;
using NHibernate.Cfg;

namespace SessionRecipes.EntityModeMap
{
    public class Recipe : HbmMappingRecipe
    {

        protected override void Configure(Configuration cfg)
        {
            cfg.SetProperty("default_entity_mode", "dynamic-map");
        }

        protected override void AddInitialData(ISession session)
        {
            var movieActors = new List<Dictionary<string, object>>()
            {
              new Dictionary<string, object>() {
                  {"Actor","Keanu Reeves"},
                  {"Role","Neo"}
              },
              new Dictionary<string, object>() {
                  {"Actor", "Carrie-Ann Moss"},
                  {"Role", "Trinity"}
              }
            };

            var movie = new Dictionary<string, object>()
            {
              {"Name", "The Matrix"},
              {"Description", "Sci-Fi Action film"},
              {"UnitPrice", 18.99M},
              {"Director", "Wachowski Brothers"},
              {"Actors", movieActors}
            };

            session.Save("Movie", movie);
        }

        public override void RunQueries(ISession session)
        {
            var movies = session.CreateQuery("from Movie").List<IDictionary>();
            foreach (var movie in movies)
            {
                Console.WriteLine("Movie:{0}", movie["Name"]);
                Console.WriteLine("Actors");
                foreach (var actor in ((IEnumerable)movie["Actors"]).OfType<IDictionary>())
                {
                    Console.WriteLine("{0} as {1}", actor["Actor"], actor["Role"]);
                }
            }
        }
    }
}
