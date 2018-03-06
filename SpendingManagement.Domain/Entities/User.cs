using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Domain.Entities
{
    public class User
    {
        public User()
        {
            this.Expenses = new HashSet<Expense>();
        }
        public int UserID { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
