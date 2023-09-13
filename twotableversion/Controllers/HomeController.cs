using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Diagnostics;
using twotableversion.Data;
//using twotableversion.Hubs;
using twotableversion.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace twotableversion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DbforlastversionContext _dbforlastversionContext;
        private readonly IHubContext<CrudHub> _hubContext;

        public HomeController(ILogger<HomeController> logger, DbforlastversionContext dbforlastversionContext, IHubContext<CrudHub> hubContext)
        {
            _logger = logger;
            _dbforlastversionContext = dbforlastversionContext;
            _hubContext = hubContext;

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

            if (int.TryParse(selectedTakvimId, out int takvimId))
            {
                var data = _dbforlastversionContext.Uygulamalars
                    .Where(row => row.TakvimId == takvimId && row.UygulamaAdı == selectedUygulamaAdi)
                    .ToList();

                ViewBag.SelectedTakvimId = takvimId;
                ViewBag.SelectedUygulamaAdi = selectedUygulamaAdi;

                return View(data);

            }
            else
            {
                return View("ErrorView"); // Replace "ErrorView" with the name of your error view.
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> DisplayData(string selectedTakvimId, string selectedUygulamaAdi)
        //{


        //    if (int.TryParse(selectedTakvimId, out int takvimId))
        //    {
        //        var data = _dbforlastversionContext.Uygulamalars
        //            .Where(row => row.TakvimId == takvimId && row.UygulamaAdı == selectedUygulamaAdi)
        //            .ToList();

        //        ViewBag.SelectedTakvimId = takvimId;
        //        ViewBag.SelectedUygulamaAdi = selectedUygulamaAdi;
        //        await NotifyClients("DisplayData", "güncelleme");

        //        return View(data);



        //    }


        //    else
        //    {

        //        return View("ErrorView"); // Replace "ErrorView" with the name of your error view.
        //    }
    //}

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
                        UiApiSenaryoId = uygulamalar.SenaryoID,
                        Version = uygulamalar.version
                    };

                    _dbforlastversionContext.Uygulamalars.Add(newUygulama);
                    _dbforlastversionContext.SaveChanges();
                    //await NotifyClients("Save", "A new record has been created.");
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

            return RedirectToAction("Index");
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
                    for (int i = 0; i < properties.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = properties[i].Name;
                    }

                    // data
                    for (int row = 0; row < data.Count; row++)
                    {
                        for (int col = 0; col < properties.Length; col++)
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

        //[HttpPost]
        //public IActionResult ImportFromExcel(IFormFile excelFile)
        //{
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //    if (excelFile != null && excelFile.Length > 0)
        //    {
        //        using (var stream = new MemoryStream())
        //        {
        //            excelFile.CopyTo(stream);

        //            using (var package = new ExcelPackage(stream))
        //            {
        //                var worksheet = package.Workbook.Worksheets[0]; // Assuming the data is in the first worksheet.

        //                var rowCount = worksheet.Dimension.Rows;
        //                var colCount = worksheet.Dimension.Columns;

        //                var importedData = new List<Uygulamalar>();

        //                for (int row = 2; row <= rowCount; row++) // Start from the second row to skip headers.
        //                {
        //                    var rowData = new Uygulamalar();

        //                    for (int col = 1; col <= colCount; col++)
        //                    {
        //                        var cellValue = worksheet.Cells[row, col].Value;

        //                        // Assuming the order of columns in the Excel file matches the Uygulamalar model.
        //                        switch (col)
        //                        {
        //                            case 1:
        //                                rowData.UygulamaAdı = cellValue?.ToString();
        //                                break;
        //                            case 2:
        //                                int takvimId;
        //                                if (int.TryParse(cellValue?.ToString(), out takvimId))
        //                                {
        //                                    rowData.TakvimId = takvimId;
        //                                }
        //                                break;
        //                            case 3:
        //                                rowData.EtkiAlanı = cellValue?.ToString();
        //                                break;
        //                            case 4:
        //                                rowData.TalepBug = cellValue?.ToString();
        //                                break;
        //                            case 5:
        //                                rowData.BulguDurumu = cellValue?.ToString();
        //                                break;
        //                            case 6:
        //                                rowData.Segment = cellValue?.ToString();
        //                                break;
        //                            case 7:
        //                                rowData.KktyeGönderİldİMİ = cellValue?.ToString();
        //                                break;
        //                            case 8:
        //                                rowData.KktOnayiAlindiMi = cellValue?.ToString();
        //                                break;
        //                            case 9:
        //                                rowData.Notlar = cellValue?.ToString();
        //                                break;
        //                            case 10:
        //                                rowData.İlgiliAnalist = cellValue?.ToString();
        //                                break;
        //                            case 11:
        //                                rowData.MergeDurumuIos = cellValue?.ToString();
        //                                break;
        //                            case 12:
        //                                rowData.MergeDurumuAnd = cellValue?.ToString();
        //                                break;
        //                            case 13:
        //                                rowData.MergeDurumuBe = cellValue?.ToString();
        //                                break;
        //                            case 14:
        //                                rowData.İlgiliIosDeveloper = cellValue?.ToString();
        //                                break;
        //                            case 15:
        //                                rowData.İlgiliAndroidDeveloper = cellValue?.ToString();
        //                                break;
        //                            case 16:
        //                                rowData.İlgiliBeDeveloper = cellValue?.ToString();
        //                                break;
        //                            case 17:
        //                                rowData.BeTaşımaKatmanları = cellValue?.ToString();
        //                                break;
        //                            case 18:
        //                                rowData.GeçİşZorunluluğu = cellValue?.ToString();
        //                                break;
        //                            case 19:
        //                                int senaryoId;
        //                                if (int.TryParse(cellValue?.ToString(), out senaryoId))
        //                                {
        //                                    rowData.UiApiSenaryoId = senaryoId;
        //                                }
        //                                break;
        //                                // Add more cases for additional columns as needed.
        //                        }
        //                    }

        //                    importedData.Add(rowData);
        //                }

        //                _dbforlastversionContext.Uygulamalars.AddRange(importedData);
        //                _dbforlastversionContext.SaveChanges();

        //                return RedirectToAction("Index"); // Redirect to a page after successful import.
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Handle the case where no file was uploaded or the file is empty.
        //        // You can return an error message or perform appropriate error handling.
        //        return View("ErrorView"); // Replace "ErrorView" with the name of your error view.
        //    }
        //}


        //[HttpPost]
        //public async Task<ActionResult> ImportFile()
        //{
        //    return View("Index");
        //}




        // Version bilgisini görüntülemek için

        public IActionResult Details(string version)
        {
            var uygulama = _dbforlastversionContext.Uygulamalars.FirstOrDefault(u => u.Version == version);
            if (uygulama == null)
            {
                return NotFound();
            }

            // Pass the specific Uygulamalar object to the view
            return View(uygulama);
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
        private async Task NotifyClients(string operation, string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveCrudOperation", operation, message);

        }
    }
}