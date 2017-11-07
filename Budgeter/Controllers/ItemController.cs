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
            db.Database.SqlQuery<int>("EXEC CreateItem @name, @amount, @budgetId, @categoryId", 
                new SqlParameter("name", i.Name),
                new SqlParameter("amount", i.Amount),
                new SqlParameter("budgetId", i.BudgetId),
                new SqlParameter("categoryId", i.CategoryId)).First();
        }

        /// <summary>
        /// Edits an item.
        /// </summary>
        /// <param name="i"></param>
        [HttpPost]
        [ActionName("Edit")]
        public void Edit(Item i)
        {
            db.Database.SqlQuery<Item>("EXEC EditItem @name, @amount, @categoryId, @itemId",
                new SqlParameter("name", i.Name),
                new SqlParameter("amount", i.Amount),
                new SqlParameter("categoryId", i.CategoryId),
                new SqlParameter("itemId", i.Id)).First();
        }

        /// <summary>
        /// Deletes an item. 
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = await db.Items.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            db.Items.Remove(result);
            await db.SaveChangesAsync();

            return Ok();
        }
    }
}