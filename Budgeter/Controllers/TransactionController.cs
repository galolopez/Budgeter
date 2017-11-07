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
        [ActionName("ByAccount")]
        public IEnumerable<Transaction> TransactionsByAccount(int id)
        {
            return db.Database.SqlQuery<Transaction>("EXEC GetTransactionsByAccountId @id", new SqlParameter("id", id)).ToList();
        }

        /// <summary>
        /// Gets all transactions by a category id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ByCategory")]
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
        [ActionName("Create")]
        public void Create(Transaction t)
        {
            db.Database.SqlQuery<int>("EXEC CreateTransaction @name, @amount, @accountId, @categoryId, @date, @description",
                new SqlParameter("name", t.Name),
                new SqlParameter("amount", t.Amount),
                new SqlParameter("accountId", t.AccountId),
                new SqlParameter("categoryId", t.CategoryId),                
                new SqlParameter("date", t.Date),
                new SqlParameter("description", t.Description)).First();
        }

        /// <summary>
        /// Edits a transaction. 
        /// </summary>
        /// <param name="t"></param>
        [HttpPost]
        [ActionName("Edit")]
        public void Edit(Transaction t)
        {
            db.Database.SqlQuery<Transaction>("EXEC EditTransaction @name, @amount, @date, @description, @status, @categoryId, @transactionId, @accountId", 
                new SqlParameter("name", t.Name),
                new SqlParameter("amount", t.Amount),
                new SqlParameter("date", t.Date),
                new SqlParameter("description", t.Description),
                new SqlParameter("status", t.Status),
                new SqlParameter("categoryId", t.CategoryId),
                new SqlParameter("transactionId", t.Id),
                new SqlParameter("accountId", t.AccountId)).First();
        }

        /// <summary>
        /// Deletes a transaction. 
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        [ActionName("Delete")]
        public void Delete(int id)
        {
            db.Database.SqlQuery<int>("EXEC DeleteTransaction @transactionId", new SqlParameter("transactionId", id)).FirstOrDefault();
        }
    }
}
