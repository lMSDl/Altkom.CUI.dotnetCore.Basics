using System.Linq;
using System.Collections.Generic;
using Bogus;
using Core.Basics.IServices;
using Core.Basics.Models;
using System.Threading.Tasks;

namespace Core.Basics.FakeServices
{
    public abstract class FakeBaseService<T> : IBaseService<T> where T : Base
    {
        protected readonly ICollection<T> entities;
        private int _lastId;

        public FakeBaseService(Faker<T> faker, int count)
        {
            entities = faker.Generate(count);
            _lastId = entities.Max(x => x.Id);
        }

        public Task<T> AddAsync(T entity)
        {
            return AddAsync(entity, true);
        }

        private Task<T> AddAsync(T entity, bool generateId)
        {
            if(generateId)
                entity.Id = ++_lastId;
            entities.Add(entity);
            return Task.FromResult(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if(entity == null)
                return false;
            entities.Remove(entity);
            return true;
        }

        public Task<ICollection<T>> GetAsync()
        {
            return Task.FromResult((ICollection<T>)entities.ToList());
        }

        public Task<T> GetAsync(int id)
        {
            return Task.FromResult(entities.SingleOrDefault(x => x.Id == id));
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            if(!await DeleteAsync(entity.Id))
                return false;
            await AddAsync(entity, false);
            return true;
        }
    }
}
