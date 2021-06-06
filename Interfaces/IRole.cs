using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRole: IEntityWithKey<string>
    {
        public static readonly string ADMIN_ID = "admin";
        public string Name { get; set; }
        public List<string> Permissions { get; set; }
    }
}
