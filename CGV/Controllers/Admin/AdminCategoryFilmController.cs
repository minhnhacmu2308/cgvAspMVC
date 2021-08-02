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
        private MyDB db = new MyDB();
        // GET: AdminCategoryFilm
        public ActionResult Index()
        {
            List<category_film> list = db.category_film.ToList();  
            return View(list);
        }
    }
}