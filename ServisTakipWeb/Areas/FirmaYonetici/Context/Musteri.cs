//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServisTakipWeb.Areas.FirmaYonetici.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Musteri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Musteri()
        {
            this.SozlesmeYapma = new HashSet<SozlesmeYapma>();
        }
    
        public int ID { get; set; }
        public string MusteriKodu { get; set; }
        public string MusteriAdi { get; set; }
        public string YetkiliKisi { get; set; }
        public string Password { get; set; }
        public string VergiDairesi { get; set; }
        public string VergiNumarasi { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public string MusteriTel { get; set; }
        public string MusteriTel2 { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SozlesmeYapma> SozlesmeYapma { get; set; }
    }
}
