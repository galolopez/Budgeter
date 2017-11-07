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
    public class ItemController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Gets all items by a budget id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("All")]
        public IEnumerable<Item> GetItemsByBudgetId(int id)
        {
            return db.Database.SqlQuery<Item>("EXEC GetItemsByBudgetId @id", new SqlParameter("id", id));
        }

        /// <summary>
        /// Gets an item by its id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Single")]
        public Item Get(int id)
        {
            return db.Database.SqlQuery<Item>("EXEC GetItemsById @id", new SqlParameter("id", id)).FirstAsync().Result;
        }

        /// <summary>
        /// Creates an item. 
        /// </summary>
        /// <param name="i"></param>
        [HttpPost]
        [ActionName("Create")]
        public void Create(Item i)
        {
            var result = db.Database.SqlQuery<Item>("EXEC CreateItem @name, @amount, @budgetId, @categoryId", 
                new SqlParameter("name", i.Name),
                new SqlParameter("amount", i.Amount),
                new SqlParameter("budgetId", i.BudgetId),
                new SqlParameter("categoryId", i.CategoryId));
        }

        /// <summary>
        /// Edits an item.
        /// </summary>
        /// <param name="i"></param>
        [HttpPost]
        [ActionName("Edit")]
        public void Edit(Item i)
        {
            var result = db.Database.SqlQuery<Item>("EXEC EditItem @name, @amount, @categoryId, @itemId",
                new SqlParameter("name", i.Name),
                new SqlParameter("amount", i.Amount),
                new SqlParameter("categoryId", i.CategoryId),
                new SqlParameter("itemId", i.Id));
        }

        /// <summary>
        /// Deletes an item. 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [ActionName("Delete")]
        public void Delete(int id)
        {
            var result = db.Database.SqlQuery<Item>("EXEC DeleteItem @id", new SqlParameter("id", id));
        }
    }
}