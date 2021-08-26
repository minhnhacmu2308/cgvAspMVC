using DatabaseIO;
using Model;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CGV.Controllers
{
    public class HomeController : Controller
    {
        SeatDao seatD = new SeatDao();
        public ActionResult Index()
        {
            StripeConfiguration.ApiKey = "sk_test_51Itn76AY7zpl2tqotBGt23IEZmOSCZOmOnpgAhVQWIvua4g5c4G74Au5P54rWqNofPUw1DZ7TdHzlBhCWJCJa81W00V76C7Z2n";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
            },
                LineItems = new List<SessionLineItemOptions>
            {
                    new SessionLineItemOptions
                    {
                        Name = "Nha",
                        Description ="sdsd",
                        Amount = 900,
                        Currency ="usd",
                        Quantity =1
                    },
            },
                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel",
                PaymentIntentData = new SessionPaymentIntentDataOptions
                {
                    Metadata = new Dictionary<string, string>
                    {
                         {"Order_id","1234" },
                         {"sdsd","hello" },
                    }

                }
            };
            var service = new SessionService();
            Session session = service.Create(options);
            return View(session);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult IndexUser()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult FilmComingSoon()
        {
            HomeDao homeDao = new HomeDao();
            List<film> list = homeDao.getFilmComingSoon().Distinct().ToList();
            return PartialView(list);
        }
        [ChildActionOnly]
        public ActionResult FilmNowShowing()
        {
            HomeDao homeDao = new HomeDao();
            List<film> list = homeDao.getFilmNowShowing().Distinct().ToList();
            var listSeat = seatD.getAll();
            ViewBag.listseat = listSeat;
            return PartialView(list);
        }
        [ChildActionOnly]
        public ActionResult FilmPromotion()
        {
            HomeDao homeDao = new HomeDao();
            List<post> list = homeDao.getPromotion();
            return PartialView(list);
        }
        public ActionResult Film()
        {
            return View("IndexUser");
        }
        public ActionResult ProfileUser(string email)
        {
            var user = Session[Constants.Constants.USER_SESSION];
            if (user == null)
            {
                return RedirectToAction("IndexUser", "Home");
            }
            else
            {
                UserDao userD = new UserDao();
                var model = userD.getInformation(email);
                return View(model);
            }
           
        }
    }
}