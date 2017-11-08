using System;
using System.Collections.Generic;
using NH4CookbookHelpers.Model;

namespace SessionPerPresenter
{
    public class ProductListView
    {
        private readonly string _description;
        private readonly IEnumerable<Product> _products;
        public ProductListView(
          string description,
          IEnumerable<Product> products)
        {
            _description = description;
            _products = products;
        }
        public void Show()
        {
            Console.WriteLine(_description);
            foreach (var p in _products)
                Console.WriteLine(" * {0}", p.Name);
        }
    }
}
