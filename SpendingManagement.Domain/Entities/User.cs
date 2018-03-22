using System;
using System.Collections.Generic;

namespace SpendingManagement.Domain.Entities
{
    public class User
    {
        public User()
        {
            this.Expenses = new HashSet<Expense>();
            this.Notes = new HashSet<Note>();
        }
        public int UserID { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
