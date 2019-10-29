using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PageCloud.Models;

namespace PageCloud.Models.Validations
{
    public class SAID : ValidationAttribute
    {
        PageCloudEntities db = new PageCloudEntities();
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (User)validationContext.ObjectInstance;
            var userObj = db.Users.Where(a => a.idno.Equals(user.idno)).FirstOrDefault();

            if (userObj != null)
            {
                return new ValidationResult("ID already exits");
            }

            

            return ValidationResult.Success;
            
        }
    }
}