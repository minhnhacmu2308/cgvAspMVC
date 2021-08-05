using DatabaseIO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CGV.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        MyDB db = new MyDB();
        UserDao userD = new UserDao();
        GenericDao genericD = new GenericDao();
        AuthenticationDao authenticationD = new AuthenticationDao();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EditPassword(string passwordOld, string passwordNew, string rePasswordNew,string email)
        {
            JsonResult js = new JsonResult();
            Console.WriteLine(passwordOld);
            if(String.IsNullOrEmpty(passwordOld) || String.IsNullOrEmpty(passwordNew) || String.IsNullOrEmpty(rePasswordNew))
            {
                js.Data = new
                {
                    status = "Error",
                    message = "Cần điền đầy đủ thông tin "
                };
            }else if (passwordNew!= rePasswordNew)
            {
                js.Data = new
                {
                    status = "Error",
                    message = "Hai mật khẩu không trùng khớp"
                };
            }
            else
            {
                string passwordMd5 = authenticationD.md5(passwordOld);
                string passwordMd5New = authenticationD.md5(passwordNew);
                var user = userD.getUpdateProfile(email, passwordMd5);
                if(user != null)
                {
                    userD.updatePassword(email, passwordMd5, passwordMd5New);
                    js.Data = new
                    {
                        status = "OK",
                        message = "Cập nhật mật khẩu thành công",
                        
                    };
                }
            }
          
            return Json(js,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult updateProfile(string email, string username,string phonenumber)
        {
            JsonResult js = new JsonResult();
            usercgv user = new usercgv();
            user.phonenumber = phonenumber;
            user.username = username;
            if(String.IsNullOrEmpty(email) || String.IsNullOrEmpty(username) || String.IsNullOrEmpty(phonenumber))
            {
                js.Data = new
                {
                    status = "Error",
                    message = "Cần điền đầy đủ thông tin"
                };
            }
            else
            {
                userD.updateProfile(email, user);
                js.Data = new
                {
                    status = "OK",
                    message = "Cập nhật thông tin thành công",
                };
            }

            return Json(js, JsonRequestBehavior.AllowGet);
        }
    }
}