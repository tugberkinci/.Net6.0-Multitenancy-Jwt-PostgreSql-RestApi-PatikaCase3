using PatikaHomework3.Data.Model;

namespace PatikaHomework3.Helpers
{
    public interface IAccountHelper
    {
        Account GetCurrentUser();
    }
    public  class AccountHelper : IAccountHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public  Account GetCurrentUser()
        {
            return (Account)_httpContextAccessor.HttpContext.Items["Account"];
        }
    }
}
