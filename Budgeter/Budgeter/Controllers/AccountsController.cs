using Budgeter.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            //string username = Request.Headers.GetValues("Username").First();
            //string household = Request.Headers.GetValues("Household").First();
            //int householdId = Int32.Parse(household);
            
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
            var result = db.Database.SqlQuery<int>("EXEC CreateAccount @name, @balance, @id", new SqlParameter("name", a.Name), new SqlParameter("balance", a.Balance), new SqlParameter("id", a.HouseholdId)).First();
        }

        /// <summary>
        /// Edits an account.
        /// </summary>
        /// <param name="a"></param>
        [HttpPost]
        [ActionName("Edit")]
        public void Edit(Account a)
        {
            var result = db.Database.SqlQuery<Account>("EXEC EditAccount @name, @id, @balance, @archived", new SqlParameter("name", a.Name), new SqlParameter("id", a.Id), new SqlParameter("balance", a.Balance), new SqlParameter("archived", a.Archived)).First();
        }

        /// <summary>
        /// Archives an account. 
        /// </summary>
        /// <param name="a"></param>
        [HttpPost]
        [ActionName("Archive")]
        public void Archive(Account a)
        {
            var result = db.Database.SqlQuery<int>("EXEC ArchiveAccount @id", new SqlParameter("id", a.Id)).First();
        }

        /// <summary>
        /// Unarchives an account. 
        /// </summary>
        /// <param name="a"></param>
        [HttpPost]
        [ActionName("Unarchive")]
        public void Unarchive(Account a)
        {
            var result = db.Database.SqlQuery<int>("EXEC UnarchiveAccount @id", new SqlParameter("id", a.Id)).First();
        }
    }
}
