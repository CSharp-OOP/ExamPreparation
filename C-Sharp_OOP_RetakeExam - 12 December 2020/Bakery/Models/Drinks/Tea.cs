namespace Bakery.Models.Drinks
{
    public class Tea : Drink
    {
        public const decimal price = 2.5m;
        public Tea(string name, int portion, string brand)
            : base(name, portion, price, brand)
        {
        }
    }
}
