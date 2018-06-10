using SpendingManagement.Core.Models;
using System;
using System.Collections.Generic;

namespace SpendingManagement.Core.Repositiories
{
    public interface IRecordRepository
    {
        IEnumerable<Record> Records { get; }
        void AddRecord(Record record);
        void DeleteRecord(Record record);
        Record GetRecord(string userId, int recordId);
        IEnumerable<Record> GetRecordsInSelectedRange(DateTime? dateFrom, DateTime? dateTo);
        IEnumerable<Record> GetRecords(string userId, int amountOfRecords, bool isRevenue);
        void Complete();
        decimal GetYearRecordsSum(string userId, bool isRevenue);
        decimal GetMonthRecordsSum(string userId, bool isRevenue);
        decimal GetWeekRecordsSum(string userId, bool isRevenue);
    }
}
