using Microsoft.AspNet.Identity;
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
        // GET: SupportCompany
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SupportCompanyService(userId);
            var model = service.GetSupportCompanies();

            return View(model);
        }

        public ActionResult Create()
        {
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
            var service = new SupportCompanyService(userId);

            service.CreateSupportCompany(model);

            return RedirectToAction("Index");
        }

    }

}