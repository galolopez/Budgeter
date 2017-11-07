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
    public class BudgetController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Gets all the budgets for a single household id.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("All")]
        public IEnumerable<Budget> GetBudgets(int id)
        {
            return db.Database.SqlQuery<Budget>("EXEC GetBudgetsByHouseholdId @id", new SqlParameter("id", id));
        }

        /// <summary>
        /// Gets a budget by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Single")]
        public Budget GetBudget(int id)
        {
            return db.Database.SqlQuery<Budget>("EXEC GetBudgetsById @id", new SqlParameter("id", id)).FirstAsync().Result;
        }

        /// <summary>
        /// Creates a budget.
        /// </summary>
        /// <param name="b"></param>
        [HttpPost]
        [ActionName("Create")]
        public void Create(Budget b)
        {
            db.Database.SqlQuery<int>("EXEC CreateBudget @name, @id", new SqlParameter("name", b.Name), new SqlParameter("id", b.HouseholdId)).First();
        }

        /// <summary>
        /// Edits a budget. 
        /// </summary>
        /// <param name="b"></param>
        [HttpPost]
        [ActionName("Edit")]
        public void Edit(Budget b)
        {
            var result = db.Database.SqlQuery<Budget>("EXEC EditBudget @name, @id", new SqlParameter("name", b.Name), new SqlParameter("id", b.Id)).First();
        }

        /// <summary>
        /// Deletes a budget.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [ActionName("Delete")]
        public void Delete(int id)
        {
            var result = db.Database.SqlQuery<Budget>("EXEC DeleteBudget @id", new SqlParameter("id", id));
        }
    }
}
