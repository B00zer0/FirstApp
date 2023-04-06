using System.Collections.Generic;
using SQLite;
using FirstApp.Models;
using System.Threading.Tasks;


namespace FirstApp.Data
{
    public class CategoriesDB
    {  
        public static SQLiteAsyncConnection db;

        public CategoriesDB(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);
    
            db.CreateTableAsync<Category>().Wait();
        }

        public Task<List<Category>> GetCategoriesAsync()
        {
            return db.Table<Category>().ToListAsync();
        }

        public Task<Category> GetCategoryAsync(int id)
        {
            return db.Table<Category>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task SaveCategoryAsync(Category category)
        {
            if (category.ID != 0)
            {
                return db.UpdateAsync(category);
            }
            else
            {
                return db.InsertAsync(category);
            }
        }

        public Task<int> DeleteCategoryAsync(Category category)
        {
            return db.DeleteAsync(category);
        }
    }
}
