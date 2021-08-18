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
        ShowtimeDao show = new ShowtimeDao();
        public MyDB db = new MyDB();
        // GET: AdminShowtime
        public ActionResult Index(string mess)
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Index", "AdminAuthen");
            }
            ViewBag.Msg = mess;
            List<showtime> list = db.showtimes.OrderByDescending(s => s.id).ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var start = form["start"];
            var end = form["end"];
            var scheid = form["id"];
            show.Add(scheid, start,end);
            var message = "Thêm thành công";
            return RedirectToAction("Index", new { mess = message });
        }
        public ActionResult Update(FormCollection form)
        {
            var start = form["start"];
            var end = form["end"];
            var id = form["id"];
            show.Update(start, end, id);
            var message = "Cập nhập thành công";
            return RedirectToAction("Index", new { mess = message });
        }
        public ActionResult Delete(FormCollection form)
        {

            var id = form["id"];
            show.Delete(id);
            var message = "Xóa thành công";
            return RedirectToAction("Index", new { mess = message });

        }
    }
}