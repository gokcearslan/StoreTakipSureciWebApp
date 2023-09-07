using System.Web.Mvc;

namespace twotableversion.Models
{
    public class TakvimModel
    {
        public List<TakvimModel> TakvimList { get; set; }

        public int Primarykey { get; set; }

        public int AyId { get; set; }
        public string AyAdı { get; set; }
        public string Uygulama { get; set; }


       

    }
}
