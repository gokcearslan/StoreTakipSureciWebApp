using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace twotableversion.Data;

public partial class Uygulamalar
{


    
    public int? satırID { get; set; }

    [Timestamp]
    public byte[] RowVersion
    {
        get; set;
    }
    public bool IsLocked { get; set; }
    public string? Version { get; set; }
    public string? UygulamaAdı { get; set; }

    public int? TakvimId { get; set; }

    public string? EtkiAlanı { get; set; }

    public string? TalepBug { get; set; }

    public string? TalepAdı { get; set; }

    public string? BulguDurumu { get; set; }

    public string? Segment { get; set; }

    public string? KktyeGönderİldİMİ { get; set; }

    public string? KktOnayiAlindiMi { get; set; }

    public string? Notlar { get; set; }

    public string? İlgiliAnalist { get; set; }

    public string? MergeDurumuIos { get; set; }

    public string? MergeDurumuAnd { get; set; }

    public string? MergeDurumuBe { get; set; }

    public string? İlgiliIosDeveloper { get; set; }

    public string? İlgiliAndroidDeveloper { get; set; }

    public string? İlgiliBeDeveloper { get; set; }

    public string? BeTaşımaKatmanları { get; set; }

    public string? GeçİşZorunluluğu { get; set; }

    public int? UiApiSenaryoId { get; set; }

 



}