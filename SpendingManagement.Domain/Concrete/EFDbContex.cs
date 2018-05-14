using System.Data.Entity;
using SpendingManagement.Domain.Entities;

namespace SpendingManagement.Domain.Concrete
{
    public class EFDbContex :DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
