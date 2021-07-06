using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public ICollection<Trip> Trips { get; set; }

        public void ValidateNewRoles(string[] roleIds)
        {
            var isAdmin = Roles.Any(r => r.ID == IRole.ADMIN_ID);

            // Prevent overposting
            if (!isAdmin && roleIds.Contains(IRole.ADMIN_ID) || isAdmin && !roleIds.Contains(IRole.ADMIN_ID))
            {
                throw new Exception("Invalid role assignment");
            }
        }

        public bool IsAdmin()
        {
            return Roles.Any(r => r.ID == IRole.ADMIN_ID);
        }
    }
}
