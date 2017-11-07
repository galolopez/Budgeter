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
        public IEnumerable<Category> Get(string name)
        {
            return db.Database.SqlQuery<Category>("EXEC GetCategoriesByHouseholdName @name", new SqlParameter("name", name));
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
        public void Create(string name)
        {
            var result = db.Database.SqlQuery<Category>("EXEC CreateCategory @name", new SqlParameter("name", name));
        }

        /// <summary>
        /// Edits a category, given a name.
        /// </summary>
        /// <param name="name"></param>
        [HttpPost]
        public void Edit(string name)
        {
            var result = db.Database.SqlQuery<Category>("EXEC EditCategory @name", new SqlParameter("name", name));
        }

        /// <summary>
        /// Deletes a category. 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Delete(int id)
        {
            var result = db.Database.SqlQuery<Category>("EXEC DeleteCategory @id", new SqlParameter("id", id));
        }
    }
}
