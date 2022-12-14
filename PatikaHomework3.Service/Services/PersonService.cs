using Microsoft.EntityFrameworkCore;
using PatikaHomework3.Data.Context;
using PatikaHomework3.Data.Model;
using PatikaHomework3.Service.IServices;

namespace PatikaHomework3.Service.Services
{
    public class PersonService : IPersonService
    {
        private readonly EfContext _efContext;

        public PersonService(EfContext EfContext)
        {
            _efContext = EfContext;
        }

        public async Task<Person> Add(Person entity)
        {
            try
            {
                _efContext.Person.AddAsync(entity);
                _efContext.SaveChanges();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

        }

        public async Task<string> Delete(int id)
        {
            var data = _efContext.Person.SingleOrDefault(x => x.Id == id);
            if (data == null)
                return null;
            try
            {
                _efContext.Person.Remove(data);
                _efContext.SaveChangesAsync();
                return "Success";

            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<Person> GetById(int id)
        {
            return _efContext.Person.SingleOrDefault(x => x.Id == id);

        }

        public async Task<Person> Update(Person entity)
        {
            try
            {
                _efContext.Person.Update(entity);
                _efContext.SaveChanges();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _efContext.Set<Person>().AsNoTracking().ToListAsync();

        }

        public async Task<Person> GetByAccountId(int id)
        {
            return _efContext.Person.SingleOrDefault(x => x.AccountId == id);
            
        }
    }
}
