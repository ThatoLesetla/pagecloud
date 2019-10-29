using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WinForms;
using PageCloud.Models;

namespace PageCloud.Controllers
{
    public class GenerateReportsController : Controller
    {
        PageCloudEntities db = new PageCloudEntities();

        // GET: GenerateReports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserDetailReport()
        {
            List<User> users = db.Users.ToList();

            return View(users);
        }

        [HttpPost]
        public ActionResult UserDetailReport(FormCollection form)
        {
            DateTime to_date = Convert.ToDateTime(form["to_Date"].ToString());
            DateTime From_Date = Convert.ToDateTime(form["From_Date"]);

            var users = db.Users.Where(a => a.registerDate >= From_Date && a.registerDate <= to_date).ToList();

            return View(users);
        }


        public ActionResult SummaryReport()
        {
            UserStatistics userStats = new UserStatistics();
            ProfitSummary profit = new ProfitSummary();

            userStats.calculateUserStats();
            profit.CalculateGrossIncome();

            SummaryReport summary = new SummaryReport()
            {
                userStatistics = userStats,
                profit = profit
            };
            return View(summary);
        }

        public PartialViewResult AllInvoice()
        {
            List<Invoice> invoices = db.Invoices.ToList();

            return PartialView("_Invoice", invoices);
        }

        public ActionResult DetailReport(string ReportType, List<User> users)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/UserReport.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "UserDataSet";
            reportDataSource.Value = db.Users.Where(a => a.userRole == UserRoles.user).ToList();
            localReport.DataSources.Add(reportDataSource);

            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;

            switch(reportType)
            {
                case "Word":
                    fileNameExtension = "docx";
                    break;

                case "PDF":
                    fileNameExtension = "pdf";
                    break;

                case "Excel":
                    fileNameExtension = "xlsx";
                    break;

                default:
                    fileNameExtension = "jpg";
                    break;
            }

            string[] streams;
            Warning[] warnings;
            byte[] reneredByte;



            reneredByte = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            Response.AddHeader("content-disposition", "attachment;filename=userReport." + fileNameExtension);

            return File(reneredByte, fileNameExtension);
        }
    }
}