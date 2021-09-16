namespace Bakery.Models.Tables
{
    public class OutsideTable : Table
    {
        public const decimal InitialPricePerPerson = 3.5m;
        public OutsideTable(int tableNumber, int capacity) 
            : base(tableNumber, capacity, InitialPricePerPerson)
        {
        }
    }
}
