using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using Model;
namespace CGV.Controllers.Admin
{
    public class AdminSeatController : Controller
    {
        public MyDB db = new MyDB();
        // GET: AdminSeat
        public ActionResult Index()
        {
            List<seat> list = db.seats.ToList();
            return View(list);
        }
    }
}