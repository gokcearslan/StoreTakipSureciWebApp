using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using twotableversion.Models;

namespace twotableversion.Data;

public partial class DbforlastversionContext : DbContext
{
    public DbforlastversionContext()
    {
    }

    public DbforlastversionContext(DbContextOptions<DbforlastversionContext> options)
        : base(options)
    {
    }

   

    public virtual DbSet<Takvim> Takvims { get; set; } = null!;
    public virtual DbSet<Uygulamalar> Uygulamalars { get; set; } = null!;



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=GÖKÇEA\\SQLEXPRESS;Database=DBforlastversion;User ID=MobileStoreAppUser; Password=1234; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {





        modelBuilder.Entity<Takvim>(entity =>
        {
            entity.ToTable("Takvim");
            entity.HasKey(e => e.Primarykey);

            entity.Property(e => e.Primarykey)
                .ValueGeneratedNever()
               .HasColumnName("primarykey");

            entity.Property(e => e.AyId)
                .ValueGeneratedNever()
                .HasColumnName("AyId");

            entity.Property(e => e.AyAdı)
                .HasColumnType("varchar(max)")
                .HasColumnName("Ay Adı");

            entity.Property(e => e.Uygulama)
                .HasColumnType("varchar(max)")
                .HasColumnName("Uygulama");
        });




        modelBuilder.Entity<Uygulamalar>(entity =>
        {
            entity.ToTable("Uygulamalar");
            entity.HasKey(e => e.satırID);

            // Configure satırID as an identity column
            entity.Property(e => e.satırID)
                .ValueGeneratedOnAdd() // Specify that it should be auto-generated
                .HasColumnName("Satır Id");
            entity.Property(e => e.RowVersion)
           .HasColumnType("timestamp")
           .HasColumnName("Row Version");



            entity.Property(e => e.IsLocked)
              .HasColumnType("bit")
              .HasColumnName("IsLocked");


            entity.Property(e => e.BeTaşımaKatmanları)
                .HasColumnType("text")
                .HasColumnName("BE Taşıma Katmanları");
            entity.Property(e => e.BulguDurumu)
                .HasColumnType("text")
                .HasColumnName("BULGU DURUMU");
            

            entity.Property(e => e.EtkiAlanı)
                .HasColumnType("text")
                .HasColumnName("Etki Alanı");
            entity.Property(e => e.GeçİşZorunluluğu)
                .HasColumnType("text")
                .HasColumnName("[GEÇİŞ ZORUNLULUĞU");
            entity.Property(e => e.KktOnayiAlindiMi)
                .HasColumnType("text")
                .HasColumnName("KKT ONAYI ALINDI MI");
            entity.Property(e => e.KktyeGönderİldİMİ)
                .HasColumnType("text")
                .HasColumnName("KKTYE GÖNDERİLDİ Mİ?");
            entity.Property(e => e.MergeDurumuAnd)
                .HasColumnType("text")
                .HasColumnName("[MERGE DURUMU AND");
            entity.Property(e => e.MergeDurumuBe)
                .HasColumnType("text")
                .HasColumnName("[MERGE DURUMU BE");
            entity.Property(e => e.MergeDurumuIos)
                .HasColumnType("text")
                .HasColumnName("MERGE DURUMU IOS");
            entity.Property(e => e.Notlar).HasColumnType("text");
            entity.Property(e => e.Segment)
                .HasColumnType("text")
                .HasColumnName("SEGMENT");
            entity.Property(e => e.TakvimId).HasColumnName("Takvim Id");
            entity.Property(e => e.TalepAdı)
                .HasColumnType("text")
                .HasColumnName("Talep Adı");
            entity.Property(e => e.TalepBug)
                .HasColumnType("text")
                .HasColumnName("Talep/Bug");


            //entity.HasKey(e => e.UiApiSenaryoId);

            //entity.Property(e => e.UiApiSenaryoId).HasColumnName("UI/API SENARYO ID");

            entity.Property(e => e.UiApiSenaryoId)
                .ValueGeneratedNever()
                .HasColumnName("UI/API SENARYO ID");

            entity.Property(e => e.UygulamaAdı)
              .HasColumnType("varchar(max)")
              .HasColumnName("Uygulama Adı");


                entity.Property(e => e.Version)
              .HasColumnType("varchar(max)")
              .HasColumnName("Version");

            entity.Property(e => e.İlgiliAnalist)
                .HasColumnType("text")
                .HasColumnName("İlgili Analist");
            entity.Property(e => e.İlgiliAndroidDeveloper)
                .HasColumnType("text")
                .HasColumnName("İlgili Android Developer");
            entity.Property(e => e.İlgiliBeDeveloper)
                .HasColumnType("text")
                .HasColumnName("İlgili BE Developer");
            entity.Property(e => e.İlgiliIosDeveloper)
                .HasColumnType("text")
                .HasColumnName("İlgili IOS Developer");
            entity.Property(e => e.TakvimId).HasColumnName("Takvim Id");



       
            //entity.Property(p => p.RowVersion).IsConcurrencyToken();
        });

       

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
