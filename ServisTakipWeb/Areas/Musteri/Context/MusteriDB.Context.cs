﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServisTakipWeb.Areas.Musteri.Context
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ServisTakipMusteriDBEntities : DbContext
    {
        public ServisTakipMusteriDBEntities()
            : base("name=ServisTakipMusteriDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Musteri> Musteri { get; set; }
        public virtual DbSet<MusteriCalisani> MusteriCalisani { get; set; }
        public virtual DbSet<MusteriYonetici> MusteriYonetici { get; set; }
    }
}
