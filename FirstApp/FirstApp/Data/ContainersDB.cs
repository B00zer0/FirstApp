using System.Collections.Generic;
using SQLite;
using FirstApp.Models;
using System.Threading.Tasks;

namespace FirstApp.Data
{
    internal class ContainersDB
    {
        readonly SQLiteAsyncConnection db;
        public ContainersDB (string connectionString)
        {
            db = new SQLiteAsyncConnection (connectionString);

            db.CreateTableAsync<Container>().Wait ();
        }
        
        public Task<List<Container>> GetContainersAsync()
        {
            return db.Table<Container>().ToListAsync ();
        }

        public Task<Container> GetContainerAsync(int id)
        {
            return db.Table<Container>().Where(i => i.ID== id).FirstOrDefaultAsync();
        }
        public Task<int> SaveContainerAsync(Container container)
        {
            if(container.ID != 0)
            {
                return db.UpdateAsync(container);
            }
            else
            {
                return db.InsertAsync(container);
            }
        }
        public Task<int> DeleteContainerAsync(Container container)
        {
            return db.DeleteAsync(container);
        }
    }
}
