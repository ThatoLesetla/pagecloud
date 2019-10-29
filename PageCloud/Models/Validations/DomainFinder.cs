using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;
using PageCloud.Models;

namespace PageCloud.Models.Validations
{    
    public class DomainFinder : ValidationAttribute
    {
        PageCloudEntities db = new PageCloudEntities();

   
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var domain = (Domain)validationContext.ObjectInstance;
            var domainObj = db.Domains.Where(a => a.domainName == domain.domainName).FirstOrDefault();
            //var host = Dns.GetHostEntry("www.google.com");
            

            if (domainObj != null)
            {
                return new ValidationResult("Domain already exits");
            }

            //if (host == null)
            //{
            //    return new ValidationResult("Domain" + domainObj.domainName + " already exists");
            //}

        
            return ValidationResult.Success;
        }
    }
}