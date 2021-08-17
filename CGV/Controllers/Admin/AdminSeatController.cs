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
        SeatDao seat = new SeatDao();
        public MyDB db = new MyDB();
        // GET: AdminSeat
        public ActionResult Index(string mess)
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Index", "AdminAuthen");
            }
            ViewBag.Msg = mess;
            List<seat> list = db.seats.ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var name = form["seatname"];
            seat.Add(name);
            var message = "Thêm thành công";
            return RedirectToAction("Index", new { mess = message });
        }
        public ActionResult Update(FormCollection form)
        {
            var name = form["seatname"];
            var id = form["id"];
            seat.Update(name, id);
            var message = "Cập nhập thành công";
            return RedirectToAction("Index", new { mess = message });
        }
        public ActionResult Delete(FormCollection form)
        {

            var id = form["id"];
            seat.Delete(id);
            var message = "Xóa thành công";
            return RedirectToAction("Index", new { mess = message });

        }
    }
}