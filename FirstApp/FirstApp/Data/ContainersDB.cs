using System.Collections.Generic;
using SQLite;
using FirstApp.Models;
using System.Threading.Tasks;

namespace FirstApp.Data
{
    public class ContainersDB
    {
        public static SQLiteAsyncConnection db;

        public ContainersDB (string connectionString)
        {
            db = new SQLiteAsyncConnection (connectionString);

            db.CreateTableAsync<Container>().Wait();
        }
        
        public static Task<List<Container>> GetContainersAsync()
        {
            return db.Table<Container>().ToListAsync();
        }

        public static Task<Container> GetContainerAsync(int id)
        {
            return db.Table<Container>().Where(i => i.ID== id).FirstOrDefaultAsync();
        }

        public Task SaveContainerAsync(Container container)
        {
            
                return db.InsertAsync(container);
            
        }

        public static Task<int> DeleteContainerAsync(Container container)
        {
            return db.DeleteAsync(container);
        }
    }
}
