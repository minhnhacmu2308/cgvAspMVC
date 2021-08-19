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
        UserDao user = new UserDao();
        AuthenticationDao authen = new AuthenticationDao();
        public MyDB db = new MyDB();
        // GET: AdminUser
        public ActionResult Index(string mess)
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Index", "AdminAuthen");
            }
            ViewBag.Msg = mess;
            List<usercgv> list = db.usercgvs.OrderBy(u => u.role_id).ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var email = form["email"];
            var username = form["username"];
            var password = form["password"];
            var roleid = form["roleid"];
            var phonenumber = form["phonenumber"];
            var passworodMd5 = authen.md5(password);
            bool result = authen.checkEmail(email);
            if (result)
            {
                var message = "Email đã tồn tại";
                return RedirectToAction("Index", new { mess = message });
            }
            else
            {
                
                user.Add(email,passworodMd5,phonenumber,roleid,username);
                var message = "Thêm thành công";
                return RedirectToAction("Index", new { mess = message });
            }
           
        }
        public ActionResult Update(FormCollection form)
        {
            var email = form["email"];
            var username = form["username"];
            var password = form["password"];
            var roleid = form["roleid"];
            var phonenumber = form["phonenumber"];
            var id = form["id"];
            var idu = Int32.Parse(id);
            var passworodMd5 = authen.md5(password);
            usercgv u = db.usercgvs.Where(p => p.id == idu).FirstOrDefault();
            if (password == u.password)
            {
                user.Update(email, password, phonenumber, roleid, username, id);
                var message = "Cập nhập thành công";
                return RedirectToAction("Index", new { mess = message });
            }
            else
            {
                user.Update(email, passworodMd5, phonenumber, roleid, username, id);
                var message = "Cập nhập thành công";
                return RedirectToAction("Index", new { mess = message });
            }
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {

           
            user.Delete(id);
            return Json(new { status = "SUCCESS", msg = "THÀNH CÔNG", JsonRequestBehavior.AllowGet });

        }
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            user.ChangStatus(id);
            return Json(new { status = "SUCCESS", msg = "THÀNH CÔNG", JsonRequestBehavior.AllowGet });
        }
    }
}