using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using Model;
namespace CGV.Controllers.Admin
{
    public class AdminFilmController : Controller
    {
        public MyDB db = new MyDB();
        // GET: AdminFilm
        public ActionResult Index()
        {
            List<film> list = db.films.OrderByDescending(f => f.id).ToList();
            return View(list);
        }
    }
}