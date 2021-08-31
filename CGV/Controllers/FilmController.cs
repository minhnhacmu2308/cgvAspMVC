using DatabaseIO;
using Model;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        SeatDao seatD = new SeatDao();
        CommentDao commentD = new CommentDao();
        MailUtils mailUtil = new MailUtils();
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
            }
            else{
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
                string html = "<option value="+0+">"+Constants.Constants.CHOOSE_SCHEDULE+"</option>";
                if(Int32.Parse(id)==0){
                    return Json(new { status = Constants.Constants.STATUS_ERROR, msg = Constants.Constants.FILL_OUT_SCHEDULE, JsonRequestBehavior.AllowGet });
                }else{
                    foreach (var item in list){
                        var roomOb = roomD.getNameRoomSchedule(item.id);
                        string dateschedule = String.Format(Constants.Constants.FORMAT_DATE, item.dateschedule);
                        html += "<option value=" + item.id + ">" + dateschedule +"" +"("+ roomOb.room_name+ ")"+ "</option>";
                    }
                    return Json(new { status = Constants.Constants.STATUS_OK, data = html, msg = Constants.Constants.MSG_SUCCESS, JsonRequestBehavior.AllowGet });
                 }    
        } 
        [HttpPost]
        public JsonResult getShowtime(string id,int idRoom)
        {
           
            List<showtime> listShowtime = showtimeD.getShowtime(id,idRoom);
            string html = " <option>"+Constants.Constants.CHOOSE_SHOWTIME+"</option>";
            foreach(var item in listShowtime){
                string showtimeString = item.start_time + " - " + item.end_time;
                html += "<option value=" + item.id + ">" + showtimeString + "</option>";
            }
            return Json(new { status = Constants.Constants.STATUS_OK, data = html, msg = Constants.Constants.MSG_SUCCESS, JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public JsonResult getRoom(int id)
        {
            List<room> listRoom = roomD.getRoom(id);
            string html = " <option>"+Constants.Constants.CHOOSE_ROOM+"</option>";
            foreach (var item in listRoom){
                html += "<option value="+item.id+">"+item.room_name+"</option>";
            }
            return Json(new { status = Constants.Constants.STATUS_OK, data = html, msg = Constants.Constants.MSG_SUCCESS, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult getSeat(int roomId, int showtimeId, int filmId, int scheduleId)
        {
            List<seat> listSeat = seatD.getSeatDone(roomId, showtimeId, filmId, scheduleId);
            List<seat> listAllSeat = seatD.getAll();
            List<seat> listSeatRoom = seatD.getSeatRoom(roomId);
            var listSeatActive = new List<SeatActive>();
            for(int i = 0; i< listSeatRoom.Count; i++){
                SeatActive sa = new SeatActive();
                sa.id = listSeatRoom[i].id;
                sa.seat_name = listSeatRoom[i].seat_name;
                sa.active = 0;
                listSeatActive.Add(sa);
            }
            for (int k = 0; k < listSeatActive.Count; k++) {
                for (int j = 0; j < listSeat.Count; j++){
                    if (listSeatActive[k].id == listSeat[j].id){
                        listSeatActive[k].active = 1;
                    }
                }
            }
            string html = "";        
            for(int j = 0; j < listSeatActive.Count; j++){
                var nameId = "in" + listSeatActive[j].id;
                var nameIdDiv = "div" + listSeatActive[j].id;
                var nameIdSeat = "id" + listSeatActive[j].id;
                var check = "false";
                if (listSeatActive[j].active == 0){
                    html += "<div style='display: flex; align-content: center; justify-content: center; justify-items: center; border: 1px solid; background-color:#DDDDDD;line-height: 50px; height: 50px; width: 104.2px; margin-right: 6.4px; margin-bottom: 6.4px '><p>🪑" + listSeatActive[j].seat_name + "</p></div>";
                }else{
                    html += "<div id='"+ nameIdDiv + "' onclick=onChoose("+listSeatActive[j].id + ") style='display: flex; align-content: center; justify-content: center;justify-items: center; border: 1px solid red; line-height: 50px; height: 50px; width: 104.2px; margin-right: 6.4px; margin-bottom: 6.4px '><p>🪑" + listSeatActive[j].seat_name + "</p><input type='hidden' value='"+ check + "' id='"+ nameId + "'/><input type='hidden' value='"+ listSeatActive[j].id + "' id='"+ nameIdSeat + "' /></div>";
                }               
            }
            return Json(new { status = Constants.Constants.STATUS_OK, data = html, msg = Constants.Constants.MSG_SUCCESS, JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public JsonResult bookingTicket( int film_id,int schedule_id,int showtime_id, int room_id, int[] seat_id )
        {
            var userInfomatiom = (usercgv)Session[Constants.Constants.USER_SESSION];
            if(userInfomatiom == null ){
                return Json(new { status = Constants.Constants.STATUS_SUCCESS, msg = Constants.Constants.YOU_NEED_LOGIN, JsonRequestBehavior.AllowGet });
            }else{
                int amount = 0;
                var listHis = new List<HistoryBooking>();
                var listG = new List<String>();
                int lengthSeat = seat_id.Length;
                for (int i = 0; i < lengthSeat; i++){
                    HistoryBooking his = new HistoryBooking();
                    his.nameFilm = filmD.getName(film_id).film_name;
                    his.id = i + 1;
                    his.roomName = roomD.getName(room_id).room_name;
                    his.seatName = seatD.getName(seat_id[i]).seat_name;
                    string dateschedule = String.Format(Constants.Constants.FORMAT_DATE, scheduleD.getName(schedule_id).dateschedule);
                    his.schedulename = dateschedule;
                    amount += Constants.Constants.PRICE_TICKET;
                    his.amount = amount.ToString();
                    string ngay = showtimeD.getName(showtime_id).start_time + "-" + showtimeD.getName(showtime_id).end_time;
                    his.showtimeName = ngay;
                    listHis.Add(his);
                    listG.Add(seatD.getName(seat_id[i]).seat_name);
                }

                StripeConfiguration.ApiKey = Constants.Constants.API_KEY;
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                    LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Name = filmD.getName(film_id).film_name,
                        Description =Constants.Constants.DESCRIPTION_BOOKING,
                        Amount = Constants.Constants.PRICE_TICKET,
                        Currency =Constants.Constants.USD,
                        Quantity = lengthSeat,
                        Images = new List<string>
                        {
                            HttpUtility.UrlPathEncode(Constants.Constants.IMAGE_URL)
                        }
                    },
                },
                    SuccessUrl = Constants.Constants.SUCCESS_URL,
                    CancelUrl = Constants.Constants.CANCEL_URL,
                    PaymentIntentData = new SessionPaymentIntentDataOptions
                    {
                        Metadata = new Dictionary<string, string>
                    {
                         {"Order_id","1234" },
                         {"sdsd","hello" },
                    }

                    }
                };
                DateTime now = DateTime.Now;
                booking book = new booking();
                book.schedule_id = schedule_id;
                book.film_id = film_id;
                book.room_id = room_id;
                book.showtime_id = showtime_id;
                book.id_user = userInfomatiom.id;
                book.status = 0;
                book.create_time = now.Ticks.ToString();
                book.amount = 3;              
                Session.Add(Constants.Constants.DATE_NOW_STRING, now.Ticks.ToString());
                Session.Add(Constants.Constants.ORDER, book);
                Session.Add(Constants.Constants.LENGTH_SEAT, seat_id);
                var service = new SessionService();
                Session session = service.Create(options);       
                return Json(new { status = Constants.Constants.STATUS_OK, data1 = listHis, data2 = listG, data3 = session, msg = Constants.Constants.BOOKINGTICKET_SUCCESS, JsonRequestBehavior.AllowGet });
            }
           
        }
        [HttpPost]
        public JsonResult bookingTicke(int film_id, int schedule_id, int showtime_id, int room_id, int[] seat_id)
        {
            int amount = 0;
            var listHis = new List<HistoryBooking>();
            var listG = new List<String>();
            int lengthSeat = seat_id.Length;
            for (int i = 0; i < lengthSeat; i++){
                HistoryBooking his = new HistoryBooking();
                his.nameFilm = filmD.getName(film_id).film_name;
                his.id = i + 1;
                his.roomName = roomD.getName(room_id).room_name;
                his.seatName = seatD.getName(seat_id[i]).seat_name;
                string dateschedule = String.Format(Constants.Constants.FORMAT_DATE, scheduleD.getName(schedule_id).dateschedule);
                his.schedulename = dateschedule;
                amount += Constants.Constants.PRICE_TICKET;
                his.amount = amount.ToString();
                string ngay = showtimeD.getName(showtime_id).start_time + "-" + showtimeD.getName(showtime_id).end_time;
                his.showtimeName = ngay;
                listHis.Add(his);
                listG.Add(seatD.getName(seat_id[i]).seat_name);

            }
            DateTime now = DateTime.Now;
            booking book = new booking();
            book.schedule_id = schedule_id;
            book.film_id = film_id;
            book.room_id = room_id;
            book.showtime_id = showtime_id;
            book.id_user = 1;
            book.status = 0;
            book.create_time = now.Ticks.ToString();
            book.amount = 3;
            Session.Add(Constants.Constants.LENGTH_SEAT, seat_id);
            Session.Add(Constants.Constants.ORDER, book);
            return Json(new { status = Constants.Constants.STATUS_OK, data1 = listHis, data2 = listG, msg = Constants.Constants.BOOKINGTICKET_SUCCESS, JsonRequestBehavior.AllowGet });
        }
        public ActionResult paymentSuccess()
        {
            var userInfomatiom = (usercgv)Session[Constants.Constants.USER_SESSION];
            var arrSeat = (int[])Session[Constants.Constants.LENGTH_SEAT];
            var order = (booking)Session[Constants.Constants.ORDER];
            var dateNowString = (string)Session[Constants.Constants.DATE_NOW_STRING];
            if (order != null && dateNowString != null){
                booking book = new booking();
                for(int i = 0;i< arrSeat.Length; i++){
                    book.schedule_id = order.schedule_id;
                    book.film_id = order.film_id;
                    book.seat_id = arrSeat[i];
                    book.room_id = order.room_id;
                    book.showtime_id = order.showtime_id;
                    book.id_user = userInfomatiom.id;
                    book.status = 1;
                    book.create_time = order.create_time;
                    book.amount = 3;
                    filmD.bookingTicket(book, dateNowString);
                } 
                string a = Constants.Constants.BOOKING_URL + dateNowString + "/" + userInfomatiom.id;
                ViewBag.link = a;
                var link = Constants.Constants.GOOGLE_URL + a;
                string content = System.IO.File.ReadAllText(Server.MapPath(Constants.Constants.PATH_SENDMAIL));
                content = content.Replace("{{link}}", link);
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
                booking book = new booking();
                for (int i = 0; i < arrSeat.Length; i++){
                    book.schedule_id = order.schedule_id;
                    book.film_id = order.film_id;
                    book.seat_id = arrSeat[i];
                    book.room_id = order.room_id;
                    book.showtime_id = order.showtime_id;
                    book.id_user = userInfomatiom.id;
                    book.status = 0;
                    book.create_time = order.create_time;
                    book.amount = 3;
                    filmD.bookingTicket(book, dateNowString);
                }
                string a = Constants.Constants.BOOKING_URL + dateNowString + "/" + userInfomatiom.id;
                ViewBag.link = a;
                var link = Constants.Constants.GOOGLE_URL + a;
                string content = System.IO.File.ReadAllText(Server.MapPath(Constants.Constants.PATH_SENDMAIL));
                content = content.Replace("{{link}}", link);
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
            booking book = new booking();
            for (int i = 0; i < arrSeat.Length; i++){
                book.schedule_id = order.schedule_id;
                book.film_id = order.film_id;
                book.seat_id = arrSeat[i];
                book.room_id = order.room_id;
                book.showtime_id = order.showtime_id;
                book.id_user = 1;
                book.status = 1;
                book.create_time = order.create_time;
                book.amount = 3;
                filmD.bookingTicket(book, "123456");
            }
            return Json(new { status = Constants.Constants.STATUS_OK, msg = Constants.Constants.BOOKINGTICKET_SUCCESS, JsonRequestBehavior.AllowGet });
        }
        public ActionResult paymentError()
        {

            var order = (booking)Session[Constants.Constants.ORDER];
            var dateNowString = (string)Session[Constants.Constants.DATE_NOW_STRING];
            if (order != null && dateNowString != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("IndexUser", "Home");
            }
        }
        public ActionResult qrResult(string dateNow,int id)
        {
            var list = filmD.getOrder(dateNow, id);
            var listHis = new List<HistoryBooking>();
            for (int i = 0; i < list.Count; i++){
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
            return Json(new { status = Constants.Constants.STATUS_OK, data = list, msg = Constants.Constants.MSG_SUCCESS, JsonRequestBehavior.AllowGet });
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
               
                var listAjax = new List<CommentAjax>();
                for (int i = 0; i < listcomment.Count; ++i){
                    CommentAjax commentA = new CommentAjax();
                    commentA.id = listcomment[i].id;
                    commentA.film_id = listcomment[i].film_id;
                    commentA.id_user = listcomment[i].id_user;
                    commentA.number_start = listcomment[i].number_start;
                    commentA.rate = listcomment[i].rate;
                    commentA.name_user = listcomment[i].name_user;
                    commentA.created_time = listcomment[i].created_time.ToString();
                    listAjax.Add(commentA);
                }    
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
        public ActionResult HistoryBooking()
        {
            var user = Session[Constants.Constants.USER_SESSION];
            if (user == null){
                return RedirectToAction("IndexUser", "Home");
            }else{
                var userInfomatiom = (usercgv)Session[Constants.Constants.USER_SESSION];
                var list = filmD.getBooking(userInfomatiom.id);
                var listHis = new List<HistoryBooking>();
                for (int i = 0; i < list.Count; i++){
                    HistoryBooking his = new HistoryBooking();
                    his.nameFilm = filmD.getName(list[i].film_id).film_name;
                    his.id = i + 1;
                    his.roomName = roomD.getName(list[i].room_id).room_name;
                    his.seatName = seatD.getName(list[i].seat_id).seat_name;
                    his.status = list[i].status;
                    string dateschedule = String.Format(Constants.Constants.FORMAT_DATE, scheduleD.getName(list[i].schedule_id).dateschedule);
                    his.schedulename = dateschedule;
                    his.amount = 3.ToString();
                    string ngay = showtimeD.getName(list[i].showtime_id).start_time + "-" + showtimeD.getName(list[i].showtime_id).end_time;
                    his.showtimeName = ngay;
                    listHis.Add(his);
                }
                return View(listHis);
            }
            
        }

    }
}