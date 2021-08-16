using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using Model;
namespace CGV.Controllers.Admin
{
    public class AdminPostController : Controller
    {
        public MyDB db = new MyDB();
        // GET: AdminPost
        public ActionResult Index()
        {
            List<post> list = db.posts.OrderByDescending(p => p.created_at).ToList();
            return View(list);
        }
    }
}