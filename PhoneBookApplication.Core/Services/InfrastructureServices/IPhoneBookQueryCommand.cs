using PhoneBookApplication.Core.Models;
using PhoneBookApplication.Core.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace PhoneBookApplication.Core.Services.InfrastructureServices
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPhoneBookQueryCommand<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync(RequestParams requestParams);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);

        Task<IPagedList<T>> GetPagedList(
            RequestParams requestParams,
            List<string> include = null
            );

        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}