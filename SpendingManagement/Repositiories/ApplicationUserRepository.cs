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

        public IEnumerable<ApplicationUser> GetUserById(string userId)
        {
            return _context.Users
                    .Where(u => u.Id == userId)
                    .Include(e=> e.Records);
        }

    }
}