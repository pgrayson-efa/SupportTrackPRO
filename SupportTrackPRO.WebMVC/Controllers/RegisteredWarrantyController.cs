using Microsoft.AspNet.Identity;
using SupportTrackPRO.Data;
using SupportTrackPRO.Models;
using SupportTrackPRO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportTrackPRO.WebMVC.Controllers
{
    public class RegisteredWarrantyController : Controller
    {
        public bool VerifyMyUserRole(string myUserRole)
        {
            bool isInRole = User.IsInRole(myUserRole);
            return isInRole;
        }

        public int ReturnMySupportCompanyId()
        {
            string myUserName = User.Identity.Name;

            var tempctx = new ApplicationDbContext();
            int mySupportCompanyId = tempctx.SupportProviders.Where(c => c.UserName == myUserName).Select(c => c.SupportCompanyId).FirstOrDefault();

            return mySupportCompanyId;
        }

        public string ReturnMySupportCompanyName()
        {
            string myUserName = User.Identity.Name;

            var tempctx = new ApplicationDbContext();
            int mySupportCompanyId = tempctx.SupportProviders.Where(c => c.UserName == myUserName).Select(c => c.SupportCompanyId).FirstOrDefault();
            string mySupportCompanyName = tempctx.SupportCompanies.Where(c => c.SupportCompanyId == mySupportCompanyId).Select(c => c.CompanyName).FirstOrDefault();

            return mySupportCompanyName;
        }

        public Guid ReturnApplicationUserId()
        {
            return Guid.Parse(User.Identity.GetUserId());
        }

        public int ReturnMyCustomerId()
        {
            var tempctx = new ApplicationDbContext();
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            int myCustomerId = tempctx.Customers.Where(c => c.ApplicationUserId == userId).Select(c => c.CustomerId).FirstOrDefault();

            return myCustomerId;
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

        // GET: RegisteredWarranty
        public ActionResult Index()
        {
            UpdateCustomerDb(ReturnApplicationUserId());

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RegisteredWarrantyService(userId, ReturnMySupportCompanyId());
            var model = service.GetRegisteredWarranties(ReturnMyCustomerId(), ReturnMySupportCompanyId());

            return View(model);
        }

        public ActionResult Create()
        {
            UpdateCustomerDb(ReturnApplicationUserId());

            int mySupportCompanyId = ReturnMySupportCompanyId();
            string mySupportCompanyName = ReturnMySupportCompanyName();
            int myCustomerId = ReturnMyCustomerId();

            ViewBag.mySupportCompanyId = mySupportCompanyId;
            ViewBag.mySupportCompanyName = mySupportCompanyName;
            ViewBag.myCustomerId = myCustomerId;

            var tempctx = new ApplicationDbContext();
            ViewBag.allSupportCompanies = new SelectList(tempctx.SupportCompanies.Select(c => new { SupportCompanyId = c.SupportCompanyId, CompanyName = c.CompanyName }), "SupportCompanyId", "CompanyName", 3);
            ViewBag.allSupportProducts = new SelectList(tempctx.SupportProducts.Select(c => new { SupportProductId = c.SupportProductId, ProductName = c.ProductName }), "SupportProductId", "ProductName", 3);

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisteredWarrantyCreate model)
        {
            UpdateCustomerDb(ReturnApplicationUserId());

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            int mySupportCompanyId = ReturnMySupportCompanyId();
            string mySupportCompanyName = ReturnMySupportCompanyName();
            int myCustomerId = ReturnMyCustomerId();
            Guid userId = Guid.Parse(User.Identity.GetUserId());

            var tempctx = new ApplicationDbContext();
            ViewBag.allSupportCompanies = new SelectList(tempctx.SupportCompanies.Select(c => new { SupportCompanyId = c.SupportCompanyId, CompanyName = c.CompanyName }), "SupportCompanyId", "CompanyName", 3);

            var service = new RegisteredWarrantyService(userId, mySupportCompanyId);

            service.CreateRegisteredWarranty(model, mySupportCompanyId, myCustomerId);

            return RedirectToAction("Index");
        }
    }
}