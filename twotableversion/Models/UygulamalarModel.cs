using Microsoft.AspNetCore.Mvc.Rendering;
using twotableversion.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace twotableversion.Models
{
    [ValidateNever]
    public class UygulamalarModel
    {
      
        public List<Uygulamalar>? AllUygulamaList { get; set; }


        public bool FilterApplied { get; set; }
        public bool IsLocked { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public string? version { get; set; }
        public int satırId { get; set; }


        public int? TakvimId { get; set; }

     
        public string? UygulamaAdı { get; set; }
        public string? EtkiAlanı { get; set; }
        public string? TalepBug { get; set; }
        public string? TalepAdi { get; set; }
        public string? BulguDurumu { get; set; }
        public string? Segment { get; set; }
        public string? KKTYeGonderilme { get; set; }
        public string? KKTOnayi { get; set; }
        public string? Notlar { get; set; }
        public string? Analist { get; set; }
        public string? mergeIOS { get; set; }
        public string? mergeAND { get; set; }
        public string? mergeBE { get; set; }
        public string? IOSDev { get; set; }
        public string? ANDDev { get; set; }
        public string? BEDev { get; set; }
        public string? TasimaKatmanlari { get; set; }
        public string? GecisZorunluluğu { get; set; }


        //[Required(ErrorMessage = "Senaryo ID farklı olmalıdır.")]
        public int? SenaryoID { get; set; }
        public string Islem { get; set; }



    }
}

