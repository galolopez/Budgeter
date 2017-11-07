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
    public class TransactionController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        /// <summary>
        /// Gets all transactions by an account id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Transaction> TransactionsByAccount(int id)
        {
            return db.Database.SqlQuery<Transaction>("EXEC GetTransactionsByAccountId @id", new SqlParameter("accountId", id));
        }

        /// <summary>
        /// Gets all transactions by a category id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Transaction> TransactionsByCategory(int id)
        {
            return db.Database.SqlQuery<Transaction>("EXEC GetTransactionsByCategoryId @id", new SqlParameter("id", id));
        }

        /// <summary>
        /// Gets a single transaction by its id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Single")]
        public Transaction Get(int id)
        {
            return db.Database.SqlQuery<Transaction>("EXEC GetTransactionsById @id", new SqlParameter("Id", id)).FirstAsync().Result;
        }

        /// <summary>
        /// Creates a transaction. 
        /// </summary>
        /// <param name="t"></param>
        [HttpPost]
        public void Create(Transaction t)
        {
            var result = db.Database.SqlQuery<Transaction>("EXEC CreateTransaction @amount, @date, @description, @accountId, @categoryId",
                new SqlParameter("accountId", t.AccountId),
                new SqlParameter("categoryId", t.CategoryId),
                new SqlParameter("description", t.Description),
                new SqlParameter("amount", t.Amount),
                new SqlParameter("date", t.Date));
        }

        /// <summary>
        /// Edits a transaction. 
        /// </summary>
        /// <param name="t"></param>
        [HttpPost]
        public void Edit(Transaction t)
        {
            var result = db.Database.SqlQuery<Transaction>("EXEC EditTransaction @name, @amount, @date, @description, @status, @categoryId, @transactionId", 
                new SqlParameter("name", t.Name),
                new SqlParameter("amount", t.Amount),
                new SqlParameter("date", t.Date),
                new SqlParameter("description", t.Description),
                new SqlParameter("status", t.Status),
                new SqlParameter("categoryId", t.CategoryId),
                new SqlParameter("transactionId", t.Id));
        }

        /// <summary>
        /// Deletes a transaction. 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Delete(int id)
        {
            var result = db.Database.SqlQuery<Transaction>("EXEC DeleteTransaction @id", new SqlParameter("id", id));
        }
    }
}
