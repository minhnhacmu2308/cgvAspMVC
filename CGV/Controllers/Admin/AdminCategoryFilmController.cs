using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using Model;
namespace CGV.Controllers.Admin
{
    public class AdminCategoryFilmController : Controller
    {
        CategoryFilmDao cfilm = new CategoryFilmDao();
        
        private MyDB db = new MyDB();
        // GET: AdminCategoryFilm
        public ActionResult Index(string mess)
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Index", "AdminAuthen");
            }
            ViewBag.Msg = mess;
            List<category_film> list = db.category_film.ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var name = form["categoryfilm"];
            bool result = cfilm.checkName(name);
            if (result)
            {
                var message = "Loại phim đã tồn tại";
                return RedirectToAction("Index", new { mess = message });
            }
            else
            {
                cfilm.Add(name);
                var message = "Thêm thành công";
                return RedirectToAction("Index", new { mess = message });
            }
        }
        public ActionResult Update(FormCollection form)
        {
            var name = form["categoryfilm"];
            var id = form["id"];
            cfilm.Update(name, id);
            var message = "Cập nhập thành công";
            return RedirectToAction("Index", new { mess = message });
        }
        public ActionResult Delete(FormCollection form)
        {
           
            var id = form["id"];
            cfilm.Delete(id);
            var message = "Xóa thành công";
            return RedirectToAction("Index", new { mess = message });

        }
    }
}