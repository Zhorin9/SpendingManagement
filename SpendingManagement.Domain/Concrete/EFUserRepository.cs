using SpendingManagement.Domain.Abstract;
using SpendingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Domain.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        private EFDbContex contex = new EFDbContex();
        public IEnumerable<User> Users
        {
            get { return contex.Users; }
        }

        public void SaveUser(User user)
        {
            contex.Users.Add(user);
            contex.SaveChanges();
        }
    }
}
