//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Детский_сад
{
    using System;
    using System.Collections.Generic;
    
    public partial class Children
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Children()
        {
            this.Attendance = new HashSet<Attendance>();
            this.Kinships = new HashSet<Kinships>();
        }
    
        public int Id_child { get; set; }
        public string Surname { get; set; }
        public string Names { get; set; }
        public string Patronymic { get; set; }
        public System.DateTime Birthdate { get; set; }
        public int Id_gender { get; set; }
        public int Id_group { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendance { get; set; }
        public virtual Sertificates Sertificates { get; set; }
        public virtual Genders Genders { get; set; }
        public virtual Groups Groups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kinships> Kinships { get; set; }
    }
}
