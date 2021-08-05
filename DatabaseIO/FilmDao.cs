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
        public void bookingTicket(booking book)
        {
            string SQL = "INSERT INTO booking(id_user, film_id, schedule_id, showtime_id, room_id, seat_id, amount) " +
                "VALUES (@userId,@filmId,@scheduleId,@showtimeId,@roomId,@seatId,@amount)";
            mydb.Database.ExecuteSqlCommand(SQL,new SqlParameter("@userId", book.id_user),
                new SqlParameter("@filmId", book.film_id),
                new SqlParameter("@scheduleId", book.schedule_id),
                new SqlParameter("@showtimeId", book.showtime_id),
                new SqlParameter("@roomId", book.room_id),
                new SqlParameter("@seatId", book.seat_id),
                new SqlParameter("@amount", book.amount)
                );
            mydb.SaveChanges();
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
    }
}
