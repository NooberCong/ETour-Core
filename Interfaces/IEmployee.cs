using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Interfaces
{
    public interface IEmployee : IEntityWithKey<string>, ISoftDelete
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartWork { get; set; }
        public string[] Roles { get; set; }

    }
}
