using DatabaseIO;
using System.Web.Mvc;
using Model;

namespace CGV.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        PostDao postD = new PostDao();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Promotion()
        {
            var list = postD.getListIntroduce();
            return View(list);
        }
        public ActionResult DetailPromotion(string id)
        {
            post post = postD.getDetailPromotion(id);
            return View(post);
        }
    }
}