using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Models.Products
{
    public class PowerSupply : Component
    {
        private const double PowerSupplyOverallPerformance = 1.05;
        public PowerSupply(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation) 
            : base(id, manufacturer, model, price, overallPerformance* PowerSupplyOverallPerformance, generation)
        {
        }
    }
}
