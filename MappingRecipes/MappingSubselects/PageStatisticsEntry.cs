namespace MappingRecipes.MappingSubselects
{
    public class PageStatisticsEntry
    {
        public virtual string Url { get; protected set; }

        public virtual int ViewCount { get; set; }
    }
}