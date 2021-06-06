using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Core.Interfaces
{
    public interface IEmployee : IEntityWithKey<string>, ISoftDelete
    {
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public List<IRole> Roles { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime DOB { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartWork { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public void ValidateNewRoles(string[] roleIds);
        public bool IsAdmin();
    }
}
