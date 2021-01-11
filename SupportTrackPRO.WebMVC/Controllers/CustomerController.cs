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
    [Authorize]
    public class CustomerController : Controller
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

        // GET: Customer
        public ActionResult Index()
        {
            UpdateCustomerDb(ReturnApplicationUserId());

            int mySupportCompanyId = ReturnMySupportCompanyId();
            string mySupportCompanyName = ReturnMySupportCompanyName();
            int myCustomerId = ReturnMyCustomerId();

            ViewBag.mySupportCompanyId = mySupportCompanyId;
            ViewBag.mySupportCompanyName = mySupportCompanyName;
            ViewBag.myCustomerId = myCustomerId;

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CustomerService(userId, ReturnMySupportCompanyId());
            var model = service.GetCustomers();

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

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerCreate model)
        {
            UpdateCustomerDb(ReturnApplicationUserId());
            int mySupportCompanyId = ReturnMySupportCompanyId();
            string mySupportCompanyName = ReturnMySupportCompanyName();
            int myCustomerId = ReturnMyCustomerId();

            ViewBag.mySupportCompanyId = mySupportCompanyId;
            ViewBag.mySupportCompanyName = mySupportCompanyName;
            ViewBag.myCustomerId = myCustomerId;

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CustomerService(userId, mySupportCompanyId);

            service.CreateCustomer(model,mySupportCompanyId);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            UpdateCustomerDb(ReturnApplicationUserId());
            int mySupportCompanyId = ReturnMySupportCompanyId();
            string mySupportCompanyName = ReturnMySupportCompanyName();
            int myCustomerId = ReturnMyCustomerId();

            ViewBag.mySupportCompanyId = mySupportCompanyId;
            ViewBag.mySupportCompanyName = mySupportCompanyName;
            ViewBag.myCustomerId = myCustomerId;

            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new CustomerService(userId, ReturnMySupportCompanyId());

            var model = svc.GetCustomerByCustomerId(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            UpdateCustomerDb(ReturnApplicationUserId());
            int mySupportCompanyId = ReturnMySupportCompanyId();
            string mySupportCompanyName = ReturnMySupportCompanyName();
            int myCustomerId = ReturnMyCustomerId();

            ViewBag.mySupportCompanyId = mySupportCompanyId;
            ViewBag.mySupportCompanyName = mySupportCompanyName;
            ViewBag.myCustomerId = myCustomerId;

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CustomerService(userId, mySupportCompanyId);

            var detail = service.GetCustomerDetailByCustomerId(id);
            var model = new CustomerEdit
            {
                CustomerId = detail.CustomerId,
                ApplicationUserId = detail.ApplicationUserId,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                Address1 = detail.Address1,
                Address2 = detail.Address2,
                City = detail.City,
                State = detail.State,
                ZipCode = detail.ZipCode,
                PhoneNumber1 = detail.PhoneNumber1,
                PhoneNumber2 = detail.PhoneNumber2
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerEdit model)
        {
            UpdateCustomerDb(ReturnApplicationUserId());
            int mySupportCompanyId = ReturnMySupportCompanyId();
            string mySupportCompanyName = ReturnMySupportCompanyName();
            int myCustomerId = ReturnMyCustomerId();

            ViewBag.mySupportCompanyId = mySupportCompanyId;
            ViewBag.mySupportCompanyName = mySupportCompanyName;
            ViewBag.myCustomerId = myCustomerId;

            if (!ModelState.IsValid) return View(model);

            if (model.CustomerId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CustomerService(userId, ReturnMySupportCompanyId());

            if (service.UpdateCustomer(model, userId))
            {
                TempData["SaveResult"] = "Updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Not updated.");
            return View(model);
        }
    }


}