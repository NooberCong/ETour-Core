using Core.Entities;
using Core.Interfaces;
using Core.Value_Objects;
using FuzzySharp;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class BlogFilterService
    {
        public IEnumerable<IPost<T>> ApplyFilter<T>(IEnumerable<IPost<T>> posts, BlogFilterParams filterParams) where T: IEmployee
        {
            return posts.Where(post =>
               (filterParams.Category == null || (int)post.Category == (int)filterParams.Category)
               && (string.IsNullOrWhiteSpace(filterParams.Keyword) || Fuzz.PartialTokenSetRatio(string.Join(" ", post.Tags), filterParams.Keyword) > 50 || Fuzz.PartialTokenSetRatio(post.Title, filterParams.Keyword) > 50)
               ).OrderBy(post => string.IsNullOrWhiteSpace(filterParams.Keyword) ? 1: - Fuzz.PartialTokenSetRatio(post.Title, filterParams.Keyword) * 1.5 - Fuzz.PartialTokenSetRatio(string.Join(" ", post.Tags), filterParams.Keyword));
        }
    }
}
