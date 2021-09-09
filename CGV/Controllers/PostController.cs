using System.Web.Mvc;
using DatabaseIO;
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

        /**
         * get list introduce for user
         * @return
         */
        public ActionResult Promotion()
        {
            var list = postD.getListIntroduce();
            return View(list);
        }

        /**
         * get detail introduce by id for user
         * @param id
         * @return
         */
        public ActionResult DetailPromotion(string id)
        {
            post post = postD.getDetailPromotion(id);
            return View(post);
        }
    }
}