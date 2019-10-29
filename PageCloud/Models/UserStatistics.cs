using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PageCloud.Models;
using System.ComponentModel.DataAnnotations;

namespace PageCloud.Models
{
    public class UserStatistics
    {
        PageCloudEntities db = new PageCloudEntities();

        [Display(Name = "Total Users")]
        public int totalUsers { get; set; }

        [Display(Name = "Active Domains")]
        public int activeDomains { get; set; }  

        [Display(Name = "Inactive Domains")]
        public int inactiveDomains { get; set; }

        [Display(Name = "Total Domains")]
        public int totalDomains { get; set; }

        public UserStatistics(int totUsers = 0, int activeWebistes = 0, int inactiveWebsites = 0, int totDomains = 0)
        {
            totalUsers = totUsers;
            activeDomains = activeWebistes;
            inactiveDomains = inactiveWebsites;
            totalDomains = totDomains;
        }


        public void calculateUserStats()
        {
            totalUsers = db.Users.Where(a => a.userRole != "admin").Count();
            activeDomains = db.Domains.Where(a => a.websiteStatus == "active").Count();
            inactiveDomains = db.Domains.Where(a => a.websiteStatus == "inactive").Count();
            totalDomains = activeDomains + inactiveDomains;
        }
    }
}