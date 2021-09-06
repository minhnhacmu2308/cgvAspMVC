using DatabaseIO;
using Model;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CGV.Utils;


namespace CGV.Controllers
{
    public class FilmController : Controller
    {
        // GET: Film
        FilmDao filmD = new FilmDao();
        ScheduleDao scheduleD = new ScheduleDao();
        RoomDao roomD = new RoomDao();
        ShowtimeDao showtimeD = new ShowtimeDao();
        BookingDao bookingDao = new BookingDao();
        SeatDao seatD = new SeatDao();
        CommentDao commentD = new CommentDao();
        MailUtils mailUtil = new MailUtils();
        PaymentStripe paymentOnline = new PaymentStripe();
      
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetailFilm(string id)
        {
            film film = filmD.getDetailFilm(id);
            var listSeat = seatD.getAll();
            ViewBag.listseat = listSeat;
            return View(film);
        }
        public ActionResult SearchFilm(string keySearch)
        {
            if (string.IsNullOrEmpty(keySearch)){
                return RedirectToAction("IndexUser","Home");
            }else{
                var listSearch = filmD.searchFilm(keySearch);
                ViewBag.keySearch = keySearch;
                var listSeat = seatD.getAll();
                ViewBag.listseat = listSeat;
                return View(listSearch);
            }      
        }  
        [HttpPost]
        public  JsonResult  getSchedule(string id)
        {
             List<schedule> list = scheduleD.getSchedule(id);      
             if(Int32.Parse(id) == 0){
                  return Json(new { status = Constants.Constants.STATUS_ERROR, msg = Constants.Constants.FILL_OUT_SCHEDULE, JsonRequestBehavior.AllowGet });
             }else{
                  string html = filmD.renderSchedule(list);
                  return Json(new { status = Constants.Constants.STATUS_OK, data = html, msg = Constants.Constants.MSG_SUCCESS, JsonRequestBehavior.AllowGet });
             }    
        } 
        [HttpPost]
        public JsonResult getShowtime(string id,int idRoom)
        {
            var idSche = Int32.Parse(id);
            var scheduledate = scheduleD.getName(idSche);
            string dtnow = DateTime.Today.ToString(Constants.Constants.FORMAT_DATE_STRING);
            int todayd = Int32.Parse(dtnow);
            string dateint = String.Format(Constants.Constants.FORMAT_DATE_SCHEDULE, scheduledate.dateschedule);
            int datesche = Int32.Parse(dateint);
            if (datesche == todayd){
                List<showtime> listShowtime = showtimeD.getShowtime(id, idRoom);
                string html = filmD.renderShowtime(listShowtime);
                return Json(new { status = Constants.Constants.STATUS_OK, data = html, msg = Constants.Constants.MSG_SUCCESS, JsonRequestBehavior.AllowGet });
            }else{
                List<showtime> listShowtime = showtimeD.getShowtimes(id, idRoom);
                string html = filmD.renderShowtime(listShowtime);
                return Json(new { status = Constants.Constants.STATUS_OK, data = html, msg = Constants.Constants.MSG_SUCCESS, JsonRequestBehavior.AllowGet });
            }           
        }
        [HttpPost]
        public JsonResult getRoom(int id)
        {
            List<room> listRoom = roomD.getRoom(id);
            string html = filmD.renderRoom(listRoom);        
            return Json(new { status = Constants.Constants.STATUS_OK, data = html, msg = Constants.Constants.MSG_SUCCESS, JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public JsonResult getSeat(int roomId, int showtimeId, int filmId, int scheduleId)
        {
            List<seat> listSeat = seatD.getSeatDone(roomId, showtimeId, filmId, scheduleId);
            List<seat> listSeatRoom = seatD.getSeatRoom(roomId);
            var listSeatActive = filmD.handleGetSeat(listSeatRoom, listSeat); ;
            string html = filmD.renderSeat(listSeatActive);        
            return Json(new { status = Constants.Constants.STATUS_OK, data = html, msg = Constants.Constants.MSG_SUCCESS, JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public JsonResult bookingTicket( int film_id,int schedule_id,int showtime_id, int room_id, int[] seat_id )
        {
            var userInfomatiom = (usercgv)Session[Constants.Constants.USER_SESSION];
            if(userInfomatiom == null ){
                return Json(new { status = Constants.Constants.STATUS_ERROR, msg = Constants.Constants.YOU_NEED_LOGIN, JsonRequestBehavior.AllowGet });
            }else{
                bool checkExist = bookingDao.checkBooking(film_id,schedule_id,showtime_id,room_id,seat_id);
                bool checkExistShowtime = showtimeD.checkExistShowtime(showtime_id);
                if (checkExist || !checkExistShowtime){
                    return Json(new { status = Constants.Constants.STATUS_ERROR, msg = Constants.Constants.CHANGE_DATABASE, JsonRequestBehavior.AllowGet });
                }else{
                    int amount = 0;
                    var listHis = new List<HistoryBooking>();
                    var listG = new List<String>();
                    int lengthSeat = seat_id.Length;
                    string stringDay = null;
                    HistoryBooking his = null;
                    for (int i = 0; i < lengthSeat; i++){
                        string dateschedule = String.Format(Constants.Constants.FORMAT_DATE, scheduleD.getName(schedule_id).dateschedule);
                        stringDay = showtimeD.getName(showtime_id).start_time + "-" + showtimeD.getName(showtime_id).end_time;
                        amount += Constants.Constants.PRICE_TICKET;
                        his = bookingDao.addHistoryBooking(film_id, i, room_id, seat_id[i], dateschedule, amount, stringDay, 0);
                        listHis.Add(his);
                        listG.Add(seatD.getName(seat_id[i]).seat_name);
                    }
                    DateTime now = DateTime.Now;
                    booking book = bookingDao.newObjectBooking(schedule_id, film_id, room_id, showtime_id, userInfomatiom.id, now.Ticks.ToString());
                    Session.Add(Constants.Constants.DATE_NOW_STRING, now.Ticks.ToString());
                    Session.Add(Constants.Constants.ORDER, book);
                    Session.Add(Constants.Constants.LENGTH_SEAT, seat_id);
                    Session session = paymentOnline.paymentOnline(filmD.getName(film_id).film_name, lengthSeat);
                    return Json(new { status = Constants.Constants.STATUS_OK, data1 = listHis, data2 = listG, data3 = session, msg = Constants.Constants.BOOKINGTICKET_SUCCESS, JsonRequestBehavior.AllowGet });
                }
               
            }        
        }
        [HttpPost]
        public JsonResult bookingTicke(int film_id, int schedule_id, int showtime_id, int room_id, int[] seat_id)
        {
            bool checkExist = bookingDao.checkBooking(film_id, schedule_id, showtime_id, room_id, seat_id);
            bool checkExistShowtime = showtimeD.checkExistShowtime(showtime_id);
            if (checkExist || !checkExistShowtime){
                return Json(new { status = Constants.Constants.STATUS_ERROR, msg = Constants.Constants.CHANGE_DATABASE, JsonRequestBehavior.AllowGet });
            }else{
                int amount = 0;
                var listHis = new List<HistoryBooking>();
                var listG = new List<String>();
                int lengthSeat = seat_id.Length;
                string stringDay = null;
                HistoryBooking his = null;
                for (int i = 0; i < lengthSeat; i++){
                    string dateschedule = String.Format(Constants.Constants.FORMAT_DATE, scheduleD.getName(schedule_id).dateschedule);
                    stringDay = showtimeD.getName(showtime_id).start_time + "-" + showtimeD.getName(showtime_id).end_time;
                    amount += Constants.Constants.PRICE_TICKET;
                    his = bookingDao.addHistoryBooking(film_id, i, room_id, seat_id[i], dateschedule, amount, stringDay, 0);
                    listHis.Add(his);
                    listG.Add(seatD.getName(seat_id[i]).seat_name);
                }
                DateTime now = DateTime.Now;
                booking book = bookingDao.newObjectBooking(schedule_id, film_id, room_id, showtime_id, 1, now.Ticks.ToString());
                Session.Add(Constants.Constants.LENGTH_SEAT, seat_id);
                Session.Add(Constants.Constants.ORDER, book);
                return Json(new { status = Constants.Constants.STATUS_OK, data1 = listHis, data2 = listG, msg = Constants.Constants.BOOKINGTICKET_SUCCESS, JsonRequestBehavior.AllowGet });
            }          
        }
        public ActionResult paymentSuccess()
        {
            var userInfomatiom = (usercgv)Session[Constants.Constants.USER_SESSION];
            var arrSeat = (int[])Session[Constants.Constants.LENGTH_SEAT];
            var order = (booking)Session[Constants.Constants.ORDER];
            var dateNowString = (string)Session[Constants.Constants.DATE_NOW_STRING];
            if (order != null && dateNowString != null){
                bookingDao.addBookingTicket(arrSeat, order, dateNowString, userInfomatiom.id,1);
                string stringLink = Constants.Constants.BOOKING_URL + dateNowString + "/" + userInfomatiom.id;
                var link = Constants.Constants.GOOGLE_URL + stringLink;
                string content = System.IO.File.ReadAllText(Server.MapPath(Constants.Constants.PATH_SENDMAIL));
                content = content.Replace("{{link}}", link);
                ViewBag.link = stringLink;
                mailUtil.sendMail(userInfomatiom.email,Constants.Constants.SUBJECT_EMAIL_QR,content);             
                Session.Remove(Constants.Constants.ORDER);
                Session.Remove(Constants.Constants.DATE_NOW_STRING);
                return View();
            }else{
                return RedirectToAction("IndexUser", "Home");
            }      
        }
        public ActionResult paymentAtCine()
        {
            var userInfomatiom = (usercgv)Session[Constants.Constants.USER_SESSION];
            var arrSeat = (int[])Session[Constants.Constants.LENGTH_SEAT];
            var order = (booking)Session[Constants.Constants.ORDER];
            var dateNowString = (string)Session[Constants.Constants.DATE_NOW_STRING];
            if (order != null && dateNowString != null && userInfomatiom !=null){
                bookingDao.addBookingTicket(arrSeat, order, dateNowString, userInfomatiom.id, 0);
                string stringLink = Constants.Constants.BOOKING_URL + dateNowString + "/" + userInfomatiom.id;
                var link = Constants.Constants.GOOGLE_URL + stringLink;
                string content = System.IO.File.ReadAllText(Server.MapPath(Constants.Constants.PATH_SENDMAIL));
                content = content.Replace("{{link}}", link);
                ViewBag.link = stringLink;
                mailUtil.sendMail(userInfomatiom.email, Constants.Constants.SUBJECT_EMAIL_QR, content);
                Session.Remove(Constants.Constants.ORDER);
                Session.Remove(Constants.Constants.DATE_NOW_STRING);
                return View();
            }else{
                return RedirectToAction("IndexUser", "Home");
            }
        }
        public JsonResult paymentAtCinema()
        {
            var arrSeat = (int[])Session[Constants.Constants.LENGTH_SEAT];
            var order = (booking)Session[Constants.Constants.ORDER];
            DateTime now = DateTime.Now;
            bookingDao.addBookingTicket(arrSeat, order, now.Ticks.ToString(),1,1);
            return Json(new { status = Constants.Constants.STATUS_OK, msg = Constants.Constants.BOOKINGTICKET_SUCCESS, JsonRequestBehavior.AllowGet });
        }
        public ActionResult paymentError()
        {
            var order = (booking)Session[Constants.Constants.ORDER];
            var dateNowString = (string)Session[Constants.Constants.DATE_NOW_STRING];
            if (order != null && !string.IsNullOrEmpty(dateNowString)){
                return View();
            }else{
                return RedirectToAction("IndexUser", "Home");
            }
        }
        public ActionResult qrResult(string dateNow,int id)
        {
            var list = filmD.getOrder(dateNow, id);
            var listHis = new List<HistoryBooking>();
            int lengthListOrder = list.Count;
            string stringDay = null;
            HistoryBooking his = null;
            for (int i = 0; i < lengthListOrder; i++){      
                string schedulename = scheduleD.getName(list[i].schedule_id).dateschedule.ToString();
                stringDay = showtimeD.getName(list[i].showtime_id).start_time + "-" + showtimeD.getName(list[i].showtime_id).end_time;
                his = bookingDao.addHistoryBooking(list[i].film_id, i, list[i].room_id, list[i].seat_id, schedulename, list[i].amount, stringDay, list[i].status);
                listHis.Add(his);
            }
            return View(listHis);
        }
     
        [HttpPost]
        public JsonResult postCommnet(int idFilm, string textComment, int numberstar)
        {
            var userInfomatiom = (usercgv)Session[Constants.Constants.USER_SESSION];
            if (userInfomatiom == null){
                return Json(new { status = Constants.Constants.STATUS_ERROR, msg = Constants.Constants.YOU_NEED_LOGIN, JsonRequestBehavior.AllowGet });
            }else{
                rating rating = new rating();
                rating.film_id = idFilm;
                rating.id_user = userInfomatiom.id;
                rating.name_user = userInfomatiom.username;
                rating.rate = textComment;
                rating.number_start = numberstar;
                commentD.comment(rating);
                var listcomment = commentD.getCommentById(idFilm);
                var listAjax = commentD.addCommentAjax(listcomment);
                return Json(new { status = Constants.Constants.STATUS_SUCCESS, data = listAjax, msg = Constants.Constants.MSG_SUCCESS, JsonRequestBehavior.AllowGet });
            }
        }
        [HttpPost]
        public JsonResult deleteComment(int id,int idFilm)
        {           
            commentD.deleteComment(id);
            var listcomment = commentD.getCommentById(idFilm);
            int lengthList = listcomment.Count;
            return Json(new { status = Constants.Constants.STATUS_SUCCESS,data = lengthList,  msg = Constants.Constants.MSG_SUCCESS, JsonRequestBehavior.AllowGet });
        }
        /**
         * get history booking ticket of user
         * @return
         */
        public ActionResult HistoryBooking()
        {
            var user = Session[Constants.Constants.USER_SESSION];
            if (user == null){
                return RedirectToAction("IndexUser", "Home");
            }else{
                var userInfomatiom = (usercgv)Session[Constants.Constants.USER_SESSION];
                var list = filmD.getBooking(userInfomatiom.id);
                var listHis = new List<HistoryBooking>();
                int lengthListOrder = list.Count;
                string stringDay = null;
                HistoryBooking his = null;
                for (int i = 0; i < lengthListOrder; i++){
                    string dateschedule = String.Format(Constants.Constants.FORMAT_DATE, scheduleD.getName(list[i].schedule_id).dateschedule);
                    stringDay = showtimeD.getName(list[i].showtime_id).start_time + "-" + showtimeD.getName(list[i].showtime_id).end_time;
                    his = bookingDao.addHistoryBooking(list[i].film_id, i, list[i].room_id, list[i].seat_id, dateschedule, 3, stringDay, list[i].status);
                    listHis.Add(his);
                }
                return View(listHis);
            }
            
        }

    }
}