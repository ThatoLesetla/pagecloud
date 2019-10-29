using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PageCloud.Models;
using PageCloud.View_Models;
using System.Web.Security;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;

namespace PageCloud.Controllers
{
    public class HomeController : Controller
    {
        PageCloudEntities db = new PageCloudEntities();

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["userID"].ToString());
            double totalInvoices = 0;

            var domainObj = db.Domains.Where(a => a.userID == id).ToList();
            var invoiceObj = db.Invoices.Where(a => a.Domain.User.userID == id).ToList();

            foreach(var price in invoiceObj)
            {
                totalInvoices = totalInvoices + Convert.ToDouble(price.total);
            }

            Dashboard dashboard = new Dashboard
            {
                domains = domainObj.Count,
                invoices = invoiceObj.Count,
                amountDue = totalInvoices,
                websites = db.Domains.ToList()
            };

            if(Session["userRole"].ToString() == "admin")
            {
                return View("Admin");
            }
            return View(dashboard);
        }

        public ActionResult Admin()
        {
            var domainObj = db.Domains.ToList();
            var userObj = db.Users.Where(a => a.userRole == "user").Count();
            var invoiceObj = db.Invoices.ToList();
            double totalEarnings = 0;
            double totIncome = 0;
            double unpaid_Invoices = 0;

            foreach(var item in invoiceObj)
            {
                if(item.invoiceStatus == true)
                {

                    totalEarnings = totalEarnings + Convert.ToDouble(item.total);
                } else
                {
                    unpaid_Invoices = unpaid_Invoices + Convert.ToDouble(item.total);
                }

                totIncome = totIncome - Convert.ToInt32(item.total);
            }

            Dashboard dashboard = new Dashboard
            {
                domains = domainObj.Count,
                totalIncome = totalEarnings,
                users = userObj,
                amountDue = unpaid_Invoices,
                websites = db.Domains.ToList()
            };

            return View(dashboard);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [Obsolete]
        public ActionResult Login(FormCollection loginForm)
        {
      
            string email = loginForm["Email"];
            string password = loginForm["Password"];
            var userName = db.Users.Where(model => model.userEmail == email).FirstOrDefault();
            var userPassword = db.Users.Where(model => model.userPassword == password).FirstOrDefault();
            try
            {
                if(ModelState.IsValid)
                {
                    var user = db.Users.Where(model => model.userEmail.Equals(email) && model.userPassword.Equals(password)).FirstOrDefault();
                  
                    if(user != null)
                    {
                        // Cancel the duplication of the code
                        Session["UserID"] = user.userID;
                        Session["UserEmail"] = user.userEmail;
                        Session["UserName"] = user.userName;
                        Session["UserSurname"] = user.userSurname;
                        Session["UserRole"] = user.userRole;
                        Session.Timeout = 4200;

                        if(user.userRole == "admin")
                        {
                            return RedirectToAction("Admin");
                        }
                        return RedirectToAction("Index");

                   
                    }
                }
            } catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View("Error");
            }

            if(userName == null)
            {

                ModelState.AddModelError("InvalidLogin", "Username does not exist");
            } else
            {
                ModelState.AddModelError("InvalidLogin", "Invalid password");
            }
            return View();
        }
        
        public ActionResult ComingSoon()
        {
            return View();
        }
        // Terminate Session on Logout
        public ActionResult LogOut()
        {
            Session.Abandon();

            return RedirectToAction("Login");
        }

        [Obsolete]
        public ActionResult hashPassword()
        {
            List<User> users = db.Users.ToList();

            try
            {

                foreach (var item in users)
                {
                    item.userPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(item.userPassword, "SHA1");
                    db.Entry(item).State = EntityState.Modified;
                }


            }
            catch (Exception Ex)
            {
                ViewBag.ErrorMessage = Ex.Message;
            }
            return Content("Success");
        }

        public ActionResult forgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult forgetPassword(string email)
        {

            try
            {
                if(email == null)
                {
                    ModelState.AddModelError("InvalidEmail", "Email does not exist on database records");
                    return View();
                }

                var user = db.Users.Where(a => a.userEmail == email).FirstOrDefault();

            } catch(Exception Ex)
            {
                ViewBag.ErrorMessage = Ex.Message;
                return View("Error");
            }

            return RedirectToAction("Login");
        }


        public ActionResult Gallery()
        {
            return View();
        }

    }
}