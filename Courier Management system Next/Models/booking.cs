//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Courier_Management_system_Next.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class booking
    {
        public int bookID { get; set; }
        public string bookOriginFirstName { get; set; }
        public string bookOriginLastName { get; set; }
        public string bookOriginAddress { get; set; }
        public string bookOriginPhoneNo { get; set; }
        public string bookDestFirstName { get; set; }
        public string bookDestLastName { get; set; }
        public string bookDestAddress { get; set; }
        public string bookDestPhoneNo { get; set; }
        public Nullable<double> bookingProductWeight { get; set; }
        public string bookDescription { get; set; }
        public Nullable<int> BookingTypeId { get; set; }
        public Nullable<double> bCost { get; set; }
        public Nullable<int> SiteUserid { get; set; }
        public Nullable<int> dID { get; set; }
        public Nullable<int> OriginAreaID { get; set; }
        public Nullable<int> DestAreaID { get; set; }
        public string status { get; set; }
        public string TrackingNumber { get; set; }
        public Nullable<System.DateTime> bookingDate { get; set; }
        public Nullable<bool> statusbit { get; set; }
    
        public virtual AreaInfo AreaInfo { get; set; }
        public virtual AreaInfo AreaInfo1 { get; set; }
        public virtual BookingDelivareyType BookingDelivareyType { get; set; }
        public virtual deliveryMan deliveryMan { get; set; }
        public virtual siteuser siteuser { get; set; }
    }
}
