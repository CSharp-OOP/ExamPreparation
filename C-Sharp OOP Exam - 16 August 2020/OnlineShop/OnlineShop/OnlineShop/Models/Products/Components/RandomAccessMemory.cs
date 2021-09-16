using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Models.Products
{
    public class RandomAccessMemory : Component
    {
        private const double RandomAccessMemoryOverallPerformance = 1.20;
        public RandomAccessMemory(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation) 
            : base(id, manufacturer, model, price, overallPerformance* RandomAccessMemoryOverallPerformance, generation)
        {
        }
    }
}
