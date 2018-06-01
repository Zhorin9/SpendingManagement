using SpendingManagement.Core.Models;
using System;
using System.Collections.Generic;

namespace SpendingManagement.Core.Repositiories
{
    public interface IRecordRepository
    {
        IEnumerable<Record> Records { get; }
        void AddExpense(Record record);
        void DeleteExpense(Record record);
        Record GetRecord(string userId, int recordId);
        IEnumerable<Record> GetRecordsInSelectedRange(DateTime? dateFrom, DateTime? dateTo);
        IEnumerable<Record> GetRecords(string userId, int amountOfRecords);
        void Complete();
        decimal GetYearRecordsSum(string userId);
        decimal GetMonthRecordsSum(string userId);
        decimal GetWeekRecordsSum(string userId);
    }
}
