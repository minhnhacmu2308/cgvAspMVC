using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public class ShowtimeDao
    {
        MyDB mydb = new MyDB();
        public List<showtime> getShowtime(string id)
        {
            int scheduleId = Int32.Parse(id);
            return mydb.showtimes.Where(s => s.schedule_id == scheduleId).ToList();
        }
        public showtime getName(int id)
        {
            return mydb.showtimes.Where(s => s.id == id).FirstOrDefault();
        }
    }
}
