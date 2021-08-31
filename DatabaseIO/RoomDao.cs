using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public  class RoomDao
    {
        MyDB mydb = new MyDB();
        public List<room> getRoom(int id)
        {
            string sql = "SELECT * from room WHERE id IN (SELECT id_room FROM scheduleroom WHERE id_schedule = @idSchedule) ";
            return mydb.Database.SqlQuery<room>(sql, new SqlParameter("@idSchedule", id)).ToList();
        }
        public List<schedule> getSchedule()
        {
            return mydb.schedules.ToList();
        }
        public room getName(int id)
        {
            return mydb.rooms.Where(r => r.id == id).FirstOrDefault();
        }
        public room getNameRoomSchedule(int idSchedule)
        {
            string sql = "select r.* from room r , scheduleroom sc, schedules s where sc.id_schedule = s.id and sc.id_schedule = '"+idSchedule+"' and sc.id_room = r.id";
            return mydb.Database.SqlQuery<room>(sql).FirstOrDefault();
        }
        public room getId(string name)
        {        
            string sql = "SELECT * FROM room WHERE room_name =  @name";
            return mydb.Database.SqlQuery<room>(sql, new SqlParameter("@name", name)).FirstOrDefault();
        }
        public List<room> getAll()
        {
            return mydb.rooms.ToList();
        }
        public void Add(string name)
        {
            string SQL = "INSERT INTO room(room_name) VALUES(N'" + name + "')";
            mydb.Database.ExecuteSqlCommand(SQL);

        }
        public void Update(string name, string id)
        {
            string SQL = "UPDATE room SET room_name = N'" + name + "' WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public void Delete(string id)
        {
            string SQL = "DELETE FROM room WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public bool checkName(string name)
        {          
            string sql = "SELECT * FROM room WHERE room_name = @name";
            var user = mydb.Database.SqlQuery<room>(sql, new SqlParameter("@name", name)).FirstOrDefault();
            if (user != null){
                return true;
            }
            return false;
        }
        public void AddRoomSeat(int id_room, int id_seat)
        {
            string SQL = "INSERT INTO roomseat(id_room,id_seat) VALUES('" + id_room + "','" + id_seat + "')";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public List<roomseat> getAllRoomSeat()
        {
            string sql = "select * from roomseat";
            return mydb.Database.SqlQuery<roomseat>(sql).ToList();
        }
        public int numberSeat(int idRoom)
        {
            string sql = "select * from roomseat where id_room = '"+ idRoom +"'";
            var roomseat =  mydb.Database.SqlQuery<roomseat>(sql).ToList();
            return roomseat.Count;
        }
        public void DeleteRoomSeat(int idRoom)
        {
            string sql ="delete from roomseat where id_room = '"+idRoom+"'";
            mydb.Database.ExecuteSqlCommand(sql);
        }
        public bool checkActive(int idRoom)
        {
            string sql = "select * from roomseat where id_room = '" + idRoom + "'";
            string sql1 = "select * from scheduleroom where id_room = '" + idRoom + "'";
            string sql2 = "select * from showtimes where id_room = '" + idRoom + "'";
            var room_seat = mydb.Database.SqlQuery<roomseat>(sql).FirstOrDefault();
            var schedule_room = mydb.Database.SqlQuery<scheduleroom>(sql1).FirstOrDefault();
            var show_time = mydb.Database.SqlQuery<showtime>(sql2).FirstOrDefault();
            if(schedule_room != null || show_time != null){
                return true;
            }else{
                return false;
            }
        }
    }
}
