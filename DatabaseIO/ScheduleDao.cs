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
            string SQL = "Select * from schedules where film_id = '" + id + "' and YEAR(dateschedule) >= YEAR(getdate()) and MONTH(dateschedule) >= MONTH(getdate()) and DAY(dateschedule) >= DAY(getdate())";
            return mydb.Database.SqlQuery<schedule>(SQL).ToList();
        }
        public schedule getName(int id)
        {
            return mydb.schedules.Where(s => s.id == id).FirstOrDefault();
        }
        public List<schedule> getAll()
        {
            return mydb.schedules.ToList();
        }
        public void Add(string filmid, DateTime dateschedule)
        {
            string SQL = "INSERT INTO schedules(film_id,dateschedule) VALUES('" + filmid + "','" + dateschedule + "')";
            mydb.Database.ExecuteSqlCommand(SQL);

        }
        public void Update(string filmid, DateTime dateschedule, string id)
        {
            string SQL = "UPDATE schedules SET film_id = '" + filmid + "', dateschedule = '" + dateschedule + "' WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public void Delete(string id)
        {
            string SQL = "DELETE FROM schedules WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public bool checkName(DateTime datesche,int id)
        {
            var user = mydb.schedules.Where(u => u.dateschedule == datesche && u.film_id == id).FirstOrDefault();
            if (user != null){
                return true;
            }
            return false;
        }
        public int getSCheduleByDate()
        {

            return Int32.Parse(mydb.schedules.ToList().Last().id.ToString());
        }
        public void AddScheduleRoom(int id_schedule, int id_room)
        {
            string sql = "insert into scheduleroom(id_schedule,id_room) values('" + id_schedule + "','" + id_room + "')";
            mydb.Database.ExecuteSqlCommand(sql);
        }
        public bool checkScheduleRoom(DateTime datesche, int idRoom,int idFilm)
        {
            string sql = "select a.* from schedules a,scheduleroom b, booking c where  a.id = b.id_schedule and a.dateschedule = '"+datesche+"' and b.id_room = '"+idRoom+"' and c.film_id = '"+idFilm+"'";
            var scheduleR = mydb.Database.SqlQuery<schedule>(sql).FirstOrDefault();
            if (scheduleR != null){
                return true;
            }
            return false;
        }
        public bool checkScheduleRoomUpdate(DateTime datesche, int idRoom,int idFilm)
        {
            string sql = "select a.* from schedules a,scheduleroom b where  a.id = b.id_schedule and a.dateschedule = '" + datesche + "' and b.id_room = '" + idRoom + "' and a.film_id = '"+idFilm+"'";
            var scheduleR = mydb.Database.SqlQuery<schedule>(sql).FirstOrDefault();
            if (scheduleR != null){
                return true;
            }
            return false;
        }
        public room getNameRoom(int idSchedule)
        {
            string sql = "select r.* from scheduleroom s, room r where  s.id_schedule = '"+idSchedule+"' and r.id = s.id_room";
            return mydb.Database.SqlQuery<room>(sql).FirstOrDefault();
        }
        public void deleteScheduleRoom(int id_schedule)
        {
            string sql = "delete from scheduleroom where id_schedule = '" + id_schedule + "'";
            mydb.Database.ExecuteSqlCommand(sql);
        }
        public bool checkShowtimeActive(int idSchedule , int id_room)
        {
            string sql = "select st.* from showtimes st, scheduleroom sc where  st.schedule_id = sc.id_schedule and st.id_room = sc.id_room and st.schedule_id ='" + idSchedule + "' and st.id_room ='" + id_room + "'";
            var check = mydb.Database.SqlQuery<showtime>(sql).FirstOrDefault();
            if(check != null){
                return true;
            }else{
                return false;
            }
        }
        public void editRoomShowTime(int idSchedule,int idRoom)
        {
            var showtime = mydb.showtimes.Where(s => s.schedule_id == idSchedule).FirstOrDefault();
            showtime.id_room = idRoom;
            mydb.SaveChanges();
        }
    }
}
