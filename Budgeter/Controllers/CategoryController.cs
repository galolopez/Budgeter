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
    public class CategoryController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Gets all the categories by a household name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("All")]
        public IEnumerable<Category> GetCategoriesByHouseholdId(int id)
        {
            return db.Database.SqlQuery<Category>("EXEC GetCategoriesByHouseholdId @id", new SqlParameter("id", id));
        }

        /// <summary>
        /// Gets a category by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Single")]
        public Category Get(int id)
        {
            return db.Database.SqlQuery<Category>("EXEC GetCategoriesById @id", new SqlParameter("id", id)).FirstAsync().Result;
        }

        /// <summary>
        /// Creates a category, given a name.
        /// </summary>
        /// <param name="name"></param>
        [HttpPost]
        [ActionName("Create")]
        public void Create(Category c)
        {
            db.Database.SqlQuery<int>("EXEC CreateCategory @name, @expenseTF, @householdId", 
                new SqlParameter("name", c.Name),
                new SqlParameter("expenseTF", c.ExpenseTF),
                new SqlParameter("householdId", c.HouseholdId)).First();
        }

        /// <summary>
        /// Edits a category, given a name.
        /// </summary>
        /// <param name="name"></param>
        [HttpPost]
        [ActionName("Edit")]
        public void Edit(Category c)
        {
            db.Database.SqlQuery<Category>("EXEC EditCategory @name, @expenseTF, @id", 
                new SqlParameter("name", c.Name),
                new SqlParameter("expenseTF", c.ExpenseTF),
                new SqlParameter("id", c.Id)).First();
        }

        /// <summary>
        /// Deletes a category. 
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = await db.Categories.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            db.Categories.Remove(result);
            await db.SaveChangesAsync();

            return Ok();
        }
    }
}
