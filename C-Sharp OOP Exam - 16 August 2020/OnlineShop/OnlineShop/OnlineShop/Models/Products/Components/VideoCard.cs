﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Models.Products
{
    public class VideoCard : Component
    {
        private const double VideoCardOverallPerformance = 1.15;
        public VideoCard(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation) 
            : base(id, manufacturer, model, price, overallPerformance* VideoCardOverallPerformance, generation)
        {
        }
    }
}