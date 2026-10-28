using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TransitDesk.Web.ViewModels
{
    public class IncidentReportViewModel
    {
        [Display(Name = "Numer boczny pojazdu")]
        [Required(ErrorMessage = "Numer boczny pojazdu jest wymagany.")]
        [Remote(action: "VerifyVehicleAvailable", controller: "Incidents", ErrorMessage = "Pojazd o podanym numerze nie istnieje lub jest wycofany.")]
        public int VehicleSideNumber { get; set; }

        [Display(Name = "Numer linii")]
        [Required(ErrorMessage = "Numer linii jest wymagany.")]
        [RegularExpression(@"^[0-9A-Za-z]{1,4}$", ErrorMessage = "Numer linii musi mieć od 1 do 4 znaków alfanumerycznych.")]
        public string LineNumber { get; set; } = string.Empty;

        [Display(Name = "Opis incydentu")]
        [Required(ErrorMessage = "Opis jest wymagany.")]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Opis musi zawierać od 20 do 500 znaków.")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Data i czas zdarzenia")]
        [Required(ErrorMessage = "Data i czas są wymagane.")]
        [DataType(DataType.DateTime)]
        public DateTime OccurredAt { get; set; } = DateTime.Now;

        public string? AttachmentPath { get; set; }
    }
}