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

        public film getDetailFilm(string id)
        {
            int filmId = Int32.Parse(id);
            return mydb.films.Where(f => f.id == filmId).FirstOrDefault();
        }
        public IEnumerable<film> searchFilm(string keySearch)
        {
            return mydb.films.Where(f => f.film_name.Contains(keySearch) || f.actor.Contains(keySearch) || f.director.Contains(keySearch));
        }
        public void bookingTicket(booking book,string createTime)
        {
            string SQL = "INSERT INTO booking(id_user, film_id, schedule_id, showtime_id, room_id, seat_id, amount,create_time) " +
                "VALUES (@userId,@filmId,@scheduleId,@showtimeId,@roomId,@seatId,@amount,@createTime)";
            mydb.Database.ExecuteSqlCommand(SQL,new SqlParameter("@userId", book.id_user),
                new SqlParameter("@filmId", book.film_id),
                new SqlParameter("@scheduleId", book.schedule_id),
                new SqlParameter("@showtimeId", book.showtime_id),
                new SqlParameter("@roomId", book.room_id),
                new SqlParameter("@seatId", book.seat_id),
                new SqlParameter("@amount", book.amount),
                 new SqlParameter("@createTime", createTime)
                );
          
        }
        public List<booking> getOrder(string datenow,int id)
        {
            return mydb.bookings.Where(b => b.create_time == datenow && b.id_user == id).ToList();
        }
        public film getName(int id)
        {
            return mydb.films.Where(f => f.id == id).FirstOrDefault();
        }
        public List<booking> getBooking(int id)
        {
            return mydb.bookings.Where(b => b.id_user == id).ToList();
        }
        public List<film> getAll()
        {
            return mydb.films.ToList();
        }
        public void Add(string description, string director, string actor, string duration, string film_name, string image, string trailer, string idcfilm)
        {
            string SQL = "INSERT INTO films(description, director, actor, duration, film_name, image, trailer, id_cfilm ) VALUES('" + description + "','" + director + "','" + actor + "','" + duration + "','" + film_name + "','" + image + "','" + trailer + "','" + idcfilm + "')";
            mydb.Database.ExecuteSqlCommand(SQL);

        }
        public void Update(string description, string director, string actor, string duration, string film_name, string image, string trailer, string idcfilm, string id)
        {
            string SQL = "UPDATE films SET description = '" + description + "',director = '" + director + "',actor = '" + actor + "',duration = '" + duration + "',film_name = '" + film_name + "',image = '" + image + "',trailer = '" + trailer + "',id_cfilm= '" + idcfilm + "'  WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public void Delete(string id)
        {
            string SQL = "DELETE FROM films WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
    }
}
