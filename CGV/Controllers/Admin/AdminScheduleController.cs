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
        public ActionResult Index()
        {
            List<schedule> list = db.schedules.OrderByDescending(s => s.dateschedule ).ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var dateschedule = form["dateschedule"];
            var filmid = form["filmid"];
            sche.Add(filmid,dateschedule);
            return RedirectToAction("Index");
        }
        public ActionResult Update(FormCollection form)
        {
            var dateschedule = form["dateschedule"];
            var filmid = form["filmid"];
            var id = form["id"];
            sche.Update(filmid, dateschedule,id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(FormCollection form)
        {

            var id = form["id"];
            sche.Delete(id);
            return RedirectToAction("Index");

        }
    }
}