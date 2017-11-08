using System;
using System.Collections.Generic;
using NH4CookbookHelpers.Model;

namespace QueryRecipes.ExtraLazy
{
    public class Car : Entity
    {
        public Car()
        {
            Accessories=new HashSet<Accessory>();
        }

        public virtual string Make { get; set; }
        public virtual string Model { get; set; }
        public virtual ISet<Accessory> Accessories { get; set; }
    }
}
