using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Models.Products
{
    class CentralProcessingUnit : Component
    {
        private const double CentralProcessingUnitOverallPerformance = 1.25;
        public CentralProcessingUnit(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation) 
            : base(id, manufacturer, model, price, overallPerformance* CentralProcessingUnitOverallPerformance, generation)
        {
        }
    }
}
