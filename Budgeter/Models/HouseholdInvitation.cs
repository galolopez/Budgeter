using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class HouseholdInvitation
    {
        public int Id { get; set; }
        public string InviteCode { get; set; }
        public int HouseholdId { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }

        public virtual Household Household { get; set; }
        public virtual ApplicationUser FromUser { get; set; }
    }
}