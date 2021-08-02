using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DatabaseIO
{
    public class ShowtimeDao
    {
        MyDB mydb = new MyDB();
        public List<showtime> getAll()
        {
            return mydb.showtimes.ToList();
        }
    }
}
