using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class Category
    {
        public Category()
        {
            this.Transactions = new HashSet<Transaction>();
            this.Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool ExpenseTF { get; set; }
        public int? HouseholdId { get; set; }

        public virtual Household Household { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}