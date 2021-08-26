using DatabaseIO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net;
using System.Text;

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
            var user = Session[Constants.Constants.USER_SESSION];
            if (user != null)
            {
                return RedirectToAction("IndexUser", "Home");
            }
            else
            {
                return View();
            }
              
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
            string strongPassword = authenticationD.checkPasswordStrong(password);
            System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (string.IsNullOrEmpty(email)|| string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phonenumber) || string.IsNullOrEmpty(rePassword))
            {
                ViewBag.message = "Cần điền đầy đủ thông tin";
                return View();
            }else if (!rEmail.IsMatch(email))
            {
                ViewBag.message = "Cần nhập đúng định dạng email";
                return View();
            }
           
            else if(!password.Equals(rePassword))
            {
                ViewBag.message = "Hai mật khẩu không trùng khớp ";
                return View();
            }
            else if (strongPassword != "")
            {
                ViewBag.message = strongPassword;
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

                    sendMail(email);

                    ViewBag.email = email;
                 

                    return View("Verify");
                }
               
            }
             
        }
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        public ActionResult Login()
        {
            var user = Session[Constants.Constants.USER_SESSION];
            if(user != null)
            {
                return RedirectToAction("IndexUser","Home");
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult Verify()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Verify(string code,string email)
        {

            var codeSession = (string)Session[Constants.Constants.CODE_VERIFY];
            if (!code.Equals(codeSession))
            {
              
                return Json(new { status = "ERROR", msg = "Mã xác thực không chính xác", JsonRequestBehavior.AllowGet });
            }
            else if(codeSession == null || codeSession == "")
            {
                return Json(new { status = "ERROR", msg = "Mã xác thực đã quá hạn vui lòng lấy lại mã", JsonRequestBehavior.AllowGet });
            }
            else
            {
                userD.activeAccount(email);
                return Json(new { status = "OK", msg = "Xác thực thành công !!", JsonRequestBehavior.AllowGet });
            }

           
        }
        [HttpPost]
        public ActionResult getCodeAgain(string email)
        {
            sendMail(email);
            ViewBag.msg = "Lấy lại mã thành công vui lòng check mail ";
            ViewBag.email = email;
            return View("Verify");
        }

        [HttpGet]
        public ActionResult getCodeAgain()
        {
          
            return View("Login");
        }

        public void sendMail(string email)
        {
            var formEmailAddress = ConfigurationManager.AppSettings["FormEmailAddress"].ToString();
            var formEmailDisplayName = ConfigurationManager.AppSettings["FormEmailDisplayName"].ToString();
            var formEmailPassword = ConfigurationManager.AppSettings["FormEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPost"].ToString();

            bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
            string code = GenerateRandomNo().ToString();
            MailMessage message = new MailMessage(new MailAddress(formEmailAddress, formEmailDisplayName), new MailAddress(email));
            message.Subject = "Verify Account";
            message.Body = "This is code verify" + " " + code;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(formEmailAddress, formEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enableSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);

            Session.Add(Constants.Constants.CODE_VERIFY, code);
            Session.Timeout = 2;
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
                    bool resultActive = authenticationD.checkActive(email);
                    if (resultActive)
                    {
                        var userInformation = userD.getInformation(email);
                        Session.Add(Constants.Constants.USER_SESSION, userInformation);
                        return RedirectToAction("IndexUser", "Home");
                    }
                    else
                    {
                        ViewBag.email = email;
                        return View("Verify");
                    }
                   
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