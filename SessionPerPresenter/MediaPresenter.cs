using System.Linq;
using NH4CookbookHelpers.Model;
using SessionPerPresenter.Data;

namespace SessionPerPresenter
{
    public class MediaPresenter : IPresenter
    {
        private readonly IDao<Movie> _movieDao;
        private readonly IDao<Book> _bookDao;
        public MediaPresenter(IDao<Movie> movieDao,
          IDao<Book> bookDao)
        {
            _movieDao = movieDao;
            _bookDao = bookDao;
        }
        public ProductListView ShowBooks()
        {
            return new ProductListView("All Books",
              _bookDao.GetAll().OfType<Product>());
        }
        public ProductListView ShowMovies()
        {
            return new ProductListView("All Movies",
              _movieDao.GetAll().OfType<Product>());
        }
        public void Dispose()
        {
            _movieDao.Dispose();
            _bookDao.Dispose();
        }
    }
}
