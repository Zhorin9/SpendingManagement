﻿using System;
using SpendingManagement.Models;

namespace SpendingManagement.Models
{
    public class Expense
    {
        public int Id { get; set; }

        public string Name { get; set; }
       
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public decimal Charge { get; set; }

        public string Category { get; set; }

        public string Subcategory { get; set; }


        public int UserID { get; set; }
    }
}
