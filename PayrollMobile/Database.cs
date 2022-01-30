using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PayrollMobile
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Shift>();
        }
        // Show Blank Record
        public Task<List<Shift>> GetShiftAsync()
        {
            return _database.Table<Shift>().ToListAsync();
        }
        // Add to Database
        public Task<int> SaveShiftAsync(Shift shift)
        {
            return _database.InsertAsync(shift);
        }
        // Change Record from Database
        public Task<int> UpdateShiftAsync(Shift shift)
        {
            return _database.UpdateAsync(shift);
        }
        // Delete Record
        public Task<int> DeleteShiftAsync(Shift shift)
        {
            return _database.DeleteAsync(shift);
        }

        //TODO 
        //public Task<List<Shift>> QuerySubscribedAsync()
        //{
        //    return _database.QueryAsync<Shift>("SELECT * FROM Person WHERE Subscribed = true");
        //}
        //public Task<List<Shift>> LinqNotSubscribedAsync()
        //{
        //    return _database.Table<Shift>().Where(p => p.Subscribed == false).ToListAsync();
        //}
    }
}
