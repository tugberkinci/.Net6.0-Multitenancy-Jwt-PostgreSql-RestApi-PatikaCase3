using PatikaHomework3.Data.Model;

namespace PatikaHomework3.Service.IServices
{
    public interface IPersonService : IGenericService<Person>
    {
        Task<Person> GetByAccountId(int id);
        
    }
}
