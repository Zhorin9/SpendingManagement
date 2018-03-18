﻿using SpendingManagement.Domain.Abstract;
using SpendingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Domain.Concrete
{
    class EFNoteRepository : INoteRepository
    {
        private EFDbContex contex = new EFDbContex();
        public IEnumerable<Note> Notes
        {
            get { return contex.Notes; }
        }
    }
}
