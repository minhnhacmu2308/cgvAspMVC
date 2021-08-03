using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DatabaseIO
{
    public class SeatDao
    {
        MyDB mydb = new MyDB();
        public List<seat> getAll()
        {
            return mydb.seats.ToList();
        }
    }
}
