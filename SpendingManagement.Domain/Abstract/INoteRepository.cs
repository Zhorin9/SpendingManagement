using SpendingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Domain.Abstract
{
    public interface INoteRepository
    {
        IEnumerable<Note> Notes { get; }
    }
}
