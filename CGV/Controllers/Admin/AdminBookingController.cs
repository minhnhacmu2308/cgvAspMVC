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
        BookingDao bk = new BookingDao();
        private MyDB db = new MyDB();
        // GET: AdminBooking
        public ActionResult Index()
        {
            List<booking> list = db.bookings.OrderByDescending(b => b.id).ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Accept(FormCollection form)
        {
            var id = form["id"];
            bk.Accpect(id);
            return RedirectToAction("Index");
        }
    }
}