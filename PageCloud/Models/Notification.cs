//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PageCloud.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Notification
    {
        public int notificationId { get; set; }
        public string notificationBody { get; set; }
        public Nullable<System.DateTime> notificationDate { get; set; }
        public Nullable<int> userId { get; set; }
    
        public virtual User User { get; set; }
    }
}