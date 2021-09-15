﻿using System.Web.Mvc;
using DatabaseIO;
using Model;

namespace CGV.Controllers
{
    /**
     * PostController
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
            if (list != null) {
                return View(list);
            } else {
                ModelState.AddModelError(Constants.Constants.ERROR_SYSTEM, Constants.Constants.ERROR_SYTEM_DETAIL);
            }
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
            if (post != null) {
                return View(post);
            } else {
                ModelState.AddModelError(Constants.Constants.ERROR_SYSTEM, Constants.Constants.ERROR_SYTEM_DETAIL);
            }
            return View(post);
        }
    }
}