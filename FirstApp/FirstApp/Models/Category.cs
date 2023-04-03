using SQLite;

namespace FirstApp.Models
{
    public class Category
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string CategoryMass { get; set; }
    }
}
