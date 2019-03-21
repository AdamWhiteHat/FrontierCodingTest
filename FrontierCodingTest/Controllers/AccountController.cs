using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using FrontierCodingTest.Models;

namespace FrontierCodingTest.Controllers
{
    public class AccountController : Controller
    {
        public async Task<ActionResult> Accounts()
        {
            try
            {
                List<AccountModel> accounts = await ControllerHelper.HttpGetDataAsync<List<AccountModel>>();

                AccountsContainerModel collectionModel = new AccountsContainerModel();
                collectionModel.ActiveAccounts = accounts.Where(accnt => accnt.AccountStatusId == AccountStatus.Active).ToList();
                collectionModel.OverdueAccounts = accounts.Where(accnt => accnt.AccountStatusId == AccountStatus.Overdue).ToList();
                collectionModel.InactiveAccounts = accounts.Where(accnt => accnt.AccountStatusId == AccountStatus.Inactive).ToList();

                return View("Accounts", collectionModel);
            }
            // Always validate and sanitize your input.
            // Handle exceptions when you can. If you do not know how to handle the exception, let it bubble up.
            // Use a global unhandled exception handler to prevent exceptions and stack trace information from being shown to the user.
            catch (AggregateException)
            {
                // Presumably there will be a unified strategy for logging exceptions/errors. The following line reflects this. 
                //Logging.Exception($"{nameof(AggregateException)} caught in {nameof(AccountController)}.{nameof(Accounts)}");
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
