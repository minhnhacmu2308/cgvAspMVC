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
        public ActionResult Index()
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Index", "AdminAuthen");
            }
            List<category_film> list = db.category_film.ToList();
            ViewBag.Data = list;
            return View();
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var name = form["categoryfilm"];
            cfilm.Add(name);
            ViewBag.Msg = "Thêm thành công";
            List<category_film> list = db.category_film.ToList();
            ViewBag.Data = list;
            return RedirectToAction("Index");
        }
        public ActionResult Update(FormCollection form)
        {
            var name = form["categoryfilm"];
            var id = form["id"];
            cfilm.Update(name, id);
            ViewBag.Msg = "Sửa thành công";
            List<category_film> list = db.category_film.ToList();
            ViewBag.Data = list;
            return RedirectToAction("Index");
        }
        public ActionResult Delete(FormCollection form)
        {
           
            var id = form["id"];
            cfilm.Delete(id);
            ViewBag.Msg = "Xóa thành công";
            List<category_film> list = db.category_film.ToList();
            ViewBag.Data = list;
            return View("Index");

        }
    }
}