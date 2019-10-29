using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PageCloud.Models.Validations;

namespace PageCloud.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
    }

    public class UserMetaData
    {

        [Display(Name = "Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only contain letters")]
        [StringLength(20)]
        public string userName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only contain letters")]
        [StringLength(20)]
        public string userSurname { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [UserValidation]
        public string userEmail { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string userPassword { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9''-'\s]{1,40}$", ErrorMessage = "Enter valid phone number")]
        [StringLength(10)]
        public string phone { get; set; }

        [Display(Name = "Address")]
        [Required]
        [StringLength(35)]
        public string userAddress { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "City")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only contain letters")]
        public string city { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [StringLength(4)]
        [RegularExpression(@"^[0-9''-'\s]{1,40}$", ErrorMessage = "Enter valid postal code")]
        public string postalCode { get; set; }

        [StringLength(13)]
        [Required]
        [Display(Name = "ID Number")]
        [RegularExpression(@"(?<Year>[5-9][0-9])(?<Month>([0][1-9])|([1][0-2]))(?<Day>([0-2][0-9])|([3][0-1]))(?<Gender>[0-9])(?<Series>[0-9]{3})(?<Citizenship>[0-9])(?<Uniform>[0-9])(?<Control>[0-9])", ErrorMessage = "Enter valid id number")]
        public string idno { get; set; }

    }
}