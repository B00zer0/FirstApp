using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FirstApp.Models
{
    public class Container
    {
        [PrimaryKey, AutoIncrement] 
        public int ID { get; set; }
        public string Text { get; set; }
        public string MassOfContainer { get; set; }
    }
}
