using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Category Category { get; set; }
    }
}