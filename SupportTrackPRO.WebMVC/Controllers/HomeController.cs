using Microsoft.AspNet.Identity;
using SupportTrackPRO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportTrackPRO.WebMVC.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public Guid ReturnApplicationUserId()
        {
            return Guid.Parse(User.Identity.GetUserId());
        }

        public bool VerifyCustomerExists(Guid applicationUserId)
        {
            var existingCustomer = new Customer { ApplicationUserId = applicationUserId };

            using (var tempCustomerDbContext = new ApplicationDbContext())
            {
                var query = tempCustomerDbContext.Customers.Where(c => c.ApplicationUserId == applicationUserId).Select(c => c.ApplicationUserId).FirstOrDefault();
                if (query == applicationUserId)
                {
                    return true;
                }
                return false;
            }
        }
        
        //
        // Returns bool depending on if the customer record was added or not
        // based on the current users ApplicationUserId.
        //
        public bool UpdateCustomerDb(Guid applicationUserId)
        {
            var newCustomer = new Customer { ApplicationUserId = applicationUserId };
            if (VerifyCustomerExists(applicationUserId) == false)
            {
                using (var tempCustomerDbContext = new ApplicationDbContext())
                {
                    tempCustomerDbContext.Customers.Add(newCustomer);
                    int wasAdded = tempCustomerDbContext.SaveChanges();
                    if (wasAdded == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public ActionResult Index()
        {

            return View();
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

        public ActionResult Support()
        {

            ViewBag.Message = "Your support page.";

            return View();
        }

        public ActionResult Manager()
        {

            ViewBag.Message = "Your support page.";

            return View();
        }

        public ActionResult Admin()
        {

            ViewBag.Message = "Your support page.";

            return View();
        }

        public ActionResult Provider()
        {

            ViewBag.Message = "Your support page.";

            return View();
        }
    }
}