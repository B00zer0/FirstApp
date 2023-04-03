using FirstApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp.Data
{
    class ActionsDB
    {
        public static SQLiteAsyncConnection db;

        public ActionsDB(string connectionString) 
        {
            db = new SQLiteAsyncConnection(connectionString);
            db.CreateTableAsync<CategoryAction>().Wait();
        }

        public Task<List<CategoryAction>> GetCategoryActions() 
        {
            return db.Table<CategoryAction>().ToListAsync();
        }

        public Task<CategoryAction> GetCategoryAction(int id) 
        {
            return db.Table<CategoryAction>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

    }
}
