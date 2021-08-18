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
        public ActionResult Index(string mess)
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Index", "AdminAuthen");
            }
            ViewBag.Msg = mess;
            List<room> list = db.rooms.ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var name = form["roomname"];
            bool result = room.checkName(name);
            if (result)
            {
                var message = "Phòng đã tồn tại";
                return RedirectToAction("Index", new { mess = message });
            }
            else
            {
                room.Add(name);
                var message = "Thêm thành công";
                return RedirectToAction("Index", new { mess = message });
            }
        }
        public ActionResult Update(FormCollection form)
        {
            var name = form["roomname"];
            var id = form["id"];
            room.Update(name, id);
            var message = "Cập nhập thành công";
            return RedirectToAction("Index", new { mess = message });
        }
        public ActionResult Delete(FormCollection form)
        {

            var id = form["id"];
            room.Delete(id);
            var message = "Xóa thành công";
            return RedirectToAction("Index", new { mess = message });

        }
    }
}