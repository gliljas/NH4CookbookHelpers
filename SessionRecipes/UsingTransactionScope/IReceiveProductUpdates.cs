using NH4CookbookHelpers.Model;

namespace SessionRecipes.UsingTransactionScope
{
    public interface IReceiveProductUpdates
    {
        void Add(Product product);
        void Update(Product product);
        void Remove(Product product);
    }
}
