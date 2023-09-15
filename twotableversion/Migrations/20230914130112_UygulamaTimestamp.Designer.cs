﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using twotableversion.Data;

#nullable disable

namespace twotableversion.Migrations
{
    [DbContext(typeof(DbforlastversionContext))]
    [Migration("20230914130112_UygulamaTimestamp")]
    partial class UygulamaTimestamp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("twotableversion.Data.Takvim", b =>
                {
                    b.Property<int>("Primarykey")
                        .HasColumnType("int")
                        .HasColumnName("primarykey");

                    b.Property<string>("AyAdı")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Ay Adı");

                    b.Property<int>("AyId")
                        .HasColumnType("int")
                        .HasColumnName("AyId");

                    b.Property<string>("Uygulama")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Uygulama");

                    b.HasKey("Primarykey");

                    b.ToTable("Takvim", (string)null);
                });

            modelBuilder.Entity("twotableversion.Data.Uygulamalar", b =>
                {
                    b.Property<int?>("satırID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Satır Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("satırID"));

                    b.Property<string>("BeTaşımaKatmanları")
                        .HasColumnType("text")
                        .HasColumnName("BE Taşıma Katmanları");

                    b.Property<string>("BulguDurumu")
                        .HasColumnType("text")
                        .HasColumnName("BULGU DURUMU");

                    b.Property<string>("EtkiAlanı")
                        .HasColumnType("text")
                        .HasColumnName("Etki Alanı");

                    b.Property<string>("GeçİşZorunluluğu")
                        .HasColumnType("text")
                        .HasColumnName("[GEÇİŞ ZORUNLULUĞU");

                    b.Property<string>("KktOnayiAlindiMi")
                        .HasColumnType("text")
                        .HasColumnName("KKT ONAYI ALINDI MI");

                    b.Property<string>("KktyeGönderİldİMİ")
                        .HasColumnType("text")
                        .HasColumnName("KKTYE GÖNDERİLDİ Mİ?");

                    b.Property<string>("MergeDurumuAnd")
                        .HasColumnType("text")
                        .HasColumnName("[MERGE DURUMU AND");

                    b.Property<string>("MergeDurumuBe")
                        .HasColumnType("text")
                        .HasColumnName("[MERGE DURUMU BE");

                    b.Property<string>("MergeDurumuIos")
                        .HasColumnType("text")
                        .HasColumnName("MERGE DURUMU IOS");

                    b.Property<string>("Notlar")
                        .HasColumnType("text");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Segment")
                        .HasColumnType("text")
                        .HasColumnName("SEGMENT");

                    b.Property<int?>("TakvimId")
                        .HasColumnType("int")
                        .HasColumnName("Takvim Id");

                    b.Property<string>("TalepAdı")
                        .HasColumnType("text")
                        .HasColumnName("Talep Adı");

                    b.Property<string>("TalepBug")
                        .HasColumnType("text")
                        .HasColumnName("Talep/Bug");

                    b.Property<int?>("UiApiSenaryoId")
                        .HasColumnType("int")
                        .HasColumnName("UI/API SENARYO ID");

                    b.Property<string>("UygulamaAdı")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Uygulama Adı");

                    b.Property<string>("Version")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Version");

                    b.Property<string>("İlgiliAnalist")
                        .HasColumnType("text")
                        .HasColumnName("İlgili Analist");

                    b.Property<string>("İlgiliAndroidDeveloper")
                        .HasColumnType("text")
                        .HasColumnName("İlgili Android Developer");

                    b.Property<string>("İlgiliBeDeveloper")
                        .HasColumnType("text")
                        .HasColumnName("İlgili BE Developer");

                    b.Property<string>("İlgiliIosDeveloper")
                        .HasColumnType("text")
                        .HasColumnName("İlgili IOS Developer");

                    b.HasKey("satırID");

                    b.ToTable("Uygulamalar", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
