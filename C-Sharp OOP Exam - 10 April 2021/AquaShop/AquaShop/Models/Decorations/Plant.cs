namespace AquaShop.Models.Decorations
{
    public class Plant : Decoration
    {
        private const int Comfort = 5;
        private const decimal Price = 10.0m;
        public Plant()
            : base(Comfort, Price)
        {
        }
    }
}
