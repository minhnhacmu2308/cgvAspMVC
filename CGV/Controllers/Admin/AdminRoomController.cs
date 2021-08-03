using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using Model;
namespace CGV.Controllers.Admin
{
    public class AdminRoomController : Controller
    {
        public MyDB db = new MyDB();
        // GET: AdminRoom
        public ActionResult Index()
        {
            List<room> list = db.rooms.ToList();
            return View(list);
        }
    }
}