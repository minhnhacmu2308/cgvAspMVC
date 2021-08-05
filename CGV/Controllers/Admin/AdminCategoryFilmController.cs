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
            List<category_film> list = db.category_film.ToList();  
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var name = form["categoryfilm"];
            cfilm.Add(name);
            return RedirectToAction("Index");
        }
        public ActionResult Update(FormCollection form)
        {
            var name = form["categoryfilm"];
            var id = form["id"];
            cfilm.Update(name, id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(FormCollection form)
        {
           
            var id = form["id"];
            cfilm.Delete(id);
            return RedirectToAction("Index");

        }
    }
}