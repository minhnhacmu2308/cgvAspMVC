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
        public List<seat> getSeatDone(int roomId, int showtimeId, int filmId, int scheduleId)
        {
            return mydb.Database.SqlQuery<seat>("SELECT * from seats WHERE id IN (SELECT rs.id_seat FROM roomseat rs WHERE rs.id_room = @idRoom )" +
                "and  id NOT IN (SELECT seat_id FROM booking WHERE room_id = @idRoom  and showtime_id = @sId and film_id = @fId and schedule_id = @scheId)",
                new SqlParameter("@idRoom", roomId),
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
        public List<seat> getSeatRoom(int id)
        {
            String sql = "select * from seats s, roomseat rs where s.id = rs.id_seat and id_room = @idRoom  ";
            return mydb.Database.SqlQuery<seat>(sql, new SqlParameter("@idRoom ",id)).ToList();
        }
        public void add(string name)
        {
            string SQL = "INSERT INTO seats(seat_name) VALUES(N'" + name + "')";
            mydb.Database.ExecuteSqlCommand(SQL);

        }
      
        public void update(string name, string id)
        {
            string SQL = "UPDATE seats SET seat_name = N'" + name + "' WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public void delete(string id)
        {
            string SQL = "DELETE FROM seats WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public bool checkName(string name)
        {
            var user = mydb.seats.Where(u => u.seat_name == name).FirstOrDefault();
            if (user != null){
                return true;
            }
            return false; ;
        }
        public bool checkActive(int id)
        {
            string sql = "select * from roomseat where id_seat = '" + id + "'";
            var roomseat = mydb.Database.SqlQuery<roomseat>(sql).FirstOrDefault();
            if(roomseat != null){
                return true;
            }else{
                return false;
            }
        }
    }
}
