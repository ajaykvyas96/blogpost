using GnosisNet.Data;
using GnosisNet.Entities.Entities;
using GnosisNet.Repository.Interface;
using GnosisNet.Repository.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GnosisNet.Repository.Service
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly GnosisDbContext _context;
        public GenericRepository(GnosisDbContext context)
        {
            _context = context;

        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
        public void Add(T enitity)
        {
            _context.Set<T>().Add(enitity);
            // _context.SaveChanges();
        }
        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            // _context.SaveChanges();

        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            // _context.SaveChanges();
        }
        public async Task AddAsync(T entity, string userId)
        {
            _context.Set<T>().Add(entity);
            _context.Entry(entity).State = EntityState.Added;
            //    await _context.SaveChangesAsync(userId);
        }
        public async Task UpdateAsync(T entity, string userId)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            //    await _context.SaveChangesAsync(userId);
        }
        public async Task DeleteAsync(T entity, string userId)
        {
            _context.Set<T>().Remove(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            //    await _context.SaveChangesAsync(userId);
        }
        public IEnumerable<T> FindWithSpecificationPattern(ISpecification<T> specification = null)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
        }
        public void AddRange(IEnumerable<T> entity)
        {
            _context.Set<T>().AddRange(entity);
        }
        public async Task AddRangeAsync(IEnumerable<T> entities, string userId)
        {
            _context.Set<T>().AddRange(entities);
            _context.Entry(entities).State = EntityState.Added;
            await _context.AddRangeAsync(userId);
        }
        public void UpdateRange(IEnumerable<T> entity)
        {
            _context.Set<T>().UpdateRange(entity);
        }
        public void RemoveRange(IEnumerable<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
        }
        public async Task<bool> ContainsAsync(ISpecification<T> specification = null)
        {
            return await CountAsync(specification) > 0 ? true : false;
        }
        public async Task<bool> ContainsAsync(Expression<Func<T, bool>> predicate)
        {
            return await CountAsync(predicate) > 0 ? true : false;
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).CountAsync();
        }
        public IReadOnlyList<T> ExecuteStored(string query, params object[] parameters)
        {
            var r = _context.Set<T>().FromSqlRaw(query, parameters);
            return r.ToList();
        }
        public async Task<IQueryable<T>> Query(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }
        public async Task<IList<T>> GetListItems(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
        {
            List<T> list;
            // using (var ctx = new _context())
            // {
            var query = _context.Set<T>().AsQueryable();
            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);//got to reaffect it.
            list = query.Where(predicate).ToList<T>();
            //}
            return list;
        }
        public async Task<T> GetSingleItemsAsync(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);//got to reaffect it.
            return await query.FirstOrDefaultAsync(predicate);
        }
        public T GetSingleItems(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);//got to reaffect it.
            return query.FirstOrDefault(predicate);
        }
    }
}
