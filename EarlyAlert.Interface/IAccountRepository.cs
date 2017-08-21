using EarlyAlert.Model;

namespace EarlyAlert.Interface
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetAccount(string id);
    }
}
