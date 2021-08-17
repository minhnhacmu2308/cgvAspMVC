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
        public MyDB db = new MyDB();
        HomeDao home = new HomeDao();

        // GET: AdminHome
        public ActionResult Index()
        {
            if(Session["usr"] == null)
            {
                return RedirectToAction("Index", "AdminAuthen");
            }
            ViewBag.Month1 = home.Statictis(1);
            ViewBag.Month2 = home.Statictis(2);
            ViewBag.Month3 = home.Statictis(3);
            ViewBag.Month4 = home.Statictis(4);
            ViewBag.Month5 = home.Statictis(5);
            ViewBag.Month6 = home.Statictis(6);
            ViewBag.Month7 = home.Statictis(7);
            ViewBag.Month8 = home.Statictis(8);
            ViewBag.Month9 = home.Statictis(9);
            ViewBag.Month10 = home.Statictis(10);
            ViewBag.Month11 = home.Statictis(11);
            ViewBag.Month12 = home.Statictis(12);
            ViewBag.Count1 = home.CountTicket(1);
            ViewBag.Count2 = home.CountTicket(2);
            ViewBag.Count3 = home.CountTicket(3);
            ViewBag.Count4 = home.CountTicket(4);
            ViewBag.Count5 = home.CountTicket(5);
            ViewBag.Count6 = home.CountTicket(6);
            ViewBag.Count7 = home.CountTicket(7);
            ViewBag.Count8 = home.CountTicket(8);
            ViewBag.Count9 = home.CountTicket(9);
            ViewBag.Count10 = home.CountTicket(10);
            ViewBag.Count11 = home.CountTicket(11);
            ViewBag.Count12 = home.CountTicket(12);
            ViewBag.CountFilm= db.films.Count(); 
            ViewBag.CountUser = db.usercgvs.Count();
            ViewBag.CountBooking = db.bookings.Count();
            ViewBag.SumMoney = db.bookings.Sum(b => b.amount);
            return View();
        }
    }
}