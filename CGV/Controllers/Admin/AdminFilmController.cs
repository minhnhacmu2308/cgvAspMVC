using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using Model;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using System.Net;
namespace CGV.Controllers.Admin
{
    public class AdminFilmController : Controller
    {
        FilmDao post = new FilmDao();
        public MyDB db = new MyDB();
        // GET: AdminFilm
        public ActionResult Index(string mess)
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Index", "AdminAuthen");
            }
            ViewBag.Msg = mess;
            List<film> list = db.films.OrderByDescending(f => f.id).ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var tenphim = form["tenphim"];
            var dienvien = form["dienvien"];
            var daodien = form["daodien"];
            var thoiluong = form["thoiluong"];
            var theloai = form["theloai"];
            var trailer = form["trailer"];
            var file = Request.Files["file"];
            var noidung = form["noidung"];
            Random random = new Random();
            int num = random.Next();
            bool result = post.checkName(tenphim);
            if (result)
            {
                var message = "Phim đã tồn tại";
                return RedirectToAction("Index", new { mess = message });
            }
            else
            {
                String filename = "film" + num + file.FileName.Substring(file.FileName.LastIndexOf("."));
                String Strpath = Path.Combine(Server.MapPath("~/Content/Assets/images/"), filename);
                file.SaveAs(Strpath);
                post.Add(noidung, daodien, dienvien, thoiluong, tenphim, filename, trailer, theloai);
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Admin/mail/mailbody.html"));
                content = content.Replace("{{title}}", tenphim);
                content = content.Replace("{{noidung}}", noidung);
                List<usercgv> listuser = db.usercgvs.Where(p => p.role_id == 3).ToList();
                foreach (var item in listuser)
                {
                    var formEmailAddress = ConfigurationManager.AppSettings["FormEmailAddress"].ToString();
                    var formEmailDisplayName = ConfigurationManager.AppSettings["FormEmailDisplayName"].ToString();
                    var formEmailPassword = ConfigurationManager.AppSettings["FormEmailPassword"].ToString();
                    var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                    var smtpPort = ConfigurationManager.AppSettings["SMTPPost"].ToString();

                    bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
                    MailMessage message = new MailMessage(new MailAddress(formEmailAddress, formEmailDisplayName), new MailAddress(item.email));

                    message.Subject = "Phim sắp chiếu - CGV Lê Độ";
                    message.IsBodyHtml = true;
                    message.Body = content;

                    var client = new SmtpClient();
                    client.Credentials = new NetworkCredential(formEmailAddress, formEmailPassword);
                    client.Host = smtpHost;
                    client.EnableSsl = enableSsl;
                    client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
                    client.Send(message);
                }
                var messag = "Thêm thành công";
                return RedirectToAction("Index", new { mess = messag });
            }
        }
        public ActionResult Update(FormCollection form)
        {
            var id = form["id"];
            var tenphim = form["tenphim"];
            var dienvien = form["dienvien"];
            var daodien = form["daodien"];
            var thoiluong = form["thoiluong"];
            var theloai = form["theloai"];
            var trailer = form["trailer"];
            var file = Request.Files["file"];
            var noidung = form["noidung"];
            var img = form["anh"];
            Random random = new Random();
            int num = random.Next();
            if (file != null && file.ContentLength > 0)
            {
                String filename = "film" + num + file.FileName.Substring(file.FileName.LastIndexOf("."));
                String Strpath = Path.Combine(Server.MapPath("~/Content/Assets/images/"), filename);
                file.SaveAs(Strpath);
                post.Update(noidung, daodien, dienvien, thoiluong, tenphim, filename, trailer, theloai,id);
                return RedirectToAction("Index");
            }

            post.Update(noidung, daodien, dienvien, thoiluong, tenphim, img, trailer, theloai, id);
            var messag = "Cập nhập thành công";
            return RedirectToAction("Index", new { mess = messag });
        }
        public ActionResult Delete(FormCollection form)
        {

            var id = form["id"];
            post.Delete(id);
            var message = "Xóa thành công";
            return RedirectToAction("Index", new { mess = message });

        }
    }
}