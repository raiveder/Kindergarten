
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
    
public partial class Photos
{

    public int Id_photo { get; set; }

    public Nullable<int> Id_employee { get; set; }

    public Nullable<int> Id_children { get; set; }

    public byte[] Byte_photo { get; set; }

    public string Path_photo { get; set; }



    public virtual Children Children { get; set; }

    public virtual Employees Employees { get; set; }

}

}
