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
    
    public partial class UserType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserType()
        {
            this.siteusers = new HashSet<siteuser>();
        }
    
        public int UserTypeId { get; set; }
        public string Usertype1 { get; set; }
        public bool status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<siteuser> siteusers { get; set; }
    }
}
