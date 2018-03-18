﻿using SpendingManagement.Domain.Entities;
using System.Collections.Generic;

namespace SpendingManagement.Domain.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
        IEnumerable<Note> Notes { get; }
        void SaveUser(User user);

    }
}
