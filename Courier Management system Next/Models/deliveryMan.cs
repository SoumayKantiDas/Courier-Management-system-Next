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
    
    public partial class deliveryMan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public deliveryMan()
        {
            this.bookings = new HashSet<booking>();
        }
    
        public int dID { get; set; }
        public string dFirstName { get; set; }
        public string dLastName { get; set; }
        public string dPhoneNo { get; set; }
        public Nullable<double> dSalary { get; set; }
        public string dAddress { get; set; }
        public Nullable<bool> status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<booking> bookings { get; set; }
    }
}
