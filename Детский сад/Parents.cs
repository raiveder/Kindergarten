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
    
    public partial class Parents
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Parents()
        {
            this.Kinships = new HashSet<Kinships>();
        }
    
        public int Id_parent { get; set; }
        public string Surname { get; set; }
        public string Names { get; set; }
        public string Patronymic { get; set; }
        public System.DateTime Birthdate { get; set; }
        public int Id_gender { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Phone { get; set; }
    
        public virtual Genders Genders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kinships> Kinships { get; set; }
    }
}
