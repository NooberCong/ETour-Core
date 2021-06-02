using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IQuery<TEntity>
    {
        public IQueryable<TEntity> Queryable { get; }
    }

    public interface IFindAsync<TEntity, TKey> where TEntity : IEntityWithKey<TKey>
    {
        public Task<TEntity> FindAsync(TKey key);
    }

    public interface IAdd<TEntity>
    {
        public Task<TEntity> AddAsync(TEntity entity);
    }

    public interface IUpdate<TEntity>
    {
        public Task<TEntity> UpdateAsync(TEntity entity);
    }

    public interface IDelete<TEntity>
    {
        public Task<TEntity> DeleteAsync(TEntity entity);
    }

    public interface IFilteredQuery<TEntity>
    {
        public IEnumerable<TEntity> QueryFiltered(Expression<Func<TEntity, bool>> filterExpression);
    }

    public interface IPagedQuery<TEntity>
    {
        public int PageCount(int pageSize);
        public IEnumerable<TEntity> QueryPaged(int pageNumber, int pageSize);
    }

    public interface IFilteredPagedQuery<TEntity> : IFilteredQuery<TEntity>, IPagedQuery<TEntity>
    {
        public int PageCount(Expression<Func<TEntity, bool>> filterExpression, int pageSize);
        public IEnumerable<TEntity> QueryFilteredPaged(Expression<Func<TEntity, bool>> filterExpression, int pageNumber, int pageSize);
    }

    public interface ITourRepository : IQuery<Tour>, IAdd<Tour>, IDelete<Tour>, IUpdate<Tour>, IFilteredPagedQuery<Tour>, IFindAsync<Tour, int>
    {

    }

    public interface ITripRepository : IQuery<Trip>, IFindAsync<Trip, int>, IAdd<Trip>, IUpdate<Trip>, IFilteredPagedQuery<Trip>
    {

    }

    public interface ITourReviewRepository : IQuery<TourReview>, IAdd<TourReview>, IDelete<TourReview>, IFilteredPagedQuery<TourReview>
    {

    }

    public interface ICustomerRepository : IQuery<Customer>, IFindAsync<Customer, string>, IAdd<Customer>, IDelete<Customer>, IUpdate<Customer>
    {

    }

    public interface IBookingRepository : IQuery<Booking>, IAdd<Booking>, IUpdate<Booking>, IFilteredPagedQuery<Booking>
    {

    }

    public interface IItineraryRepository : IQuery<Itinerary>, IFindAsync<Itinerary, int>, IAdd<Itinerary>, IUpdate<Itinerary>, IDelete<Itinerary>, IFilteredQuery<Itinerary>
    {

    }

    public interface IQuestionRepository : IQuery<Question>, IAdd<Question>, IUpdate<Question>, IDelete<Question>, IFilteredPagedQuery<Question>
    {

    }

    public interface IAnswerRepository : IQuery<Answer>, IAdd<Answer>, IUpdate<Answer>, IDelete<Answer>, IFilteredQuery<Answer>
    {

    }

    public interface IDiscountRepository : IFindAsync<Discount, int>, IQuery<Discount>, IAdd<Discount>, IUpdate<Discount>, IDelete<Discount>, IFilteredPagedQuery<Discount>
    {

    }

    public interface ILogRepository : IQuery<Log>, IAdd<Log>, IFilteredPagedQuery<Log>
    {

    }

    public interface IPostRepository<TPost, TEmployee> : IQuery<TPost>, IFindAsync<TPost, int>, IAdd<TPost>, IUpdate<TPost>, IDelete<TPost>, IFilteredPagedQuery<TPost> where TEmployee : IEmployee where TPost : IPost<TEmployee>
    {

    }

    public interface ITripDiscountRepository : IAdd<TripDiscount>, IDelete<TripDiscount>
    {

    }

    public interface IOrderRepository : IQuery<Order>, IFindAsync<Order, int>, IAdd<Order>, IUpdate<Order>, IPagedQuery<Order>
    {

    }
}
