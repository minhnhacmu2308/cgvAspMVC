using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public class ScheduleDao
    {
        MyDB mydb = new MyDB();
        public List<schedule> getSchedule(string id)
        {
            int filmId = Int32.Parse(id);
            return mydb.schedules.Where(s => s.film_id == filmId).ToList();
        }
        public schedule getName(int id)
        {
            return mydb.schedules.Where(s => s.id == id).FirstOrDefault();
        }
      
    }
}
