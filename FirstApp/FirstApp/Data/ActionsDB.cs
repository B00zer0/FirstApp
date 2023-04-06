using FirstApp.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FirstApp.Data
{
    public class ActionsDB
    {
        public static SQLiteAsyncConnection db;

        public ActionsDB(string connectionString) 
        {
            db = new SQLiteAsyncConnection(connectionString);
            db.CreateTableAsync<CategoryAction>().Wait();
        }

        public Task<List<CategoryAction>> GetCategoryActions() 
        {
            return db.Table<CategoryAction>().OrderByDescending(i => i.Id).ToListAsync(); //Получаем список в порядке убывания id
        }



 

        public Task DeleteAllActions(int x)
        {
            return db.Table<CategoryAction>().DeleteAsync(i => i.Id < x);
        }

        public Task<CategoryAction> GetCategoryAction(int id) 
        {
            return db.Table<CategoryAction>().Where(i => i.Id == id).FirstOrDefaultAsync(); 
        }

        public Task SaveCategoryAction(CategoryAction action)
        {
            if(action.Id != 0)
            {
                return db.UpdateAsync(action);
            }
            else
            {
                return db.InsertAsync(action);
            }
        }

        public Task DeleteCategoryAction(CategoryAction action) 
        {
                return db.DeleteAsync(action);
        }
    }
}
