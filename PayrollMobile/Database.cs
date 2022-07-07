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
            db.CreateTableAsync<ContentPage>();
        }
        // Show Blank Record
        public Task<List<ContentPage>> GetShiftAsync()
        {
            return db.Table<ContentPage>().ToListAsync();
        }
        // Add to Database
        public Task<int> SaveShiftAsync(ContentPage shift)
        {
            return db.InsertAsync(shift);
        }
        // Change Record from Database
        public Task<int> UpdateShiftAsync(ContentPage shift)
        {
            return db.UpdateAsync(shift);
        }
        // Delete Record
        public Task<int> DeleteShiftAsync(ContentPage shift)
        {
            return db.DeleteAsync(shift);
        }

        //TODO 
        public Task<List<ContentPage>> QueryGrandTotalAsync()
        {
            return db.QueryAsync<ContentPage>("SELECT * FROM Shift ORDER BY WorkDate");
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
