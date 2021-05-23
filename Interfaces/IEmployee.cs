using System;

namespace Core.Interfaces
{
    public interface IEmployee: IEntityWithKey<string>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public DateTime StartWork { get; set; }

    }
}
