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
        public List<seat> getAll()
        {
            return mydb.seats.ToList();
        }
        public void Add(string name)
        {
            string SQL = "INSERT INTO seats(seat_name) VALUES(N'" + name + "')";
            mydb.Database.ExecuteSqlCommand(SQL);

        }
        public void Update(string name, string id)
        {
            string SQL = "UPDATE seats SET seat_name = N'" + name + "' WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public void Delete(string id)
        {
            string SQL = "DELETE FROM seats WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public bool checkName(string name)
        {
            var user = mydb.seats.Where(u => u.seat_name == name).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false; ;
        }
    }
}
