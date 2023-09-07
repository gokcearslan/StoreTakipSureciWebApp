using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using twotableversion.Data;
using twotableversion.Models;

namespace twotableversion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DbforlastversionContext _dbforlastversionContext;


        public HomeController(ILogger<HomeController> logger, DbforlastversionContext dbforlastversionContext)
        {
            _logger = logger;
            _dbforlastversionContext = dbforlastversionContext;
        }

        public IActionResult Index()
        {
            // Retrieve distinct values for TakvimId and UygulamaAdi columns from the database
            var takvimIdOptions = _dbforlastversionContext.Uygulamalars
                .Select(row => row.TakvimId)
                .Distinct()
                .ToList();

            var uygulamaAdiOptions = _dbforlastversionContext.Uygulamalars
                .Select(row => row.UygulamaAdı)
                .Distinct()
                .ToList();

            ViewBag.TakvimIdOptions = new SelectList(takvimIdOptions);
            ViewBag.UygulamaAdiOptions = new SelectList(uygulamaAdiOptions);

            return View();
        }

        [HttpPost]
        public IActionResult DisplayData(string selectedTakvimId, string selectedUygulamaAdi)
        {
            // Convert selectedTakvimId to an integer
            if (int.TryParse(selectedTakvimId, out int takvimId))
            {
                // Retrieve data based on the selected TakvimId and UygulamaAdi values
                var data = _dbforlastversionContext.Uygulamalars
                    .Where(row => row.TakvimId == takvimId && row.UygulamaAdı == selectedUygulamaAdi)
                    .ToList();

                ViewBag.SelectedTakvimId = takvimId;
                ViewBag.SelectedUygulamaAdi = selectedUygulamaAdi;

                return View(data);
            }
            else
            {
                // Handle the case where 'selectedTakvimId' is not a valid integer.
                // You can return an error message or perform appropriate error handling.
                return View("ErrorView"); // Replace "ErrorView" with the name of your error view.
            }
        }

        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(UygulamalarModel uygulamalar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUygulama = new Uygulamalar
                    {
                        TakvimId = uygulamalar.TakvimId,
                        UygulamaAdı = uygulamalar.UygulamaAdı,
                        EtkiAlanı = uygulamalar.EtkiAlanı,
                        TalepBug = uygulamalar.TalepBug,
                        TalepAdı = uygulamalar.TalepAdi,
                        BulguDurumu = uygulamalar.BulguDurumu,
                        Segment = uygulamalar.Segment,
                        KktyeGönderİldİMİ = uygulamalar.KKTYeGonderilme,
                        KktOnayiAlindiMi = uygulamalar.KKTOnayi,
                        Notlar = uygulamalar.Notlar,
                        İlgiliAnalist = uygulamalar.Analist,
                        MergeDurumuIos = uygulamalar.mergeIOS,
                        MergeDurumuAnd = uygulamalar.mergeAND,
                        MergeDurumuBe = uygulamalar.mergeBE,
                        İlgiliIosDeveloper = uygulamalar.IOSDev,
                        İlgiliAndroidDeveloper = uygulamalar.ANDDev,
                        İlgiliBeDeveloper = uygulamalar.BEDev,
                        BeTaşımaKatmanları = uygulamalar.TasimaKatmanlari,
                        GeçİşZorunluluğu = uygulamalar.GecisZorunluluğu,
                        UiApiSenaryoId = uygulamalar.SenaryoID
                    };

                    _dbforlastversionContext.Uygulamalars.Add(newUygulama);
                    _dbforlastversionContext.SaveChanges();

                    return RedirectToAction("Index"); // Redirect to the appropriate action
                }
            }
            catch (DbUpdateException e)
            {
                // Log the error and handle it gracefully
                Console.WriteLine(e.InnerException.Message);
                TempData["SaveStatus"] = 0;
            }

            return View(uygulamalar); // Return the view with validation errors
        }




        [HttpGet]
        public IActionResult Edit(int id)
        {
            var existingData = _dbforlastversionContext.Uygulamalars.Find(id);
            if (existingData == null)
            {
                return NotFound(); // Handle the case when the record is not found
            }

            var uygulamalar = new Models.UygulamalarModel
            {
                //TakvimId=existingData.TakvimID,
                UygulamaAdı = existingData.UygulamaAdı,
                EtkiAlanı = existingData.EtkiAlanı,
                TalepBug = existingData.TalepBug,
                TalepAdi = existingData.TalepAdı,
                BulguDurumu = existingData.BulguDurumu,
                Segment = existingData.Segment,
                KKTYeGonderilme = existingData.KktyeGönderİldİMİ,
                KKTOnayi = existingData.KktOnayiAlindiMi,
                Notlar = existingData.Notlar,
                Analist = existingData.İlgiliAnalist,
                mergeIOS = existingData.MergeDurumuIos,
                mergeAND = existingData.MergeDurumuAnd,
                mergeBE = existingData.MergeDurumuBe,
                IOSDev = existingData.İlgiliIosDeveloper,
                ANDDev = existingData.İlgiliAndroidDeveloper,
                BEDev = existingData.İlgiliBeDeveloper,
                TasimaKatmanlari = existingData.BeTaşımaKatmanları,
                GecisZorunluluğu = existingData.GeçİşZorunluluğu,
                // senaryo Id Key olduğu için editlenmiyor değiştirilebilir

                SenaryoID = existingData.UiApiSenaryoId
            };

            return View(uygulamalar);
        }

        [HttpPost]
        public IActionResult Edit(int id, Models.UygulamalarModel uygulamalar)
        {
            var existingData = _dbforlastversionContext.Uygulamalars.Find(id);
            if (existingData == null)
            {
                return NotFound(); // Handle the case when the record is not found
            }

            // Update the fields with new values
            //existingData.TakvimID = uygulamalar.TakvimId;
            //existingData.TakvimID = uygulamalar.TakvimId;
            existingData.EtkiAlanı = uygulamalar.EtkiAlanı;
            existingData.TalepBug = uygulamalar.TalepBug;
            existingData.TalepAdı = uygulamalar.TalepAdi;
            existingData.BulguDurumu = uygulamalar.BulguDurumu;
            existingData.Segment = uygulamalar.Segment;
            existingData.KktyeGönderİldİMİ = uygulamalar.KKTYeGonderilme;
            existingData.KktOnayiAlindiMi = uygulamalar.KKTOnayi;
            existingData.Notlar = uygulamalar.Notlar;
            existingData.İlgiliAnalist = uygulamalar.Analist;
            existingData.MergeDurumuIos = uygulamalar.mergeIOS;
            existingData.MergeDurumuAnd = uygulamalar.mergeAND;
            existingData.MergeDurumuBe = uygulamalar.mergeBE;
            existingData.İlgiliIosDeveloper = uygulamalar.IOSDev;
            existingData.İlgiliAndroidDeveloper = uygulamalar.ANDDev;
            existingData.İlgiliBeDeveloper = uygulamalar.BEDev;
            existingData.BeTaşımaKatmanları = uygulamalar.TasimaKatmanlari;
            existingData.GeçİşZorunluluğu = uygulamalar.GecisZorunluluğu;
            // senaryo Id Key olduğu için editlenmiyor değiştirilebilir
            //existingData.UiApiSenaryoId = uygulamalar.SenaryoID;

            _dbforlastversionContext.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingData = _dbforlastversionContext.Uygulamalars.Find(id);
                if (existingData != null) // Check if the data exists before attempting to remove it
                {
                    _dbforlastversionContext.Uygulamalars.Remove(existingData);
                    _dbforlastversionContext.SaveChanges();
                    // TempData["DeleteStatus"] = 1;
                }
                else
                {
                    // TempData["DeleteStatus"] = 0; // Data not found
                }
            }
            catch (Exception ex)
            {
                //TempData["DeleteStatus"] = 0; // An error occurred
            }

            return RedirectToAction("Index");
        }


        public IActionResult ResultAction()
        {
            // Implement the logic to display results or perform actions based on the selected values.
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };
            return View(errorViewModel);
        }




    }
}