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
        RoomDao room = new RoomDao();
        public MyDB db = new MyDB();
        // GET: AdminRoom
        public ActionResult Index()
        {
            List<room> list = db.rooms.ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var name = form["roomname"];
            room.Add(name);
            return RedirectToAction("Index");
        }
        public ActionResult Update(FormCollection form)
        {
            var name = form["roomname"];
            var id = form["id"];
            room.Update(name, id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(FormCollection form)
        {

            var id = form["id"];
            room.Delete(id);
            return RedirectToAction("Index");

        }
    }
}