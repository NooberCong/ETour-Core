using System;
using System.Collections.Generic;

namespace Core.Services
{
    public class CrossYearRevenueReport: CrossYearBaseReport
    {
        public decimal[] ThisYearRevenues { get; } = new decimal[12];
        public decimal[] LastYearRevenues { get; } = new decimal[12];

        public void SetThisYearMonth(int month, decimal revenue)
        {
            Index index = month > StartMonth ? month - StartMonth : ^(EndMonth - month + 1);
            ThisYearRevenues[index] = revenue;
        }

        public void SetLastYearMonth(int month, decimal revenue)
        {
            Index index = month > StartMonth ? month - StartMonth : ^(EndMonth - month + 1);
            LastYearRevenues[index] = revenue;
        }
    }

}
