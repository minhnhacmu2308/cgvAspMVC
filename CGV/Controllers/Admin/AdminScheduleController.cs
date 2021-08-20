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
        ScheduleDao sche = new ScheduleDao();
        public MyDB db = new MyDB();
        // GET: AdminSchedule
        public ActionResult Index(string mess)
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Index", "AdminAuthen");
            }
            ViewBag.Msg = mess;
            List<schedule> list = db.schedules.OrderByDescending(s => s.dateschedule ).ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var dateschedule = form["dateschedule"];
            DateTime datesche =  DateTime.Parse(dateschedule);
            var filmid = form["filmid"];
            int idf = Int32.Parse(filmid);
            bool result = sche.checkName(datesche, idf);
            if (result)
            {
                var message = "Lịch chiếu của phim đã tồn tại";
                return RedirectToAction("Index", new { mess = message });
            }
            else
            {
                sche.Add(filmid, dateschedule);
                var message = "Thêm thành công";
                return RedirectToAction("Index", new { mess = message });
            }
        }
        public ActionResult Update(FormCollection form)
        {
            var dateschedule = form["dateschedule"];
            var filmid = form["filmid"];
            var id = form["id"];
            sche.Update(filmid, dateschedule,id);
            var message = "Cập nhập thành công";
            return RedirectToAction("Index", new { mess = message });
        }
        public ActionResult Delete(FormCollection form)
        {

            var id = form["id"];
            sche.Delete(id);
            var message = "Xóa thành công";
            return RedirectToAction("Index", new { mess = message });

        }
    }
}