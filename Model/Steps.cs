using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Model
{
    public class Steps
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Totalsteps { get; set; }
        public int Stepsleft { get; set; }
        public string Walkdistance { get; set; }
        public string Date { get; set; }
    }
}
