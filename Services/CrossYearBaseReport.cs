using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CrossYearBaseReport
    {
        public static string[] MonthNames = new string[] {
            "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Sep", "Nov", "Dec"
        };

        public int StartMonth { get; set; }
        public int EndMonth { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }

        public List<string> Months
        {
            get
            {
                List<string> names = new();
                int m = StartMonth - 1;
                for (int i = 0; i < 12; i++)
                {
                    names.Add(MonthNames[m]);
                    m++;
                    if (m >= 12)
                    {
                        m = 0;
                    }
                }
                return names;
            }
        }
    }
}
