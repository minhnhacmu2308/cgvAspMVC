using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public  class RoomDao
    {
        MyDB mydb = new MyDB();
        public List<room> getRoom()
        {
            return mydb.rooms.ToList();
        }
        public List<schedule> getSchedule()
        {
            return mydb.schedules.ToList();
        }
        public room getName(int id)
        {
            return mydb.rooms.Where(r => r.id == id).FirstOrDefault();
        }
    }
}
