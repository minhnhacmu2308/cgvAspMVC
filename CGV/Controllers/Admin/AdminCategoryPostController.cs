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
        CategoryPostDao cpost = new CategoryPostDao();
        public MyDB db = new MyDB();
        // GET: AdminCategoryPost
        public ActionResult Index()
        {
            List<category_post> list = db.category_post.ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var name = form["categorypost"];
            cpost.Add(name);
            return RedirectToAction("Index");
        }
        public ActionResult Update(FormCollection form)
        {
            var name = form["categorypost"];
            var id = form["id"];
            cpost.Update(name, id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(FormCollection form)
        {

            var id = form["id"];
            cpost.Delete(id);
            return RedirectToAction("Index");

        }
    }
}