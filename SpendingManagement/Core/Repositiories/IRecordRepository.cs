using SpendingManagement.Core.Models;
using System;
using System.Collections.Generic;

namespace SpendingManagement.Core.Repositiories
{
    public interface IRecordRepository
    {
        IEnumerable<Record> Records { get; }

        /// <summary>
        /// Function to add record
        /// </summary>
        /// <param name="record"></param>
        void AddRecord(Record record);

        /// <summary>
        /// Function to delete record
        /// </summary>
        /// <param name="record"></param>
        void DeleteRecord(Record record);
        Record GetRecord(string userId, int recordId);
        IEnumerable<Record> GetRecordsInSelectedRange(DateTime? dateFrom, DateTime? dateTo, bool isRevenue, string userId);
        IEnumerable<Record> GetRecordsInSelectedRange(DateTime? dateFrom, DateTime? dateTo, string category, string userId);

        IEnumerable<Record> GetRecords(string userId, int amountOfRecords, bool isRevenue);

        /// <summary>
        /// Function to save changes in database
        /// </summary>
        void Complete();

        /// <summary>
        /// Return sum of expenses in the current year.
        /// </summary>
        /// <param name="userId">Id of current authorized user</param>
        /// <param name="isRevenue"></param>
        decimal GetYearRecordsSum(string userId, bool isRevenue);

        /// <summary>
        /// Return sum of expenses in the current month.
        /// </summary>
        /// <param name="userId">Id of current authorized user</param>
        /// <param name="isRevenue"></param>
        decimal GetMonthRecordsSum(string userId, bool isRevenue);

        /// <summary>
        /// Return sum of expenses in the current week.
        /// </summary>
        /// <param name="userId">Id of current authorized user</param>
        /// <param name="isRevenue"></param>
        decimal GetWeekRecordsSum(string userId, bool isRevenue);
    }
}
