using System;
using NH4CookbookHelpers.Model;
using NHibernate;
using Ninject;


namespace SessionPerPresenter
{
    class Program
    {
        static void Main(string[] args)
        {
            var sessionFactory = ProductModel.CreateExampleSessionFactory(true);
            var kernel = new StandardKernel();
            kernel.Load(new NinjectBindings());
            kernel.Bind<ISessionFactory>()
                .ToConstant(sessionFactory);

            var media1 = kernel.Get<MediaPresenter>();
            var media2 = kernel.Get<MediaPresenter>();

            media1.ShowBooks().Show();
            media2.ShowMovies().Show();

            media1.Dispose();
            media2.Dispose();

            using (var product = kernel.Get<ProductPresenter>())
            {
                product.ShowAllProducts().Show();
            }

            Console.WriteLine("Press any key");
            Console.ReadKey();


        }

    }
}
