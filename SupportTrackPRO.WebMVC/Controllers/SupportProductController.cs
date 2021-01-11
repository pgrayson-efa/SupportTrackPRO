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
    public class SupportProductController : Controller
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

        // GET: SupportProduct
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SupportProductService(userId, ReturnMySupportCompanyId());
            var model = service.GetSupportProducts(User.Identity.Name, ReturnMySupportCompanyId());

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
        public ActionResult Create(SupportProductCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());
            int mySupportCompanyId = ReturnMySupportCompanyId();
            var service = new SupportProductService(userId, mySupportCompanyId);

            service.CreateSupportProduct(model,mySupportCompanyId);

            return RedirectToAction("Index");
        }
    }


}