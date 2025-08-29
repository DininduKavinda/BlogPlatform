using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagement.Repositories.Interfaces;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services
{
    public class Service<T, TDTO> : IService<T, TDTO> where T : class where TDTO : class
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper _mapper;

        protected int GetEntityID(T entity)
        {
            return (int)entity.GetType().GetProperty("Id")?.GetValue(entity);
        }

        public Service(IRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDTO>>(entities);
        }

        public async Task<TDTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {           
                throw new KeyNotFoundException();
            }
            return _mapper.Map<TDTO>(entity);
        }

        public virtual async Task<TDTO> AddAsync(TDTO dto)
        {
            var entity = _mapper.Map<T>(dto);
            await _repository.AddAsync(entity);
            var createdEntity = await _repository.GetByIdAsync(GetEntityID(entity));
            return _mapper.Map<TDTO>(createdEntity);
        }

        public virtual async Task<TDTO> UpdateAsync(int id, TDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException();
            }
            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<TDTO>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException();
            }
            await _repository.DeleteAsync(id);
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.AnyAsync(predicate); 
        }
    }
}