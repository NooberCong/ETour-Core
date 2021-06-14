using Core.Interfaces;
using System;

namespace Core.Entities
{
    public class CustomerInfo : EntityWithKey<int>
    {
        public string Name { get; set; }
        public CustomerSex Sex { get; set; }
        public DateTime DOB { get; set; }
        public CustomerAgeGroup AgeGroup { get; set; }

        public static CustomerAgeGroup AgeGroupFor(DateTime dob)
        {
            int age = CalculateAge(dob);

            if (age < 2)
            {
                return CustomerAgeGroup.Baby;
            }

            if (age < 5)
            {
                return CustomerAgeGroup.Children;
            }

            if (age < 12)
            {
                return CustomerAgeGroup.Youth;
            }

            return CustomerAgeGroup.Adult;
        }

        public static Tuple<DateTime, DateTime> DobRangeFor(CustomerAgeGroup ageGroup)
        {
            return ageGroup switch
            {
                CustomerAgeGroup.Adult => Tuple.Create(DateTime.Today.AddYears(-70), DateTime.Today.AddYears(-12).AddDays(-1)),
                CustomerAgeGroup.Youth => Tuple.Create(DateTime.Today.AddYears(-12), DateTime.Today.AddYears(-5).AddDays(-1)),
                CustomerAgeGroup.Children => Tuple.Create(DateTime.Today.AddYears(-5), DateTime.Today.AddYears(-2).AddDays(-1)),
                CustomerAgeGroup.Baby => Tuple.Create(DateTime.Today.AddYears(-2), DateTime.Today.AddDays(-1)),
                _ => throw new InvalidOperationException("Unknown age group")
            };
        }


        private static int CalculateAge(DateTime dob)
        {
            var age = DateTime.Today.Year - dob.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (dob.Date > DateTime.Today.AddYears(-age))
            {
                age -= 1;
            }

            return age;
        }

        public enum CustomerSex
        {
            Male,
            Female
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
