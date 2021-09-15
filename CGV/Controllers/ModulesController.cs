using System.Web.Mvc;

namespace CGV.Controllers
{
    /**
     * ModulesController
     * 
     * Version 1.0
     * 
     * Date 07-08-2021
     * 
     * Copyright
     * 
     * Modification Logs:
     * DATE            AUTHOR            DESCRIPTION
     * ----------------------------------------------
     * 07-08-2021      NhaNM2              Create
     */
    public class ModulesController : Controller
    {
        // GET: Modules
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }
    }
}