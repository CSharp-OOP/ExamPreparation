namespace AquaShop.Models.Decorations
{
    public class Ornament : Decoration
    {
        private const int Comfort = 1;
        private const decimal Price = 5.0m;
        public Ornament() 
            : base(Comfort, Price)
        {
        }
    }
}
