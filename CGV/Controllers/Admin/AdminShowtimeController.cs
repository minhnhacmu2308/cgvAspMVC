using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using Model;
namespace CGV.Controllers.Admin
{
    public class AdminShowtimeController : Controller
    {
        public MyDB db = new MyDB();
        // GET: AdminShowtime
        public ActionResult Index()
        {
            List<showtime> list = db.showtimes.ToList();
            return View(list);
        }
    }
}