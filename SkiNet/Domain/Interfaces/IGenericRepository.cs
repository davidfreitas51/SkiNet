using SkiNet.Domain.Entities;
using SkiNet.Domain.Specifications;

namespace SkiNet.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, int itemsPerPage, int pageNumber, string search);
    }
}
