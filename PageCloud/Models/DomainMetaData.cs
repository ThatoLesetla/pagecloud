using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PageCloud.Models.Validations;

namespace PageCloud.Models
{
    [MetadataType(typeof(DomainMetaData))]
    public partial class Domain
    {
    }
        public class DomainMetaData
    {
        [Required]
        [StringLength(30)]
        [Display(Name = "Domain Name")]
        [DomainFinder]
       //[RegularExpression(@"/[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)?/gi", ErrorMessage = "Enter valid domain with https://")]
        public string domainName { get; set; }

        public Nullable<System.DateTime> registrationDate { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Payment Method")]
        public string paymentMethod { get; set; }

        [StringLength(15)]
        [Display(Name = "Status")]
        public string websiteStatus { get; set; }

        public int domainID { get; set; }
    }
}