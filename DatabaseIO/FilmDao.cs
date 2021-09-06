using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public class FilmDao
    {
        MyDB mydb = new MyDB();
        RoomDao roomD = new RoomDao();

        public film getDetailFilm(string id)
        {
            int filmId = Int32.Parse(id);
            return mydb.films.Where(f => f.id == filmId).FirstOrDefault();
        }
        public IEnumerable<film> searchFilm(string keySearch)
        {
            String nameSearch = "%" + keySearch + "%";
            string sql = "select * from films WHERE film_name LIKE @keysearch or actor LIKE @keysearch or director LIKE @keysearch ";
            return mydb.Database.SqlQuery<film>(sql, new SqlParameter("@keysearch", nameSearch)).ToList();
        }
        public void bookingTicket(booking book, string createTime)
        {
            string SQL = "INSERT INTO booking(id_user, film_id, schedule_id, showtime_id, room_id, seat_id, status,amount,create_time) " +
                "VALUES (@userId,@filmId,@scheduleId,@showtimeId,@roomId,@seatId,@status,@amount,@createTime)";
            mydb.Database.ExecuteSqlCommand(SQL, new SqlParameter("@userId", book.id_user),
                new SqlParameter("@filmId", book.film_id),
                new SqlParameter("@scheduleId", book.schedule_id),
                new SqlParameter("@showtimeId", book.showtime_id),
                new SqlParameter("@roomId", book.room_id),
                new SqlParameter("@status", book.status),
                new SqlParameter("@seatId", book.seat_id),
                new SqlParameter("@amount", book.amount),
                 new SqlParameter("@createTime", createTime)
                );

        }
        public List<booking> getOrder(string datenow, int id)
        {
            return mydb.bookings.Where(b => b.create_time == datenow && b.id_user == id).ToList();
        }
        public film getName(int id)
        {
            return mydb.films.Where(f => f.id == id).FirstOrDefault();
        }
        public List<booking> getBooking(int id)
        {
            return mydb.bookings.Where(b => b.id_user == id).OrderByDescending(b => b.id).ToList();
        }
        public List<film> getAll()
        {
            return mydb.films.ToList();
        }
        public void add(string description, string director, string actor, string duration, string film_name, string image, string trailer, string idcfilm, string ngaycc)
        {
            string SQL = "INSERT INTO films(description, director, actor, duration, film_name, image, trailer, id_cfilm, premiere_date ) VALUES(N'" + description + "',N'" + director + "',N'" + actor + "',N'" + duration + "',N'" + film_name + "','" + image + "',N'" + trailer + "','" + idcfilm + "','" + ngaycc + "' )";
            mydb.Database.ExecuteSqlCommand(SQL);

        }
        public void update(string description, string director, string actor, string duration, string film_name, string image, string trailer, string idcfilm, string id, string ngaycc)
        {
            string SQL = "UPDATE films SET description = N'" + description + "',director = N'" + director + "',actor = N'" + actor + "',duration = N'" + duration + "',film_name = N'" + film_name + "',image = '" + image + "',trailer = N'" + trailer + "',id_cfilm= '" + idcfilm + "', premiere_date = '" + ngaycc + "'  WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public void delete(string id)
        {
            string SQL = "DELETE FROM films WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public bool checkName(string name)
        {
            string sql = "SELECT * FROM films WHERE film_name = N'" + name + "'";
            var user = mydb.Database.SqlQuery<film>(sql).FirstOrDefault();
            if (user != null){
                return true;
            }
            return false;
        }
        public bool checkActive(int id)
        {
            string sql = "SELECT * FROM schedules WHERE film_id = @id";
            List<schedule> user = mydb.Database.SqlQuery<schedule>(sql, new SqlParameter("@id", id)).ToList();
            if (user.Count == 0){
                return true;
            }
            return false;
        }
        public List<SeatActive> handleGetSeat(List<seat> listSeatRoom, List<seat> listSeat)
        {
           
            int lengthArrSeatRoom = listSeatRoom.Count;
            var listSeatActive = new List<SeatActive>();
            for (int i = 0; i < lengthArrSeatRoom; i++){
                SeatActive sa = new SeatActive();
                sa.id = listSeatRoom[i].id;
                sa.seat_name = listSeatRoom[i].seat_name;
                sa.active = 0;
                listSeatActive.Add(sa);
            }
            int lengthArrSeatactive = listSeatActive.Count;
            for (int k = 0; k < lengthArrSeatactive; k++){
                for (int j = 0; j < listSeat.Count; j++){
                    if (listSeatActive[k].id == listSeat[j].id){
                        listSeatActive[k].active = 1;
                    }
                }
            }
            return listSeatActive;
        }
        public string renderSeat(List<SeatActive> listSeatActive)
        {
            string html = "";
            int lengthArrSeatactive = listSeatActive.Count;
            for (int j = 0; j < lengthArrSeatactive; j++){
                var nameId = "in" + listSeatActive[j].id;
                var nameIdDiv = "div" + listSeatActive[j].id;
                var nameIdSeat = "id" + listSeatActive[j].id;
                var check = "false";
                if (listSeatActive[j].active == 0){
                    html += "<div style='display: flex; align-content: center; justify-content: center; justify-items: center; border: 1px solid; background-color:#DDDDDD;line-height: 50px; height: 50px; width: 104.2px; margin-right: 6.4px; margin-bottom: 6.4px '><p>🪑" + listSeatActive[j].seat_name + "</p></div>";
                }else{
                    html += "<div id='" + nameIdDiv + "' onclick=onChoose(" + listSeatActive[j].id + ") style='display: flex; align-content: center; justify-content: center;justify-items: center; border: 1px solid red; line-height: 50px; height: 50px; width: 104.2px; margin-right: 6.4px; margin-bottom: 6.4px '><p>🪑" + listSeatActive[j].seat_name + "</p><input type='hidden' value='" + check + "' id='" + nameId + "'/><input type='hidden' value='" + listSeatActive[j].id + "' id='" + nameIdSeat + "' /></div>";
                }
            }
            return html;
        }
        public string renderRoom(List<room> listRoom)
        {
            string html = " <option> Chọn phòng </option>";
            foreach (var item in listRoom){
                html += "<option value=" + item.id + ">" + item.room_name + "</option>";
            }
            return html;
        }
        public string renderShowtime(List<showtime> listShowtime)
        {
            string html = " <option>Chọn suất chiếu</option>";
            foreach (var item in listShowtime){
                string showtimeString = item.start_time + " - " + item.end_time;
                html += "<option value=" + item.id + ">" + showtimeString + "</option>";
            }
            return html;
        }
        public string renderSchedule(List<schedule> list)
        {
            string html = "<option value=" + 0 + ">Chọn lịch chiếu</option>";
            foreach (var item in list){
                var roomOb = roomD.getNameRoomSchedule(item.id);
                string dateschedule = String.Format("{0:yyyy-MM-dd}", item.dateschedule);
                html += "<option value=" + item.id + ">" + dateschedule + "" + "(" + roomOb.room_name + ")" + "</option>";
            }
            return html;
        }
        
    }
}
