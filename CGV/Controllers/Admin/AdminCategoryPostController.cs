using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using Model;
namespace CGV.Controllers.Admin
{
    public class AdminCategoryPostController : Controller
    {
        public MyDB db = new MyDB();
        // GET: AdminCategoryPost
        public ActionResult Index()
        {
            List<category_post> list = db.category_post.ToList();
            return View(list);
        }
    }
}