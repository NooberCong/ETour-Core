using Core.Interfaces;
using System;

namespace Core.Entities
{
    public class CustomerInfo: EntityWithKey<int>
    {
        public string Name { get; set; }
        public CustomerSex Sex { get; set; }
        public DateTime DOB { get; set; }
        public CustomerAgeGroup AgeGroup { get; set; }


        public enum CustomerSex
        {
            Male, Female
        }

        public enum CustomerAgeGroup
        {
            Adult,
            Youth,
            Children,
            Baby
        }
    }
}
