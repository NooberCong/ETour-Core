using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    class PointLog : TrackedEntityWithKey<int>
    {
        public int Amount { get; set; }
        public string Description { get; set; }
        public string Trigger { get; set; }
    }
}
