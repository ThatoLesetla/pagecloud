using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PageCloud.Models;

namespace PageCloud.Controllers
{
    public class BuildSiteController : Controller
    {
        PageCloudEntities db = new PageCloudEntities();

        // GET: BuildSite
        public ActionResult Index(int? id)
        {
            Domain domain = db.Domains.Find(id);

            if (domain == null)
            {
                return Content("The domain is null");
            }
            return View(domain);
        }
    }
}