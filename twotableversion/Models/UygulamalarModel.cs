using Microsoft.AspNetCore.Mvc.Rendering;
using twotableversion.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace twotableversion.Models
{
    [ValidateNever]
    public class UygulamalarModel
    {
        //public int SelectedTakvimId { get; set; }
        //public string SelectedUygulamaAdı { get; set; }
      
        //public List<Uygulamalar> SelectedUygulamaList { get; set; }
        //public List<Uygulamalar> UygulamaList { get; set; }
        public List<Uygulamalar> AllUygulamaList { get; set; }
        //public List<Uygulamalar> SelectedId { get; set; }
        //public List<SelectList> SelectedRowList { get; set; } 

        public bool FilterApplied { get; set; }


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


        [Required(ErrorMessage = "Senaryo ID farklı olmalıdır.")]
        public int? SenaryoID { get; set; }
        public string Islem { get; set; }
    }
}

