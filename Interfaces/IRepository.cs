using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFirstOrDefaultAsync<TEntity, TKey> where TEntity: IEntityWithKey<TKey>, new()
    {
        public Task<TEntity> FirstOrDefaultAsync(TKey key);
    }

    public interface IAdd<TEntity> where TEntity: class, new()
    {
        public Task<TEntity> AddAsync(TEntity entity);
    }

    public interface IUpdate<TEntity> where TEntity : class, new()
    {
        public Task<TEntity> UpdateAsync(TEntity entity);
    }

    public interface IDelete<TEntity> where TEntity : class, new()
    {
        public Task<TEntity> DeleteAsync(TEntity entity);
    }

    public interface IFilteredQuery<TEntity> where TEntity : class, new()
    {
        public IEnumerable<TEntity> QueryFiltered(Expression<Func<TEntity, bool>> filterExpression);
    }

    public interface IPagedQuery<TEntity> where TEntity : class, new()
    {
        public int PageCount(int pageSize);
        public IEnumerable<TEntity> QueryPaged(int pageNumber, int pageSize);
    }

    public interface IFilteredPagedQuery<TEntity>: IFilteredQuery<TEntity>, IPagedQuery<TEntity> where TEntity : class, new()
    {
        public IEnumerable<TEntity> QueryFilteredPaged(Expression<Func<TEntity, bool>> filterExpression, int pageNumber, int pageSize);
    }

    public interface ITourRepository: IAdd<Tour>, IDelete<Tour>, IUpdate<Tour>, IFilteredPagedQuery<Tour>, IFirstOrDefaultAsync<Tour, int>
    {

    }

    public interface ITripRepository : IAdd<Trip>, IUpdate<Trip>, IFilteredQuery<Trip>
    {

    }

    public interface ITourReviewRepository : IAdd<TourReview>, IDelete<TourReview>, IFilteredPagedQuery<TourReview>
    {

    }

    public interface IUserRepository : IAdd<User>, IDelete<User>, IUpdate<User>, IFilteredPagedQuery<User>
    {

    }

    public interface IBookingRepository: IAdd<Booking>, IUpdate<Booking>, IFilteredPagedQuery<Booking>
    {

    }

    public interface IItineraryRepository: IAdd<Itinerary>, IUpdate<Itinerary>, IDelete<Itinerary>, IFilteredQuery<Itinerary>
    {

    }

    public interface IQuestionRepository: IAdd<Question>, IUpdate<Question>, IDelete<Question>, IFilteredPagedQuery<Question>
    {

    }

    public interface IAnswerRepository: IAdd<Answer>, IUpdate<Answer>, IDelete<Answer>, IFilteredQuery<Answer>
    {

    }

    public interface IDiscountRepository: IAdd<Discount>, IUpdate<Discount>, IDelete<Discount>, IFilteredPagedQuery<Discount>
    {

    }

    public interface ILogRepository: IAdd<Log>, IFilteredPagedQuery<Log>
    {

    }
}
