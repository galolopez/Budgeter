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
    public class HouseholdController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        /// <summary>
        /// Gets the household members for a single household id.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Members")]
        public IEnumerable<ApplicationUser> HouseholdMembers(int id)
        {
            return db.Database.SqlQuery<ApplicationUser>("EXEC GetUsersByHouseholdId @id", new SqlParameter("id", id));
        }

        /// <summary>
        /// Gets a household by its id.
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //[ActionName("Single")]
        //public Household Get()
        //{
        //    return db.Database.SqlQuery<Household>("EXEC GetHouseholdById @id", new SqlParameter("id", )).FirstAsync().Result;
        //}

        /// <summary>
        /// Gets a household by the user id. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Name")]
        public string GetHouseholdNameById(int id)
        {
            return db.Database.SqlQuery<string>("EXEC GetHouseholdNameById @id", new SqlParameter("id", id)).FirstAsync().Result;
        }

        /// <summary>
        /// Gets a household by the user id. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("UserId")]
        public int HouseholdByUser()
        {
            return db.Database.SqlQuery<int>("EXEC GetHouseholdIdByUserId @name", new SqlParameter("name", User.Identity.Name)).FirstAsync().Result;
        }

        /// <summary>
        /// Creates a household, given a name.
        /// </summary>
        /// <param name="h"></param>
        [HttpPost]
        [ActionName("Create")]
        public void Create(Household h)
        {
            var result = db.Database.SqlQuery<Household>("EXEC CreateHousehold @name", new SqlParameter("name", h.Name));
        }

        /// <summary>
        /// Joins a household, given a user id. 
        /// </summary>
        /// <param name="u"></param>
        [HttpPost]
        [ActionName("Join")]
        public void Join(ApplicationUser u)
        {
            var result = db.Database.SqlQuery<Household>("EXEC JoinHousehold @UserId, @param", new SqlParameter("UserId", u.Id), new SqlParameter("param", u.HouseholdId));
        }

        /// <summary>
        /// Leaves a household, given a user id. 
        /// </summary>
        /// <param name="u"></param>
        [HttpPost]
        [ActionName("Leave")]
        public void Leave(ApplicationUser u)
        {
            var result = db.Database.SqlQuery<Household>("EXEC LeaveHousehold @UserId", new SqlParameter("UserId", u.Id));
        }
    }
}
