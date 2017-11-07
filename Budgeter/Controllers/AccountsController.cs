using Budgeter.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Budgeter.Controllers
{
    public class AccountsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        /// <summary>
        /// Gets all the accounts by household id. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("All")]
        public IEnumerable<Account> Get(int id)
        {
            return db.Database.SqlQuery<Account>("EXEC GetAccountsByHouseholdId @id", new SqlParameter("id", id));
        }

        /// <summary>
        /// Gets an account by its id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Single")]
        public Account GetSingleAccount(int id)
        {
            return db.Database.SqlQuery<Account>("EXEC GetAccountsById @id", new SqlParameter("id", id)).FirstAsync().Result;
        }

        /// <summary>
        /// Creates an account.
        /// </summary>
        /// <param name="a"></param>
        [HttpPost]
        [ActionName("Create")]
        public void Create(Account a)
        {
            db.Database.SqlQuery<int>("EXEC CreateAccount @name, @balance, @id", 
                new SqlParameter("name", a.Name), 
                new SqlParameter("balance", a.Balance), 
                new SqlParameter("id", a.HouseholdId)).First();
        }

        /// <summary>
        /// Edits an account.
        /// </summary>
        /// <param name="a"></param>
        [HttpPost]
        [ActionName("Edit")]
        public void Edit(Account a)
        {
            db.Database.SqlQuery<Account>("EXEC EditAccount @name, @id, @balance, @archived", 
                new SqlParameter("name", a.Name), 
                new SqlParameter("id", a.Id), 
                new SqlParameter("balance", a.Balance), 
                new SqlParameter("archived", a.Archived)).First();
        }

        /// <summary>
        /// Deletes an account.
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = await db.Accounts.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            db.Accounts.Remove(result);
            await db.SaveChangesAsync();

            return Ok();
        }
    }
}
