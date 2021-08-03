using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using Model;
namespace CGV.Controllers.Admin
{
    public class AdminBookingController : Controller
    {
        private MyDB db = new MyDB();
        // GET: AdminBooking
        public ActionResult Index()
        {
            List<booking> list = db.bookings.ToList();
            return View(list);
        }
    }
}