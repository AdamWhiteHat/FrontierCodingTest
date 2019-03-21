using System.Collections.Generic;

namespace FrontierCodingTest.Models
{
    public class AccountsContainerModel
    {
        public List<AccountModel> ActiveAccounts { get; set; }
        public List<AccountModel> OverdueAccounts { get; set; }
        public List<AccountModel> InactiveAccounts { get; set; }
    }
}
