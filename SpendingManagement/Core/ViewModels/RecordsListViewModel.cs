using SpendingManagement.Core.Models;
using System.Collections.Generic;

namespace SpendingManagement.Core.ViewModels
{
    public class RecordsListViewModel
    {
        public IEnumerable<Record> Records { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public SortingInfo SortingInfo { get; set; }
    }
}