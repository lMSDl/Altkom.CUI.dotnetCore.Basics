using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Basics.Models;

namespace Core.Basics.IServices
{
    public interface IBaseService<T> where T : Base
    {
        Task<ICollection<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
