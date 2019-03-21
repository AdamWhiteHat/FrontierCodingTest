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

                // It was tempting to put the linq where clauses directly in the view, thereby avoiding having to create this container model.
                // However, that would technically be breaking the MVC pattern by putting logic in the view.
                // The rule of thumb here is, is to imagine the view will be handled by UX or graphic designer people,
                // and is unreasonable to expect them to deal with linq, session state or any kind of logic really.
                // Even the foreach loops that I did put in the view is pushing it.
                // I kept all the diplay formatting logic in the model and out of the view for the same reasons.
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
