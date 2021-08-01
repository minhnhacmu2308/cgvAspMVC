using DatabaseIO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CGV.Controllers
{
    public class AuthenticationController : Controller
    {
        GenericDao genericD = new GenericDao();
        AuthenticationDao authenticationD = new AuthenticationDao();
        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            var email = form["email"];
            var username = form["username"];
            var password = form["password"];
            var phonenumber = form["phonenumber"];
            var rePassword = form["rePassword"];
            var passworodMd5 = authenticationD.md5(password);
            if(string.IsNullOrEmpty(email)|| string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phonenumber) || string.IsNullOrEmpty(rePassword))
            {
                ViewBag.message = "Cần điền đầy đu thông tin";
                return View();
            }
            else if(!password.Equals(rePassword))
            {
                ViewBag.message = "Hai mật khẩu không trùng khớp ";
                return View();
            }
            else
            {
                bool result = authenticationD.checkEmail(email);
                if (result)
                {
                    ViewBag.message = "Email đã tôn tại ";
                    return View();
                }
                else
                {
                    usercgv user = new usercgv();
                    user.id = 101;
                    user.email = email;
                    user.password = passworodMd5;
                    user.phonenumber = phonenumber;
                    user.username = username;
                    user.is_active = 0;
                    user.role_id = 3;
                    genericD.AddObject<usercgv>(user);
                    genericD.Save();
                    return RedirectToAction("Login");
                }
               
            }
             
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}