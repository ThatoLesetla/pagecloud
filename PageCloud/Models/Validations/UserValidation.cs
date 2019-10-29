using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PageCloud.Models.Validations
{
    public class UserValidation : ValidationAttribute
    {
        PageCloudEntities db = new PageCloudEntities();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (User)validationContext.ObjectInstance;
            var userObj = db.Users.Where(a => a.userEmail.Equals(user.userEmail)).FirstOrDefault();

            if(userObj == null)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Email address already exits");
            
        }

    }
}