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
        public ActionResult Index(string mess)
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Index", "AdminAuthen");
            }
            ViewBag.Msg = mess;
            List<category_post> list = db.category_post.ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var name = form["categorypost"];
            bool result = cpost.checkName(name);
            if (result)
            {
                var message = "Loại bài viết đã tồn tại";
                return RedirectToAction("Index", new { mess = message });
            }
            else
            {
                cpost.Add(name);
                var message = "Thêm thành công";
                return RedirectToAction("Index", new { mess = message });
            }
            
        }
        public ActionResult Update(FormCollection form)
        {
            var name = form["categorypost"];
            var id = form["id"];
            cpost.Update(name, id);
            var message = "Cập nhập thành công";
            return RedirectToAction("Index", new { mess = message });
        }
        public ActionResult Delete(FormCollection form)
        {

            var id = form["id"];
            cpost.Delete(id);
            var message = "Xóa thành công";
            return RedirectToAction("Index", new { mess = message });

        }
    }
}