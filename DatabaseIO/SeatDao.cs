using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public class SeatDao
    {
        MyDB mydb = new MyDB();

        
        public List<seat> getSeat(int roomId, int showtimeId, int filmId, int scheduleId)
        {
            return mydb.Database.SqlQuery<seat>("SELECT * from seats WHERE id NOT IN (SELECT seat_id FROM booking WHERE room_id = @rId " +
                "and showtime_id = @sId and film_id = @fId and schedule_id = @scheId)",
                new SqlParameter("@rId",roomId),
                new SqlParameter("@sId", showtimeId),
                new SqlParameter("@fId", filmId),
                new SqlParameter("@scheId", scheduleId)
                ).ToList();
        }
        public seat getName(int id)
        {
            return mydb.seats.Where(s => s.id == id).FirstOrDefault();
        }
    }
}
