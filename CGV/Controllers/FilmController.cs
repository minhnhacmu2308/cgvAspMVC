using DatabaseIO;
using Model;
using QRCoder;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;


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
                int amount = 0;
                var listHis = new List<HistoryBooking>();
                var listG = new List<String>();
                int lengthSeat = seat_id.Length;
                for (int i = 0; i < lengthSeat; i++)
                {
                    HistoryBooking his = new HistoryBooking();
                    his.nameFilm = filmD.getName(film_id).film_name;
                    his.id = i + 1;
                    his.roomName = roomD.getName(room_id).room_name;
                    his.seatName = seatD.getName(seat_id[i]).seat_name;
                    his.schedulename = scheduleD.getName(schedule_id).dateschedule.ToString();
                    amount += Constants.Constants.PRICE_TICKET;
                    his.amount = amount.ToString();


                    string ngay = showtimeD.getName(showtime_id).start_time + "-" + showtimeD.getName(showtime_id).end_time;
                    his.showtimeName = ngay;
                    listHis.Add(his);
                    listG.Add(seatD.getName(seat_id[i]).seat_name);

                }

                StripeConfiguration.ApiKey = "sk_test_51Itn76AY7zpl2tqotBGt23IEZmOSCZOmOnpgAhVQWIvua4g5c4G74Au5P54rWqNofPUw1DZ7TdHzlBhCWJCJa81W00V76C7Z2n";
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
                        Description ="Phim hot 2021",
                        Amount = Constants.Constants.PRICE_TICKET,
                        Currency ="usd",
                        Quantity = lengthSeat,
                        Images = new List<string>
                        {
                            HttpUtility.UrlPathEncode("https://ci4.googleusercontent.com/proxy/HYuBnblArUYo9VwBr3QIRdi26frt7JG-rbJ2s5fYfFeNu8i_SVLXb4SQWPxsZNj-qVOmbFJT3Sy_XmSJQPmVCDPX3MFJCcE1ct5YGZ8O_E6vw-uL3Hr7vK22FOVrqgs=s0-d-e1-ft#https://i.pinimg.com/originals/fb/45/ba/fb45baac1eed3c1b19d4aad23b054fa8.jpg")
                        }
                    },
                },
                    SuccessUrl = "https://localhost:44313/payment/success",
                    CancelUrl = "https://localhost:44313/payment/error",
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
               
                return Json(new { status = "OK", data1 = listHis, data2 = listG, data3 = session, msg = "✅ Đặt vé thành công", JsonRequestBehavior.AllowGet });
           

            }
           
        }
        public void sendMailQR(string email,string body)
        {

            var formEmailAddress = ConfigurationManager.AppSettings["FormEmailAddress"].ToString();
            var formEmailDisplayName = ConfigurationManager.AppSettings["FormEmailDisplayName"].ToString();
            var formEmailPassword = ConfigurationManager.AppSettings["FormEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPost"].ToString();
            bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
            MailMessage message = new MailMessage(new MailAddress(formEmailAddress, formEmailDisplayName), new MailAddress(email));
            message.Subject = "QR về hóa đơn của quý khách";
            message.IsBodyHtml = true;
            message.Body = body;
            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(formEmailAddress, formEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enableSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }
        public ActionResult paymentSuccess()
        {
            var userInfomatiom = (usercgv)Session[Constants.Constants.USER_SESSION];
            var arrSeat = (int[])Session[Constants.Constants.LENGTH_SEAT];
            var order = (booking)Session[Constants.Constants.ORDER];
            var dateNowString = (string)Session[Constants.Constants.DATE_NOW_STRING];
            if (order != null && dateNowString != null)
            {
                booking book = new booking();
                for(int i = 0;i< arrSeat.Length; i++)
                {
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
               
                string a = "https://localhost:44313/booking/ticket/" + dateNowString + "/" + userInfomatiom.id;
                ViewBag.link = a;
                var link = "https://chart.googleapis.com/chart?chs=300x300&cht=qr&chl=" + a;
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Assets/html/sendQRMail.html"));
                content = content.Replace("{{link}}", link);
                sendMailQR(userInfomatiom.email, content);
                Session.Remove(Constants.Constants.ORDER);
                Session.Remove(Constants.Constants.DATE_NOW_STRING);
                return View();

            }
            else
            {
                return RedirectToAction("IndexUser", "Home");
            }
         
           
           
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
            for (int i = 0; i < list.Count; i++)
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
                his.amount = 3.ToString();
                string ngay = showtimeD.getName(list[i].showtime_id).start_time + "-" + showtimeD.getName(list[i].showtime_id).end_time;
                his.showtimeName = ngay;
                listHis.Add(his);

            }
            return View(listHis);
        }

    }
}