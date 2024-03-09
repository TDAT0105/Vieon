//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vieon.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Phim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phim()
        {
            this.BinhLuans = new HashSet<BinhLuan>();
            this.Phim_DaoDien = new HashSet<Phim_DaoDien>();
            this.Phim_TheLoai = new HashSet<Phim_TheLoai>();
            this.PhimYeuThiches = new HashSet<PhimYeuThich>();
            this.TapPhims = new HashSet<TapPhim>();
        }
    
        public int ID_Phim { get; set; }
        public string TenPhim { get; set; }
        public string MoTa { get; set; }
        public Nullable<System.DateTime> NamRaMat { get; set; }
        public string ThoiLuong { get; set; }
        public string LoaiPhim { get; set; }
        public string HinhMinhHoa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phim_DaoDien> Phim_DaoDien { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phim_TheLoai> Phim_TheLoai { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhimYeuThich> PhimYeuThiches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TapPhim> TapPhims { get; set; }
    }
}