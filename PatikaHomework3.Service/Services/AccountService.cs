using Microsoft.EntityFrameworkCore;
using PatikaHomework3.Data.Context;
using PatikaHomework3.Data.Model;
using PatikaHomework3.Service.IServices;

namespace PatikaHomework3.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly EfContext _efContext;

        public AccountService(EfContext EfContext)
        {
            _efContext = EfContext;
        }

        public async Task<Account> Add(Account entity)
        {
            try
            {
                _efContext.Account.AddAsync(entity);
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
            var data = _efContext.Account.SingleOrDefault(x => x.Id == id);
            if (data == null)
                return null;
            try
            {
                _efContext.Account.Remove(data);
                _efContext.SaveChangesAsync();
                return "Success";

            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<Account> GetById(int id)
        {
            return _efContext.Account.SingleOrDefault(x => x.Id == id);

        }

        public async Task<Account> Update(Account entity)
        {
            try
            {
                _efContext.Account.Update(entity);
                _efContext.SaveChanges();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _efContext.Set<Account>().AsNoTracking().ToListAsync();

        }
    }
}
