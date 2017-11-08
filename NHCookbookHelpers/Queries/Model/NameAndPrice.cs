namespace NH4CookbookHelpers.Model
{
    public class NameAndPrice
    {

        public NameAndPrice()
        {
        }

        public NameAndPrice(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }

    }

}
