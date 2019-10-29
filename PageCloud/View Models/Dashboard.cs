using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PageCloud.Models;

namespace PageCloud.View_Models
{
    public class Dashboard
    {
        public int domains { get; set; }
        public int invoices { get; set; }
        public double amountDue { get; set; }
        public IEnumerable<Domain> websites { get; set; }
        public int users { get; set; }
        public double totalIncome {get; set; }
        
    }
}