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
        UserDao userD = new UserDao();
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
                 
                    user.email = email;
                    user.password = passworodMd5;
                    user.phonenumber = phonenumber;
                    user.username = username;
                    user.is_active = 0;
                    user.role_id = 3;
                    authenticationD.register(user);
                    return RedirectToAction("Login");
                }
               
            }
             
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var email = form["email"];
            var password = form["password"];
            var passworodMd5 = authenticationD.md5(password);
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.message = "Cần điền đầy đủ thông tin";
                return View();
            }
            else
            {

                bool result = authenticationD.checklogin(email, passworodMd5);
                if(result)
                {
                    var userInformation = userD.getInformation(email);
                    Session.Add(Constants.Constants.USER_SESSION, userInformation);
                    return RedirectToAction("IndexUser", "Home");
                }
                else
                {
                    ViewBag.message = "Tài khoản hoặc mật khẩu không chính xác";
                    return View();
                }
            }
            
        }
        public ActionResult Logout()
        {
            Session.Remove(Constants.Constants.USER_SESSION);
            return Redirect("/");
        }
    }
}