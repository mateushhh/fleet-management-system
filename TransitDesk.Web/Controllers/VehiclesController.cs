using Microsoft.AspNetCore.Mvc;
using TransitDesk.Web.Extensions;
using TransitDesk.Web.ViewModels;

namespace TransitDesk.Web.Controllers
{
    public class VehiclesController : Controller
    {
        // Tymczasowa baza danych w pamięci (statyczna lista na potrzeby zadania)
        private static readonly List<VehicleViewModel> _vehicles = new();

        // Strona główna listy pojazdów (obsługa odczytu TempData po PRG)
        public IActionResult Index()
        {
            return View(_vehicles);
        }

        // Formularz dodawania pojazdu (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Obsługa wysłania formularza (POST) z uwzględnieniem wzorca PRG
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VehicleViewModel model)
        {
            // 1. Jeśli walidacja modelu nie przeszła, natychmiast zwracamy ten sam widok z błędami
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 2. Zapis do pamięci (symulacja)
            _vehicles.Add(model);

            // 3. Ustawienie komunikatu w TempData i przekierowanie (Pattern PRG)[cite: 24]
            TempData["SuccessMessage"] = $"Pojazd o numerze bocznym {model.SideNumber} został pomyślnie dodany do rejestru!";
            return RedirectToAction(nameof(Index));
        }

        // Wyświetlanie wyłącznie monitorowanych pojazdów z sesji
        public IActionResult Watchlist()
        {
            const string sessionKey = "Watchlist";
            var watchedNumbers = HttpContext.Session.GetObject<List<int>>(sessionKey) ?? new List<int>();

            // Wyfiltrowanie z ogólnej listy tylko tych pojazdów, których numery są w sesji
            var watchedVehicles = _vehicles.Where(v => watchedNumbers.Contains(v.SideNumber)).ToList();

            return View(watchedVehicles);
        }

        [HttpPost]
        public IActionResult AddVehicleToWatchlist(int sideNumber)
        {
            const string sessionKey = "Watchlist";

            // Pobranie istniejącej listy z sesji lub utworzenie nowej
            var watchlist = HttpContext.Session.GetObject<List<int>>(sessionKey) ?? new List<int>();

            if (!watchlist.Contains(sideNumber))
            {
                watchlist.Add(sideNumber);
                HttpContext.Session.SetObject(sessionKey, watchlist);
            }

            TempData["SuccessMessage"] = $"Pojazd #{sideNumber} został dodany do listy monitorowanych.";
            return RedirectToAction(nameof(Index));
        }

        // Usuwanie pojazdu z listy monitorowanych w sesji
        [HttpPost]
        public IActionResult RemoveFromWatchlist(int sideNumber)
        {
            const string sessionKey = "Watchlist";
            var watchlist = HttpContext.Session.GetObject<List<int>>(sessionKey);

            if (watchlist != null)
            {
                watchlist.Remove(sideNumber);
                HttpContext.Session.SetObject(sessionKey, watchlist);
            }

            TempData["SuccessMessage"] = $"Pojazd #${sideNumber} został usunięty z listy monitorowanych.";
            return RedirectToAction(nameof(Watchlist));
        }

        // --- Poprzednie akcje z Części 2 ---
        public IActionResult DepotFleetList(string depotName, string? tractionType)
        {
            ViewBag.DepotName = depotName;
            ViewBag.TractionType = tractionType ?? "Wszystkie";
            return View();
        }

        [HttpGet("vehicle/{sideNumber:fleetSideNo}")]
        public IActionResult Details(int sideNumber)
        {
            ViewBag.SideNumber = sideNumber;
            return View();
        }
    }
}