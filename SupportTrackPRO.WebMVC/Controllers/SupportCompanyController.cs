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
    public class SupportCompanyController : Controller
    {
        public bool VerifyMyUserRole(string myUserRole)
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            bool isInRole = User.IsInRole(myUserRole);
            return isInRole;
        }

        public int ReturnMySupportCompanyId()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
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

        public int ReturnMyCustomerId()
        {
            string myUserName = User.Identity.Name;

            var tempctx = new ApplicationDbContext();
            int myCustomerId = tempctx.Customers.Where(c => c.ApplicationUserId == Guid.Parse(User.Identity.GetUserId())).Select(c => c.CustomerId).FirstOrDefault();

            return myCustomerId;
        }

        // GET: SupportCompany
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SupportCompanyService(userId,ReturnMySupportCompanyId());
            var model = service.GetSupportCompanies();

            return View(model);
        }

        public ActionResult Create()
        {
            int mySupportCompanyId = ReturnMySupportCompanyId();
            string mySupportCompanyName = ReturnMySupportCompanyName();

            ViewBag.mySupportCompanyId = mySupportCompanyId;
            ViewBag.mySupportCompanyName = mySupportCompanyName;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupportCompanyCreate model)
        {


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());
            int mySupportCompanyId = ReturnMySupportCompanyId();
            var service = new SupportCompanyService(userId, mySupportCompanyId);

            service.CreateSupportCompany(model);

            return RedirectToAction("Index");
        }

    }

}