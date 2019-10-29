using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PageCloud.Models;
using System.ComponentModel.DataAnnotations;

namespace PageCloud.Models
{
    public class ProfitSummary
    {
        PageCloudEntities db = new PageCloudEntities();
        [Display(Name = "Gross Registration Income")]
        public double grossRegistrationIncome { get; set; }

        [Display(Name = "Gross Monthly Fee Income")]
        public double grossMonthlyFeeIncome { get; set; }

        [Display(Name = "Highest Selling Month")]
        public string highestSellingMonth { get; set; }

        [Display(Name = "Gross Month Sales")]
        public double grossMonthSales { get; set; }

        [Display(Name = "Pending Payments")]
        public double pendingPayments { get; set; }

        [Display(Name = "Insolvent Payments")]
        public double insolventPayments { get; set; }

        public ProfitSummary(double grossRegIncome = 0.00, double grossMonthlyIncome = 0.00, string highestMonth = "January", double totMonthSales = 0.00, double pendingInvoices = 0.00, double insolventInvoices = 0.00)
        {
            grossRegistrationIncome = grossRegIncome;
            grossMonthlyFeeIncome = grossMonthlyFeeIncome;
            highestSellingMonth = highestMonth;
            grossMonthSales = totMonthSales;
            pendingPayments = pendingInvoices;
            insolventPayments = insolventInvoices;
        }

        public void CalculateGrossIncome()
        {
            string highestMonth = "January";
            double monthTotal = 0;

            SetRegistrationIncome();
            HighestMonth(ref highestMonth, ref monthTotal);
            SetPendingPayments();

            highestSellingMonth = highestMonth;
            grossMonthSales = monthTotal;
        }

        public void SetRegistrationIncome()
        {
            decimal registrationFee = 95;
            double totalAmount = 0;

            foreach(var item in db.Invoices.ToList())
            {
                if(item.total == registrationFee)
                {
                    totalAmount = totalAmount + Convert.ToDouble(item.total);
                }
            }

            grossRegistrationIncome = totalAmount;
        }

        public void SetPendingPayments()
        {
            double total = 0;

            foreach(var item in db.Invoices.Where(a => a.invoiceStatus == false).ToList())
            {
                total = total + Convert.ToDouble(item.total);
            }

            pendingPayments = total;
        }

        public void HighestMonth(ref string highestMonth, ref double highestSales)
        {
            double total;
            double highestTotal = 0;
            int month = 0;

            for (int i = 1; i <= 12; i++)
            {
                total = 0;

                var monthSales = db.Invoices.Where(a => a.invoiceDate.Month == i).ToList();

                for (int m = 0; m < monthSales.Count(); m++)
                {
                    total = total + Convert.ToDouble(monthSales[m].total);
                }

                if (total > highestTotal)
                {
                    highestTotal = total;
                    month = i;
                }
            }

            switch (month)
            {
                case 1:
                    highestMonth = "January";
                    break;

                case 2:
                    highestMonth = "February";
                    break;

                case 3:
                    highestMonth = "March";
                    break;

                case 4:
                    highestMonth = "April";
                    break;

                case 5:
                    highestMonth = "May";
                    break;

                case 6:
                    highestMonth = "June";
                    break;

                case 7:
                    highestMonth = "July";
                    break;

                case 8:
                    highestMonth = "August";
                    break;

                case 9:
                    highestMonth = "September";
                    break;

                case 10:
                    highestMonth = "October";
                    break;

                case 11:
                    highestMonth = "November";
                    break;

                case 12:
                    highestMonth = "December";
                    break;
            }

            highestSales = highestTotal;
            return;
        }
    }
}