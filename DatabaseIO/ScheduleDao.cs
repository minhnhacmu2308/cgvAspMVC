using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DatabaseIO
{
    public class ScheduleDao
    {
        MyDB mydb = new MyDB();
        public List<schedule> getAll()
        {
            return mydb.schedules.ToList();
        }
    }
}
