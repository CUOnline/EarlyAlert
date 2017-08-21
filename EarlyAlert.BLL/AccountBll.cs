using EarlyAlert.Interface;
using EarlyAlert.Model;

namespace EarlyAlert.BLL
{
    public class AccountBll : IAccountBll
    {
        private readonly IAccountRepository accountRepository;

        public AccountBll(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public Account GetAccount(string id)
        {
            return accountRepository.GetAccount(id);
        }
    }
}
