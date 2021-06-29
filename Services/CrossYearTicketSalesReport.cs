using System;

namespace Core.Services
{
    public class CrossYearTicketSalesReport: CrossYearBaseReport
    {
        public int[] ThisYearTicketSales { get; } = new int[12];
        public int[] LastYearTicketSales { get; } = new int[12];

        public void SetThisYearMonth(int month, int sales)
        {
            Index index = month > StartMonth ? month - StartMonth : ^(EndMonth - month + 1);
            ThisYearTicketSales[index] = sales;
        }

        public void SetLastYearMonth(int month, int sales)
        {
            Index index = month > StartMonth ? month - StartMonth : ^(EndMonth - month + 1);
            LastYearTicketSales[index] = sales;
        }
    }
}
