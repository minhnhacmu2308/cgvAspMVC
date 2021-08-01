using DatabaseIO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CGV.Controllers
{
    public class FilmController : Controller
    {
        // GET: Film
        FilmDao filmD = new FilmDao();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetailFilm(string id)
        {
            film film = filmD.getDetailFilm(id);
            return View(film);
        }
        public ActionResult SearchFilm(string keySearch)
        {
            if (string.IsNullOrEmpty(keySearch))
            {
                return RedirectToAction("IndexUser","Home");
            }
            else
            {
                var listSearch = filmD.searchFilm(keySearch);
                ViewBag.keySearch = keySearch;
                return View(listSearch);
            }
           
        }
       
    }
}