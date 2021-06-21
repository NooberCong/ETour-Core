using Core.Entities;
using Core.Interfaces;

namespace Core.Value_Objects
{
    public record BlogFilterParams
    {
        public string Keyword { get; set; }
        public IPost<IEmployee>.PostCategory? Category { get; set; }
    }
}
