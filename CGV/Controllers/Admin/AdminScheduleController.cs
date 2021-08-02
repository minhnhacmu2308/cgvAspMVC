using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using Model;
namespace CGV.Controllers.Admin
{
    public class AdminScheduleController : Controller
    {
        public MyDB db = new MyDB();
        // GET: AdminSchedule
        public ActionResult Index()
        {
            List<schedule> list = db.schedules.ToList();
            return View(list);
        }
    }
}