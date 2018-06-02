using SpendingManagement.Core.Models;
using SpendingManagement.Core.Repositiories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SpendingManagement.Repositiories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly ApplicationDbContext _contex = new ApplicationDbContext();

        public RecordRepository(ApplicationDbContext contex)
        {
            _contex = contex;
        }

        public IEnumerable<Record> Records { get { return _contex.Records; } }

        public void AddRecord(Record record)
        {
            _contex.Records.Add(record);
        }

        public void DeleteRecord(Record record)
        {
            _contex.Records.Remove(record);
        }

        public void Complete()
        {
            _contex.SaveChanges();
        }

        public Record GetRecord(string userId, int recordId)
        {
            return _contex.Records
                .SingleOrDefault(e => e.Id == recordId && e.UserID == userId);
        }

        public IEnumerable<Record> GetRecords(string userId, int amountOfRecords)
        {
            return _contex.Records.Where(u=> u.UserID == userId)
                .OrderBy(o => o.Date)
                .Take(amountOfRecords);
        }

        public IEnumerable<Record> GetRecordsInSelectedRange(DateTime? dateFrom, DateTime? dateTo)
        {
            return _contex.Records
                .Where(d => d.Date > dateFrom && d.Date < dateTo)
                .ToList();
        }

        public decimal GetYearRecordsSum(string userId)
        {
            var list = _contex.Records
                .Where(p => p.Date.Year == DateTime.Now.Year && p.UserID == userId)
                .Select(p => p.Charge).ToList();
            return list == null ? 0 : list.Sum();
        }

        public decimal GetMonthRecordsSum(string userId)
        {
            var list = _contex.Records
                .Where(p => p.Date.Month == DateTime.Now.Month && p.UserID == userId)
                .Select(p => p.Charge).ToList();
            return list == null ? 0 : list.Sum();
        }

        public decimal GetWeekRecordsSum(string userId)
        {
            var currentWeek = _GetFirstDayOfWeek().Date;
            var list = _contex.Records
                .Where(p => p.Date >= currentWeek && p.UserID == userId)
                .Select(p => p.Charge).ToList();
            return list == null ? 0 : list.Sum();
        }

        private static DateTime _GetFirstDayOfWeek()
        {
            var firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            DateTime currentDate = DateTime.Now;
            while (currentDate.DayOfWeek != firstDayOfWeek)
            {
                currentDate = currentDate.AddDays(-1);
            }
            return currentDate;
        }
    }
}