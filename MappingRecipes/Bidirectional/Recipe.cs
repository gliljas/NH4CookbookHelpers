using System;
using NH4CookbookHelpers.Mapping;
using NHibernate;

namespace MappingRecipes.Bidirectional
{
    public class Recipe : HbmMappingRecipe
    {
        protected override void AddInitialData(ISession session)
        {
            var project1 = new Project();
            var project2 = new Project();

            session.Save(project1);
            session.Save(project2);

            var order1 = new Order();
            order1.AddItem(new OrderItem("Lemons"));
            order1.AddItem(new OrderItem("Cucumbers"));

            var order2 = new Order();
            order2.AddItem(new OrderItem("Bananas"));
            order2.AddItem(new OrderItem("Oranges"));

            project1.ConnectOrder(order1);
            project1.ConnectOrder(order2);

            order1.ConnectProject(project2);

            session.Save(order1);
            session.Save(order2);
        }

        public override void RunQueries(ISession session)
        {
            var projects = session.QueryOver<Project>().List();
            foreach (var project in projects)
            {
                Console.WriteLine("Project: {0}", project.Id);
                foreach (var order in project.Orders)
                {
                    Console.WriteLine("Order: {0}", order.Id);
                    foreach (var item in order.Items)
                        Console.WriteLine("Item: {0}", item.Name);
                }
                Console.WriteLine();
            }
        }
    }
}