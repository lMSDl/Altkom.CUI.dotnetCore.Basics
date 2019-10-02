using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.Basics.IServices;
using Core.Basics.Models;

namespace Core.Basics.Services
{
    public abstract class EntityService<T> : BaseService, IBaseService<T> where T : Base 
    {
        public EntityService(HttpClient httpClient) : base(httpClient) {
        }

        public async Task<T> AddAsync(T entity)
        {
            try{
                var response = await Client.PostAsJsonAsync<T>(Path, entity);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<T>();
            }
            catch
            {
                return null;
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<T>> GetAsync()
        {
            try {
                var response = await Client.GetAsync(Path);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<ICollection<T>>();
                }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<T> GetAsync(int id)
        {
            try {
                var response = await Client.GetAsync($"{Path}?id={id}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<T>();
                }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<bool> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
