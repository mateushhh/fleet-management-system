using Microsoft.AspNetCore.Mvc;
using TransitDesk.Web.ViewModels;

namespace TransitDesk.Web.Controllers
{
    public class IncidentsController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncidentReportViewModel model, IFormFile? attachmentFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (attachmentFile != null && attachmentFile.Length > 0)
            {
                if (attachmentFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("attachmentFile", "Rozmiar załącznika nie może przekraczać 5 MB.");
                    return View(model);
                }

                var allowedExt = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
                var ext = Path.GetExtension(attachmentFile.FileName).ToLowerInvariant();
                if (!allowedExt.Contains(ext))
                {
                    ModelState.AddModelError("attachmentFile", "Niedozwolony typ pliku. Dozwolone formaty: .jpg, .jpeg, .png, .pdf.");
                    return View(model);
                }

                var secureFileName = $"{Guid.NewGuid()}{ext}";
                var storageDir = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "Uploads");
                Directory.CreateDirectory(storageDir);

                var targetPath = Path.Combine(storageDir, secureFileName);

                using (var stream = new FileStream(targetPath, FileMode.Create))
                {
                    await attachmentFile.CopyToAsync(stream);
                }

                model.AttachmentPath = targetPath;
            }

            TempData["SuccessMessage"] = "Zgłoszenie incydentu wraz z załącznikiem zostało pomyślnie zarejestrowane.";
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public IActionResult VerifyVehicleAvailable(int vehicleSideNumber)
        {
            bool existsAndActive = vehicleSideNumber >= 1000 && vehicleSideNumber <= 9999;

            if (!existsAndActive)
            {
                return Json($"Pojazd o podanym numerze nie istnieje lub jest wycofany.");
            }

            return Json(true);
        }
    }
}