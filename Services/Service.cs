using AutoMapper;
using TaskManagement.Repositories.Interfaces;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services
{
    public class Service<T, TDTO> : IService<T, TDTO> where T : class where TDTO : class
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper _mapper;

        public Service(IRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDTO>> GetAllAsync()
        {
            var entities
        }
    }
}