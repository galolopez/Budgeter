using Budgeter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;

namespace Budgeter.Controllers
{
    public class HouseholdInvitationController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: api/HouseholdInvitation
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/HouseholdInvitation/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/HouseholdInvitation
        public void Post([FromBody]string email)
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            db.Database.SqlQuery<HouseholdInvitation>("EXEC CreateInvitation @toEmail, @fromEmail, @householdId, @inviteCode", 
                new SqlParameter("toEmail", email),
                new SqlParameter("fromEmail", user.Email),
                new SqlParameter("householdId", user.HouseholdId),
                new SqlParameter("inviteCode", Guid.NewGuid().ToString()));
        }

        // DELETE: api/HouseholdInvitation/5
        public void Delete(int id)
        {
        }
    }
}
