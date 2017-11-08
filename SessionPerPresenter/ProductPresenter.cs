using NH4CookbookHelpers.Model;
using SessionPerPresenter.Data;

namespace SessionPerPresenter
{
    public class ProductPresenter : IPresenter
    {
        private readonly IDao<Product> _productDao;
        public ProductPresenter(IDao<Product> productDao)
        {
            _productDao = productDao;
        }
        public ProductListView ShowAllProducts()
        {
            return new ProductListView("All Products",
                _productDao.GetAll());
        }
        public virtual void Dispose()
        {
            _productDao.Dispose();
        }
    }
}