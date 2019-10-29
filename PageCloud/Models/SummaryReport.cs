using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PageCloud.Models
{
    public class SummaryReport
    {
        public UserStatistics userStatistics { get; set; }
        public ProfitSummary profit { get; set; }

    }
}