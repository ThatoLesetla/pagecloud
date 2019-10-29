using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PageCloud.Models;

namespace PageCloud.Controllers
{
    public class DomainsController : Controller
    {
        private PageCloudEntities db = new PageCloudEntities();

        // GET: Domains
        public ActionResult Index()
        {
            int userID = Convert.ToInt32(Session["UserID"].ToString());

            //var domains = db.Domains.Include(d => d.User).ToListAsync();

            List<Domain> domains = new List<Domain>();

            if (Session["UserRole"].ToString() == "user")
            {
                domains = db.Domains.Where(a => a.userID == userID).ToList();
            } else
            {
               domains = db.Domains.ToList();
            }
            

            return View(domains);
        }

        // GET: Domains/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Domain domain = await db.Domains.FindAsync(id);
            if (domain == null)
            {
                return HttpNotFound();
            }
            return View(domain);
        }

        // GET: Domains/Create
        public ActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "domainName,userID,registrationDate,paymentMethod,websiteStatus")] Domain domain)
        {
            int user_ID = Convert.ToInt32(Session["UserID"].ToString());

            if (ModelState.IsValid)
            {
                domain.registrationDate = DateTime.Now;
                domain.websiteStatus = "inactive";
                domain.userID = user_ID;

                db.Domains.Add(domain);
                await db.SaveChangesAsync();

                Invoice invoice = new Invoice
                {
                    invoiceDate = DateTime.Now,
                    dueDate = DateTime.Now.AddDays(15),
                    total = 95,
                    invoiceStatus = false,
                    domainID = domain.domainID
                };

                db.Invoices.Add(invoice);

                Notification notification = new Notification
                {
                    notificationBody = "You have one unpaid invoice. Pay now to get a peace of mind",
                    notificationDate = DateTime.Now,
                    userId = user_ID
                };

                db.Notifications.Add(notification);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(domain);
        }

        // GET: Domains/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Domain domain = await db.Domains.FindAsync(id);
            if (domain == null)
            {
                return HttpNotFound();
            }

            ViewBag.userID = new SelectList(db.Users, "userID", "userName", domain.userID);
            return View(domain);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "domainName,userID,registrationDate,paymentMethod,websiteStatus, domainID")] Domain domain)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // domain.domainID = 8;
                    db.Entry(domain).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
            } catch(Exception Ex)
            {
                ViewBag.ErrorMessage = Ex.Message;
                return View("Error");
            }

            ViewBag.userID = new SelectList(db.Users, "userID", "userName", domain.userID);
            return View(domain);
        }

        // GET: Domains/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Domain domain = await db.Domains.FindAsync(id);
            if (domain == null)
            {
                return HttpNotFound();
            }
            return View(domain);
        }

        // POST: Domains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            Domain domain = await db.Domains.FindAsync(id);
            db.Domains.Remove(domain);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult ValidateDomain()
        {
            string hostAddress = Dns.GetHostAddresses("www.google.com").ToString();
            return Content("The site ip is: " + hostAddress);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
