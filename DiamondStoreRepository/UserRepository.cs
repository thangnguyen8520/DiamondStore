using DiamondDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository
{
    public class UserRepository
    {
        private readonly UserDAO userDAO = null;
        public UserRepository()
        {
            if (accountDAO == null)
            {
                accountDAO = new AccountDAO();
            }
        }
        public Account GetAccount(string userName, string password) => AccountDAO.Instance.GetAccount(userName, password);
        //{
        //    return accountDAO.GetAccount(userName, password);
        //}
    }
}
