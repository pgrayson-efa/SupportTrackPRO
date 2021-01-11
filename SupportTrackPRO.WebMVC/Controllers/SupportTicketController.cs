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
    public class SupportTicketController : Controller
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

        // GET: Support Tickets
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
            var service = new SupportTicketService(userId, ReturnMySupportCompanyId());
            var model = service.GetSupportTickets(myCustomerId);

            return View(model);
        }

        //public ActionResult Index(int id)
        //{
        //    UpdateCustomerDb(ReturnApplicationUserId());

        //    int mySupportCompanyId = ReturnMySupportCompanyId();
        //    string mySupportCompanyName = ReturnMySupportCompanyName();
        //    int myCustomerId = ReturnMyCustomerId();

        //    ViewBag.mySupportCompanyId = mySupportCompanyId;
        //    ViewBag.mySupportCompanyName = mySupportCompanyName;
        //    ViewBag.myCustomerId = myCustomerId;

        //    var userId = Guid.Parse(User.Identity.GetUserId());
        //    var service = new SupportTicketService(userId, ReturnMySupportCompanyId());
        //    var model = service.GetSupportTicketBySupportTicketId(id);

        //    return View(model);
        //}

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
        public ActionResult Create(SupportTicketCreate model)
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
            var service = new SupportTicketService(userId, myCustomerId);

            service.CreateSupportTicket(model, myCustomerId);

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
            var svc = new SupportTicketService(userId, ReturnMySupportCompanyId());

            var model = svc.GetSupportTicketBySupportTicketId(id);

            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new SupportTicketService(userId, ReturnMySupportCompanyId());
            var model = svc.GetSupportTicketBySupportTicketId(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SupportTicketService(userId, ReturnMySupportCompanyId());

            service.DeleteSupportTicket(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
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
            var service = new SupportTicketService(userId, mySupportCompanyId);

            var detail = service.GetSupportTicketDetailBySupportTicketId(id);
            var model = new SupportTicketEdit
            {
                SupportTicketId = detail.SupportTicketId,
                Status = detail.Status,
                SupportProviderId = detail.SupportProviderId,
                RegisteredWarrantyId = detail.RegisteredWarrantyId,
                CustomerId = detail.CustomerId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SupportTicketEdit model)
        {
            UpdateCustomerDb(ReturnApplicationUserId());
            int mySupportCompanyId = ReturnMySupportCompanyId();
            string mySupportCompanyName = ReturnMySupportCompanyName();
            int myCustomerId = ReturnMyCustomerId();

            ViewBag.mySupportCompanyId = mySupportCompanyId;
            ViewBag.mySupportCompanyName = mySupportCompanyName;
            ViewBag.myCustomerId = myCustomerId;

            if (!ModelState.IsValid) return View(model);

            if (model.SupportTicketId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SupportTicketService(userId, ReturnMySupportCompanyId());

            if (service.UpdateSupportTicket(model, userId))
            {
                TempData["SaveResult"] = "Updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Not updated.");
            return View(model);
        }
    }


}