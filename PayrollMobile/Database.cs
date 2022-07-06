using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Microsoft.Data.Sqlite;
using System.Collections;
using System;

namespace PayrollMobile
{
    public class Database
    {
        private readonly SQLiteAsyncConnection db;
        public Database(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Shift>();
        }
        // Show Blank Record
        public Task<List<Shift>> GetShiftAsync()
        {
            return db.Table<Shift>().ToListAsync();
        }
        // Add to Database
        public Task<int> SaveShiftAsync(Shift shift)
        {
            return db.InsertAsync(shift);
        }
        // Change Record from Database
        public Task<int> UpdateShiftAsync(Shift shift)
        {
            return db.UpdateAsync(shift);
        }
        // Delete Record
        public Task<int> DeleteShiftAsync(Shift shift)
        {
            return db.DeleteAsync(shift);
        }

        //TODO 
        public Task<List<Shift>> QueryGrandTotalAsync()
        {
            return db.QueryAsync<Shift>("SELECT * FROM Shift ORDER BY WorkDate");
        }
        //public Task<List<Shift>> LinqNotGrandTotalAsync()
        //{
        //    return db.Table<Shift>().Where(p => p.GrandTotal < 10).ToListAsync();
        //}

        internal IEnumerable GetShift()
        {
            throw new NotImplementedException();
        }
    }
}
