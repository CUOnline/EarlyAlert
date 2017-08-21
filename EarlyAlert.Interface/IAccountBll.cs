using EarlyAlert.Model;

namespace EarlyAlert.Interface
{
    public interface IAccountBll : IBll<Account>
    {
        Account GetAccount(string id);
    }
}
