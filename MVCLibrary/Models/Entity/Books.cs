//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCLibrary.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Books
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Books()
        {
            this.LibraryOperations = new HashSet<LibraryOperations>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Category { get; set; }
        public Nullable<int> Author { get; set; }
        public string PublicationYear { get; set; }
        public string PublisherCompany { get; set; }
        public string PageCount { get; set; }
        public Nullable<bool> State { get; set; }
        public string Photo { get; set; }
    
        public virtual Authors Authors { get; set; }
        public virtual Categories Categories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LibraryOperations> LibraryOperations { get; set; }
    }
}
