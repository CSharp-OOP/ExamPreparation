namespace Bakery.Models.BakedFoods
{
    public class Bread : BakedFood
    {
        public const int portion = 200;
        public Bread(string name, decimal price) 
            : base(name, portion, price)
        {
        }
    }
}
