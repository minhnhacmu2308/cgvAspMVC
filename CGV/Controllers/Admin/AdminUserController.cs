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
        public ActionResult Index()
        {
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
                ViewBag.loznha = "Email đã tôn tại ";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.loznha = "Ok đã tôn tại ";
                user.Add(email,passworodMd5,phonenumber,roleid,username);
                return RedirectToAction("Index");
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
            var passworodMd5 = authen.md5(password);
            user.Update(email,passworodMd5,phonenumber,roleid,username,id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(FormCollection form)
        {

            var id = form["id"];
            user.Delete(id);
            return RedirectToAction("Index");

        }
    }
}