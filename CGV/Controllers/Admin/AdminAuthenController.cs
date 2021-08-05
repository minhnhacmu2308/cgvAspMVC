using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using Model;
using System.Web.Security;
namespace CGV.Controllers.Admin
{
    public class AdminAuthenController : Controller
    {
        MyDB mydb = new MyDB();
        AuthenticationDao authenticationD = new AuthenticationDao();
        // GET: AdminAuthen
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult Login(FormCollection form)
        {
            var email = form["email"];
            var password = form["password"];
            var passworodMd5 = authenticationD.md5(password);
            usercgv cs = mydb.usercgvs.SingleOrDefault(u => u.email == email && u.password == passworodMd5 && u.role_id != 3);
            if (cs != null)
            {

                Session["usr"] = cs;
                return RedirectToAction("Index", "AdminHome");
            }
            
            return RedirectToAction("Index");
        }
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index");

        }
    }
}