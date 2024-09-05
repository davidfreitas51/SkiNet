using Microsoft.EntityFrameworkCore;
using SkiNet.Domain.Entities;
using SkiNet.Domain.Interfaces;
using SkiNet.Domain.Specifications;
using SkiNet.Infrastructure.Data;
using System.Linq.Expressions;

namespace SkiNet.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, int itemsPerPage, int pageNumber, string search)
        {
            var query = ApplySpecifications(spec);

            if (!string.IsNullOrEmpty(search))
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                Expression predicate = Expression.Constant(false);

                var stringProperties = typeof(T).GetProperties()
                    .Where(p => p.PropertyType == typeof(string));

                foreach (var property in stringProperties)
                {
                    var propertyAccess = Expression.Property(parameter, property.Name);
                    var searchExpression = Expression.Constant(search, typeof(string));
                    var containsExpression = Expression.Call(propertyAccess, containsMethod, searchExpression);
                    predicate = Expression.OrElse(predicate, containsExpression);
                }

                var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
                query = query.Where(lambda);
            }

            var skip = (pageNumber - 1) * itemsPerPage;
            query = query.Skip(skip).Take(itemsPerPage);

            return await query.ToListAsync();
        }



        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
