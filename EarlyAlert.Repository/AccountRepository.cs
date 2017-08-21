using System.Data;
using EarlyAlert.Interface;
using EarlyAlert.Model;

namespace EarlyAlert.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public Account GetAccount(string accountId)
        {
            var canvas = new CanvasRedShift();
            var canvasAccount = new Account();
            var sql = $"SELECT Id, Name FROM account_dim WHERE Id = {accountId}";
            var terms = canvas.GetCanvasData(sql);

            foreach (DataRow row in terms.Tables[0].Rows)
            {
                canvasAccount.Id = row["id"].ToString();
                canvasAccount.Name = row["name"].ToString();
            }

            return canvasAccount;
        }
    }
}
