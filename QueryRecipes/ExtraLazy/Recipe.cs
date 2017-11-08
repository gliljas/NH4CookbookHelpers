using System;
using System.Linq;
using NH4CookbookHelpers;
using NHibernate;
using NHibernate.Cfg;

namespace QueryRecipes.ExtraLazy
{
    public class Recipe : QueryRecipe
    {
        private int _carId;
        private int _firstAccessoryId;

        protected override void Configure(Configuration nhConfig)
        {
            nhConfig.AddResource("QueryRecipes.ExtraLazy.Car.hbm.xml", GetType().Assembly);
        }
        
protected override void AddData(ISessionFactory sessionFactory)
{
    using (var session = sessionFactory.OpenSession())
    {
        using (var tx = session.BeginTransaction())
        {
            var car = new Car { Make = "SAAB", Model = "9-5" };
            for (var i = 0; i < 100; i++)
            {
                var accessory = new Accessory { Name = "Accessory" + i };
                car.Accessories.Add(accessory);
            }
            session.Save(car);
            _carId = car.Id;
            _firstAccessoryId = car.Accessories.First().Id;
            tx.Commit();
        }
    }
}

protected override void Run(ISession session)
{
    //Get the car
    var car = session.Get<Car>(_carId);
    //And one of the accessories
    var accessory = session.Get<Accessory>(_firstAccessoryId);
    Console.WriteLine("Accessory count: {0}", car.Accessories.Count);
    Console.WriteLine("Car has accessory {0}: {1}", accessory.Name, car.Accessories.Contains(accessory));
}
    }
}
