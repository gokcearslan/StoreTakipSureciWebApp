using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Diagnostics;
using twotableversion.Data;
using Microsoft.AspNetCore.Http;
//using twotableversion.Hubs;
using twotableversion.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;



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



        [HttpGet]
        public IActionResult DisplayData()
        {
            int takvimId = Convert.ToInt32(TempData["SelectedTakvimId"]);
            string selectedUygulamaAdi = Convert.ToString(TempData["SelectedUygulamaAdi"]);
            return DisplayData(Convert.ToString(takvimId), selectedUygulamaAdi, "");
        }

        [HttpPost]
        public IActionResult DisplayData(string selectedTakvimId, string selectedUygulamaAdi, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(selectedUygulamaAdi)) // Check if the UygulamaAdi is not selected
            {
                return View("ErrorView"); // Return the error view
            }

            if (int.TryParse(selectedTakvimId, out int takvimId))
            {
                var data = _dbforlastversionContext.Uygulamalars
                    .Where(row => row.TakvimId == takvimId && row.UygulamaAdı == selectedUygulamaAdi)
                    .ToList();

                ViewBag.SelectedTakvimId = takvimId;
                ViewBag.SelectedUygulamaAdi = selectedUygulamaAdi;

                TempData["SelectedTakvimId"] = takvimId;
                TempData["SelectedUygulamaAdi"] = selectedUygulamaAdi;

                return View(data);
            }
            else
            {
                return View("ErrorView"); // Return the error view
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
                            UiApiSenaryoId = uygulamalar.SenaryoID,
                            Version = uygulamalar.version
                        };


                        _dbforlastversionContext.Uygulamalars.Add(newUygulama);
                        _dbforlastversionContext.SaveChanges();


                        TempData["SelectedTakvimId"] = uygulamalar.TakvimId;
                        TempData["SelectedUygulamaAdi"] = uygulamalar.UygulamaAdı;

                        return RedirectToAction("DisplayData");
                    }
                }
            }
            catch (DbUpdateException e)
            {
                // Handle the exception...
                TempData["SaveStatus"] = 0;
            }

            return View(uygulamalar); // Return the view with validation errors
        }







 








        [HttpGet]
        public IActionResult SaveTakvim()
        {
            return View();
        }



        [HttpPost]
        public IActionResult SaveTakvim(UygulamalarModel uygulamalar)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    var newUygulama = new Uygulamalar
                    {

                        TakvimId = uygulamalar.TakvimId,
                        UygulamaAdı = uygulamalar.UygulamaAdı,
                        Version = uygulamalar.version
                    };

                    _dbforlastversionContext.Uygulamalars.Add(newUygulama);
                    _dbforlastversionContext.SaveChanges();

                    return RedirectToAction("Index"); 
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

            if (existingData.IsLocked)
            {
                //ViewBag.ErrorMessage = "Bu kayıt şu anda başka bir kullanıcı tarafından düzenlenmektedir.";
                return View("EditError");
            }

            existingData.IsLocked = true;
            _dbforlastversionContext.SaveChanges();
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
                SenaryoID = existingData.UiApiSenaryoId,
                version = existingData.Version,

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
            if (!existingData.IsLocked)
            {
                // The record is not locked by the current user; handle this situation.
                return RedirectToAction("Index", new { errorMessage = "Bu kayıt size kilitleme yapılmadan önce düzenlenmiş olabilir." });

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
            existingData.UiApiSenaryoId = uygulamalar.SenaryoID;
            existingData.Version = uygulamalar.version;
            existingData.IsLocked = false;

            _dbforlastversionContext.SaveChanges();
            int takvimId = Convert.ToInt32(TempData["SelectedTakvimId"]);
            string selectedUygulamaAdi = Convert.ToString(TempData["SelectedUygulamaAdi"]);
            return RedirectToAction("DisplayData", new { selectedTakvimId = takvimId, selectedUygulamaAdi = selectedUygulamaAdi, errorMessage = "" });
        }





        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingData = _dbforlastversionContext.Uygulamalars.Find(id);
                if (existingData != null)
                {
                    _dbforlastversionContext.Uygulamalars.Remove(existingData);
                    _dbforlastversionContext.SaveChanges();
                    //TempData["DeleteStatus"] = 1;
                }
                else
                {
                    /*TempData["DeleteStatus"] = 0;*/ // Data not found
                }
            }
            catch (Exception ex)
            {
                //TempData["DeleteStatus"] = 0; // An error occurred
            }

            //return RedirectToAction("Index");
            int takvimId = Convert.ToInt32(TempData["SelectedTakvimId"]);
            string selectedUygulamaAdi = Convert.ToString(TempData["SelectedUygulamaAdi"]);



            return RedirectToAction("DisplayData", new { selectedTakvimId = takvimId, selectedUygulamaAdi = selectedUygulamaAdi, errorMessage = "" });
        }

        [HttpPost]
        public IActionResult ExportToExcel(string selectedTakvimId, string selectedUygulamaAdi)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (int.TryParse(selectedTakvimId, out int takvimId))
            {
                var data = _dbforlastversionContext.Uygulamalars
                    .Where(row => row.TakvimId == takvimId && row.UygulamaAdı == selectedUygulamaAdi)
                    .ToList();


                using (var package = new ExcelPackage())
                {

                    var worksheet = package.Workbook.Worksheets.Add("Uygulama Data");

                    // headers
                    var properties = typeof(Uygulamalar).GetProperties();
                    for (int i = 3; i < properties.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = properties[i].Name;
                    }

                    // data
                    for (int row = 0; row < data.Count; row++)
                    {
                        for (int col = 3; col < properties.Length; col++)
                        {
                            worksheet.Cells[row + 2, col + 1].Value = properties[col].GetValue(data[row]);
                        }
                    }

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    var fileName = "UygulamaData.xlsx";
                    var stream = new MemoryStream(package.GetAsByteArray());
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            else
            {
                // Handle the case where 'selectedTakvimId' is not a valid integer.
                // You can return an error message or perform appropriate error handling.
                return View("ErrorView"); // Replace "ErrorView" with the name of your error view.
            }
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