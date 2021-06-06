﻿using Core.Interfaces;

namespace Core.Entities
{
    public class PointLog : AuthoredTrackedEntityWithKey<Customer, int, string>
    {
        public int Amount { get; set; }
        public string Description { get; set; }
        public string Trigger { get; set; }
    }
}
