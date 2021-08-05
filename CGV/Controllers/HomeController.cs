using DatabaseIO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CGV.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult IndexUser()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult FilmComingSoon()
        {
            HomeDao homeDao = new HomeDao();
            List<film> list = homeDao.getFilmComingSoon();
            return PartialView(list);
        }
        [ChildActionOnly]
        public ActionResult FilmNowShowing()
        {
            HomeDao homeDao = new HomeDao();
            List<film> list = homeDao.getFilmNowShowing();
            return PartialView(list);
        }
        [ChildActionOnly]
        public ActionResult FilmPromotion()
        {
            HomeDao homeDao = new HomeDao();
            List<post> list = homeDao.getPromotion();
            return PartialView(list);
        }
        public ActionResult Film()
        {
            return View("IndexUser");
        }
        public ActionResult ProfileUser(string email)
        {
            UserDao userD = new UserDao();
            var model = userD.getInformation(email);
            return View(model);
        }
    }
}