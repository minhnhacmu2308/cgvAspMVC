using DatabaseIO;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CGV.Controllers
{
    public class FilmController : Controller
    {
        // GET: Film
        FilmDao filmD = new FilmDao();
        ScheduleDao scheduleD = new ScheduleDao();
        RoomDao roomD = new RoomDao();
        ShowtimeDao showtimeD = new ShowtimeDao();
        SeatDao seatD = new SeatDao();
        CommentDao commentD = new CommentDao();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetailFilm(string id)
        {
            film film = filmD.getDetailFilm(id);
            return View(film);
        }
        public ActionResult SearchFilm(string keySearch)
        {
            if (string.IsNullOrEmpty(keySearch))
            {
                return RedirectToAction("IndexUser","Home");
            }
            else
            {
                var listSearch = filmD.searchFilm(keySearch);
                ViewBag.keySearch = keySearch;
                return View(listSearch);
            }
           
        }
     
        [HttpPost]
        public  JsonResult  getSchedule(string id)
        {

                List<schedule> list = scheduleD.getSchedule(id);
                string html = "<option value="+0+">Chọn lịch chiếu</option>";
                if(Int32.Parse(id)==0)
                {
                    return Json(new { status = "ERROR", msg = "Cần chọn lịch chiếu", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    foreach (var item in list)
                    {
                        html += "<option value=" + item.id + ">" + item.dateschedule + "</option>";
                    }
                    return Json(new { status = "OK", data = html, msg = "thanhcong", JsonRequestBehavior.AllowGet });
                 }
               
           
          
        } 
        [HttpPost]
        public JsonResult getShowtime(string id)
        {
           
            List<showtime> listShowtime = showtimeD.getShowtime(id);
            string html = " <option>Chọn suất chiếu</option>";
            foreach(var item in listShowtime)
            {
                string showtimeString = item.start_time + " - " + item.end_time;
                html += "<option value=" + item.id + ">" + showtimeString + "</option>";
            }
            return Json(new { status = "OK", data = html, msg = "thanhcong", JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public JsonResult getRoom()
        {
            List<room> listRoom = roomD.getRoom();
            string html = " <option>Chọn phòng</option>";
            foreach (var item in listRoom)
            {
                html += "<option value="+item.id+">"+item.room_name+"</option>";
            }
            return Json(new { status = "OK", data = html, msg = "thanhcong", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult getSeat(int roomId, int showtimeId, int filmId, int scheduleId)
        {
            List<seat> listSeat = seatD.getSeat(roomId, showtimeId, filmId, scheduleId);
            string html = "";
            foreach(var item in listSeat)
            {
                html += " <option value=" + item.id + ">" + item.seat_name + "</option>";
            }
            return Json(new { status = "OK", data = html, msg = "thanhcong", JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public JsonResult bookingTicket( int film_id,int schedule_id,int showtime_id, int room_id, int[] seat_id )
        {
            var userInfomatiom = (usercgv)Session[Constants.Constants.USER_SESSION];
            if(userInfomatiom == null )
            {
                return Json(new { status = "ERROR", msg = "❌ Bạn cần phải đăng nhập !", JsonRequestBehavior.AllowGet });
            }
            else
            {
                booking book = new booking();
                for (int i = 0; i< seat_id.Length; i++)
                {
                    
                    book.schedule_id = schedule_id;
                    book.seat_id = seat_id[i];
                    book.film_id = film_id;
                    book.room_id = room_id;
                    book.showtime_id = showtime_id;
                    book.id_user = userInfomatiom.id;
                    book.status = 0;
                    book.amount = Constants.Constants.PRICE_TICKET;
                    filmD.bookingTicket(book);
                   
                }
                return Json(new { status = "OK", msg = "✅ Đặt vé thành công", JsonRequestBehavior.AllowGet });
            }
           
        }


        public ActionResult testSchedule()
        {
            MyDB mydb = new MyDB();


            List<schedule> listSchedule = mydb.schedules.ToList();
            return View(listSchedule);
        }
        
        [HttpPost]
        public JsonResult getTest()
        {
            MyDB mydb = new MyDB();
            List<schedule> list = mydb.schedules.ToList();
            return Json(new { status = "OK", data = list, msg = "thanhcong", JsonRequestBehavior.AllowGet });
        }
       [HttpPost]
       public JsonResult postCommnet(int idFilm,string textComment)
        {
            var userInfomatiom = (usercgv)Session[Constants.Constants.USER_SESSION];
            if (userInfomatiom == null)
            {
                return Json(new { status = "ERROR", msg = "❌ Bạn cần phải đăng nhập !", JsonRequestBehavior.AllowGet });
            }
            else
            {
                rating rating = new rating();
                rating.film_id = idFilm;
                rating.id_user = userInfomatiom.id;
                rating.name_user = userInfomatiom.username;
                rating.rate = textComment;
                commentD.comment(rating);
                var listcomment = commentD.getCommentById(idFilm);
               
                var listAjax = new List<CommentAjax>();
                for(int i = 0; i < listcomment.Count; ++i)
                {
                    CommentAjax commentA = new CommentAjax();
                    commentA.film_id = listcomment[i].film_id;
                    commentA.id_user = listcomment[i].id_user;
                    commentA.rate = listcomment[i].rate;
                    commentA.name_user = listcomment[i].name_user;
                    commentA.created_time =  listcomment[i].created_time.ToString();
                    listAjax.Add(commentA);
                }
               
                return Json(new { status = "SUCCESS",data = listAjax, msg = "Thanh cong !", JsonRequestBehavior.AllowGet });

            }
        }
        public ActionResult HistoryBooking()
        {
           
            var userInfomatiom = (usercgv)Session[Constants.Constants.USER_SESSION];
            var list = filmD.getBooking(userInfomatiom.id);
            var listHis = new List<HistoryBooking>();
            for(int i = 0;i < list.Count; i++)
            {
                HistoryBooking his = new HistoryBooking();
                his.nameFilm = filmD.getName(list[i].film_id).film_name;
                his.id = i + 1;
                his.roomName = roomD.getName(list[i].room_id).room_name;
                his.seatName = seatD.getName(list[i].seat_id).seat_name;
                his.status = list[i].status;
                his.schedulename = scheduleD.getName(list[i].schedule_id).dateschedule.ToString();
                his.amount = list[i].amount.ToString();
                string ngay = showtimeD.getName(list[i].showtime_id).start_time + "-" + showtimeD.getName(list[i].showtime_id).end_time;
                his.showtimeName = ngay;
                listHis.Add(his);

            }
            return View(listHis);
        }

    }
}