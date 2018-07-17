using SpendingManagement.Core.Models;
using SpendingManagement.Core.Repositiories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SpendingManagement.Repositiories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void DeleteUser(ApplicationUser user)
        {
            _context.Users.Remove(user);
        }

        public ApplicationUser GetUserById(string userId)
        {
            return _context.Users
                    .Where(u => u.Id == userId)
                    .Include(e=> e.Records)
                    .FirstOrDefault();
        }

    }
}