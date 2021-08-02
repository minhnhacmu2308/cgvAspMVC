using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using Model;
namespace CGV.Controllers.Admin
{
    public class AdminUserController : Controller
    {
        public MyDB db = new MyDB();
        // GET: AdminUser
        public ActionResult Index()
        {
            List<usercgv> list = db.usercgvs.ToList();
            return View(list);
        }
    }
}