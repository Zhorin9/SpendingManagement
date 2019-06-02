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
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public RecordRepository(ApplicationDbContext contex)
        {
            _context = contex;
        }

        public IEnumerable<Record> Records { get { return _context.Records; } }

        public void AddRecord(Record record)
        {
            _context.Records.Add(record);
        }

        public void DeleteRecord(Record record)
        {
            _context.Records.Remove(record);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public Record GetRecord(string userId, int recordId)
        {
            return _context.Records
                .SingleOrDefault(e => e.Id == recordId && e.UserID == userId);
        }

        public IEnumerable<Record> GetRecords(string userId, int amountOfRecords, bool isRevenue)
        {
            return _context.Records.Where(u=> u.UserID == userId && u.IsRevenue == isRevenue)
                .OrderByDescending(o => o.Date)
                .Take(amountOfRecords);
        }

        public IEnumerable<Record> GetRecordsInSelectedRange(DateTime? dateFrom, DateTime? dateTo, bool isRevenue, string userId)
        {
            if (dateFrom == null)
                dateFrom = DateTime.MinValue;
            if (dateTo == null)
                dateTo = DateTime.MaxValue;

            return _context.Records
                .Where(d => d.Date >= dateFrom && d.Date <= dateTo && d.IsRevenue == isRevenue && d.UserID == userId)
                .ToList();
        }


        public IEnumerable<Record> GetRecordsInSelectedRange(DateTime? dateFrom, DateTime? dateTo, string category, string userId)
        {
            if (dateFrom == null)
                dateFrom = DateTime.MinValue;
            if (dateTo == null)
                dateTo = DateTime.MaxValue;

            return _context.Records
                .Where(d => d.Date >= dateFrom && d.Date <= dateTo && d.Category == category && d.UserID == userId)
                .ToList();
        }

        public decimal GetYearRecordsSum(string userId, bool isRevenue)
        {
            var list = _context.Records
                .Where(p => p.Date.Year == DateTime.Now.Year && p.IsRevenue == isRevenue && p.UserID == userId)
                .Select(p => p.Charge).ToList();
            return list == null ? 0 : list.Sum();
        }

        public decimal GetMonthRecordsSum(string userId, bool isRevenue)
        {
            var list = _context.Records
                .Where(p => p.Date.Month == DateTime.Now.Month && p.IsRevenue == isRevenue && p.UserID == userId)
                .Select(p => p.Charge).ToList();
            return list == null ? 0 : list.Sum();
        }

        public decimal GetWeekRecordsSum(string userId, bool isRevenue)
        {
            var currentWeek = _GetFirstDayOfWeek().Date;
            var list = _context.Records
                .Where(p => p.Date >= currentWeek && p.IsRevenue == isRevenue && p.UserID == userId)
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