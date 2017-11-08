namespace NH4CookbookHelpers
{
    public abstract class BaseRecipe : IRecipe
    {
        public virtual void Dispose()
        {
        }

        void IRecipe.Initialize(IRecipeLogger logger)
        {
            Initialize();
        }

        public virtual void Initialize()
        {
        }

        public virtual void Run()
        {
        }

        public bool LogEnabled { get; set; }
    }
}