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
    
    public partial class Distributions
    {
        public int Id_distribution { get; set; }
        public int Id_group { get; set; }
        public int Id_type { get; set; }
    
        public virtual Groups Groups { get; set; }
        public virtual Types_group Types_group { get; set; }
    }
}
