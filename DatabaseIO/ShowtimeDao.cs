using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public class ShowtimeDao
    {
        MyDB mydb = new MyDB();
        public List<showtime> getShowtime(string id, int idRoom)
        {
            int scheduleId = Int32.Parse(id);
            return mydb.Database.SqlQuery<showtime>("SELECT s.* FROM showtimes s, schedules sc WHERE s.schedule_id = '" + scheduleId + "' and s.schedule_id = sc.id and CONVERT(varchar, sc.dateschedule, 101) = CONVERT(varchar, getdate(), 101) and s.id_room = '" + idRoom + "' and s.start_time >= convert(varchar(32),getdate(),108)").ToList();
        }
        public List<showtime> getShowtimes(string id, int idRoom)
        {
            int scheduleId = Int32.Parse(id);
            return mydb.Database.SqlQuery<showtime>("select * FROM showtimes WHERE schedule_id = '" + scheduleId + "' and id_room = '" + idRoom + "'").ToList();
        }
        public showtime getName(int id)
        {
            return mydb.showtimes.Where(s => s.id == id).FirstOrDefault();
        }
        public List<showtime> getAll()
        {
            return mydb.showtimes.ToList();
        }
        public void add(string scheid, string start, string end, int idRoom)
        {
            string SQL = "INSERT INTO showtimes(schedule_id,start_time,end_time,id_room) VALUES('" + scheid + "','" + start + "','" + end + "','"+idRoom+"')";
            mydb.Database.ExecuteSqlCommand(SQL);

        }
        public void update(string start, string end, string id)
        {
            string SQL = "UPDATE showtimes SET start_time = '" + start + "', end_time = '" + end + "' WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public void delete(string id)
        {
            string SQL = "DELETE FROM showtimes WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public List<showtime> checkShow(string start,string id,string idRoom)
        {          
            string sql = "Select * From showtimes where schedule_id = @idsche  AND id_room = @idroom  AND (@start BETWEEN start_time AND end_time OR (DATEDIFF(minute, @start, start_time) <= 120 AND DATEDIFF(minute, @start, start_time) >= 0 ))";
            return mydb.Database.SqlQuery<showtime>(sql, new SqlParameter("@start", start),new SqlParameter("@idsche", id),new SqlParameter("@idroom", idRoom)).ToList();          
        }
        public List<room> getNameRoom(int idSchedule)
        {
            string sql = "select r.* from scheduleroom s, room r where  s.id_schedule = '" + idSchedule + "' and r.id = s.id_room";
            return mydb.Database.SqlQuery<room>(sql).ToList();
        }
        public bool checkActive(int idShowtime)
        {
            string sql = "select * from booking where showtime_id = '" + idShowtime + "'";
            var showtimeOBj = mydb.Database.SqlQuery<booking>(sql).FirstOrDefault();
            if(showtimeOBj != null){
                return true;
            }
            return false;
        }
    }
}
