using GnosisNet.Entities.Entities;
using GnosisNet.Repository.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GnosisNet.Repository.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        void Add(T enitity);
        Task AddAsync(T enitity, string userId);
        void Update(T entity);
        Task UpdateAsync(T entity, string userId);
        void Delete(T entity);
        Task DeleteAsync(T entity, string userId);
        void AddRange(IEnumerable<T> entity);
        Task AddRangeAsync(IEnumerable<T> entities, string userId);
        void UpdateRange(IEnumerable<T> entity);
        void RemoveRange(IEnumerable<T> entity);
        IEnumerable<T> FindWithSpecificationPattern(ISpecification<T> specification = null!);
        Task<bool> ContainsAsync(ISpecification<T> specification = null!);
        Task<bool> ContainsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(ISpecification<T> specification = null!);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        IReadOnlyList<T> ExecuteStored(string query, params object[] parameters);
        Task<IQueryable<T>> Query(Expression<Func<T, bool>> predicate);
        Task<IList<T>> GetListItems(Expression<Func<T, bool>> predicate, params string[] navigationProperties);
        Task<T> GetSingleItemsAsync(Expression<Func<T, bool>> predicate, params string[] navigationProperties);
        T GetSingleItems(Expression<Func<T, bool>> predicate, params string[] navigationProperties);

    }
}
