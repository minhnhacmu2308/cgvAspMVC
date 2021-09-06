using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DatabaseIO
{
    
    public class BookingDao
    {
        MyDB mydb = new MyDB();
        FilmDao filmD = new FilmDao();
        ScheduleDao scheduleD = new ScheduleDao();
        RoomDao roomD = new RoomDao();
        SeatDao seatD = new SeatDao();

        public void accpect(string id)
        {
            string SQL = "UPDATE booking SET status = 1 WHERE id = '" + id + "'";
            mydb.Database.ExecuteSqlCommand(SQL);
        }
        public booking newObjectBooking(int schedule_id,int film_id,int room_id,int showtime_id, int id_user, string create_time)
        {
          
            booking book = new booking();
            book.schedule_id = schedule_id;
            book.film_id = film_id;
            book.room_id = room_id;
            book.showtime_id = showtime_id;
            book.id_user = id_user;
            book.status = 0;
            book.create_time = create_time;
            book.amount = 3;
            return book;
        }
        public void addBookingTicket(int[] arrSeat,booking order, string create_time,int id_user,int status)
        {
            booking book = new booking();
            int lengthArSeat = arrSeat.Length;
            for (int i = 0; i < lengthArSeat; i++)
            {
                book.schedule_id = order.schedule_id;
                book.film_id = order.film_id;
                book.seat_id = arrSeat[i];
                book.room_id = order.room_id;
                book.showtime_id = order.showtime_id;
                book.id_user = id_user;
                book.status = status;
                book.create_time = order.create_time;
                book.amount = 3;
                filmD.bookingTicket(book, create_time);
            }
        }
        public HistoryBooking addHistoryBooking(int film_id,int id, int room_id, int seat_id, string dateschedule, int amount,string ngay,int status)
        {

            HistoryBooking his = new HistoryBooking();
            his.nameFilm = filmD.getName(film_id).film_name;
            his.id = id + 1;
            his.roomName = roomD.getName(room_id).room_name;
            his.seatName = seatD.getName(seat_id).seat_name;
            his.status = status;
            his.schedulename = dateschedule;
            his.amount = amount.ToString();
            his.showtimeName = ngay;
            return his;
        }
        public bool checkBooking(int film_id, int schedule_id, int showtime_id, int room_id, int[] seat_id)
        {
            int lengthArrSeatId = seat_id.Length;
            for (int i=0 ; i< lengthArrSeatId; i++){
                int idSeat = seat_id[i];
                var booking = mydb.bookings.Where(b => b.film_id == film_id && b.schedule_id == schedule_id && b.room_id == room_id 
                && b.seat_id == idSeat && b.showtime_id == showtime_id).FirstOrDefault();
                if(booking != null){
                    return true;
                }
            }
            return false;
        }
    }
}
