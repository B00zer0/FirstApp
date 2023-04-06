using SQLite;

namespace FirstApp.Models
{
    public class CategoryAction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Action { get; set; }
        public string OldMass { get; set; }
    }
}
