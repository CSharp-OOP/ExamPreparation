namespace Bakery.Models.BakedFoods
{
    public class Cake : BakedFood
    {
        public const int portion = 245;
        public Cake(string name, decimal price) 
            : base(name, portion, price)
        {
        }
    }
}
