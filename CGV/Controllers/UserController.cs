using DatabaseIO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

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
            var userSession = Session[Constants.Constants.USER_SESSION];
            if (userSession != null)
            {
                if (String.IsNullOrEmpty(passwordOld) || String.IsNullOrEmpty(passwordNew) || String.IsNullOrEmpty(rePasswordNew))
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
            }
            else
            {
                js.Data = new
                {
                    status = "Error",
                    message = "Bạn đã bị đăng xuất ở nơi khác vui lòng reload lại trang"
                };
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
            var userSession = Session[Constants.Constants.USER_SESSION];
            if(userSession != null)
            {
                if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(username) || String.IsNullOrEmpty(phonenumber))
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
            }
            else
            {
                js.Data = new
                {
                    status = "Error",
                    message = "Bạn đã bị đăng xuất ở nơi khác vui lòng reload lại trang"
                };
            }
           

            return Json(js, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Forgot(string msg)
        {
            ViewBag.msg = msg;
            return View();
        }
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
       [HttpGet] 
        public ActionResult GetForgot()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PostForgot(string email)
        {
          
            bool checkemail = authenticationD.checkEmail(email);
            if (checkemail)
            {
                var formEmailAddress = ConfigurationManager.AppSettings["FormEmailAddress"].ToString();
                var formEmailDisplayName = ConfigurationManager.AppSettings["FormEmailDisplayName"].ToString();
                var formEmailPassword = ConfigurationManager.AppSettings["FormEmailPassword"].ToString();
                var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                var smtpPort = ConfigurationManager.AppSettings["SMTPPost"].ToString();

                bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
                string code = GenerateRandomNo().ToString();
                MailMessage message = new MailMessage(new MailAddress(formEmailAddress, formEmailDisplayName), new MailAddress(email));
                message.Subject = "Mã  quên mật khẩu";
                message.Body = "Mã quên mật khẩu" + " " + code;

                var client = new SmtpClient();
                client.Credentials = new NetworkCredential(formEmailAddress, formEmailPassword);
                client.Host = smtpHost;
                client.EnableSsl = enableSsl;
                client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
                client.Send(message);

                Session.Add(Constants.Constants.FOR_GOT, code);
                Session.Add(Constants.Constants.EMAIL_SESSION, email);
                Session.Timeout = 2;
                return RedirectToAction("GetForgot");
            }
            else
            {
                return RedirectToAction("Forgot", new { msg = "Email không tồn tại" });
            }
           
        }
        [HttpPost]
        public JsonResult VerifyForgot(string code)
        {
            var codeSession = (string)Session[Constants.Constants.FOR_GOT];
            if (!code.Equals(codeSession))
            {

                return Json(new { status = "ERROR", msg = "Mã xác thực không chính xác", JsonRequestBehavior.AllowGet });
            }
            else if (codeSession == null || codeSession == "")
            {
                return Json(new { status = "ERROR", msg = "Mã xác thực đã quá hạn vui lòng lấy lại mã", JsonRequestBehavior.AllowGet });
            }
            else
            {
              
                return Json(new { status = "OK", msg = "Xác thực thành công !!", JsonRequestBehavior.AllowGet });
            }
        }
        [HttpGet]
        public ActionResult NewPassword()
        {
            var email = (string)Session[Constants.Constants.EMAIL_SESSION];
            if(email != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
          
        }
        [HttpPost]
        public JsonResult PostNewPassword(string password)
        {
            var email = (string)Session[Constants.Constants.EMAIL_SESSION];
            if (email != null)
            {
                authenticationD.forgotPassword(email,password);
                Session.Remove(Constants.Constants.EMAIL_SESSION);
                return Json(new { status = "OK", msg = "Đổi mật khẩu thành công!!", JsonRequestBehavior.AllowGet });
            }
            else
            {
                return Json(new { status = "ERROR", msg = "Đã hết hạn", JsonRequestBehavior.AllowGet });
            }
         
            
        }
    }
}