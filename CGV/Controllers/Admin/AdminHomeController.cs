using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using Model;

namespace CGV.Controllers.Admin
{
    public class AdminHomeController : Controller
    {
        private MyDB db = new MyDB();
        
        // GET: AdminHome
        public ActionResult Index()
        {
            ViewBag.CountFilm= db.films.Count();
            ViewBag.CountUser = db.usercgvs.Count();
            ViewBag.CountBooking = db.bookings.Count();
            ViewBag.SumMoney = db.bookings.Sum(b => b.amount);
            return View();
        }
    }
}