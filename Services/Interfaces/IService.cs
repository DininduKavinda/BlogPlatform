namespace TaskManagement.Services.Interfaces
{
    public interface IService<T, TDTO> where T : class where TDTO : class
    {
        Task<IEnumerable<TDTO>> GetAllAsync();
        Task<TDTO> GetByIdAsync(int id);
        Task<TDTO> AddAsync(TDTO dto);
        Task<TDTO> UpdateAsync(int id, TDTO dto);
        Task DeleteAsync(int id);
    }
}