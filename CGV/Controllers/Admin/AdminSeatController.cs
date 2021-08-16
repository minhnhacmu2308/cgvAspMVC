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
        public ActionResult Index()
        {
            List<seat> list = db.seats.ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var name = form["seatname"];
            seat.Add(name);
            return RedirectToAction("Index");
        }
        public ActionResult Update(FormCollection form)
        {
            var name = form["seatname"];
            var id = form["id"];
            seat.Update(name, id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(FormCollection form)
        {

            var id = form["id"];
            seat.Delete(id);
            return RedirectToAction("Index");

        }
    }
}