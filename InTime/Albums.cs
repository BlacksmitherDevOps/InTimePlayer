//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InTime
{
    using System;
    using System.Collections.Generic;
    
    public partial class Albums
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Albums()
        {
            this.Tracks = new HashSet<Tracks>();
        }
    
        public int Album_ID { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public Nullable<int> Singer_Singer_ID { get; set; }
    
        public virtual Singers Singers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tracks> Tracks { get; set; }
    }
}
