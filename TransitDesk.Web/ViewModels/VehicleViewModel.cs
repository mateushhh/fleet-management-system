using System.ComponentModel.DataAnnotations;

namespace TransitDesk.Web.ViewModels
{
    public class VehicleViewModel
    {
        [Display(Name = "Numer boczny")]
        [Required(ErrorMessage = "Numer boczny jest wymagany.")]
        [Range(1000, 9999, ErrorMessage = "Numer boczny musi mieścić się w zakresie od 1000 do 9999.")]
        public int SideNumber { get; set; }

        [Display(Name = "Model pojazdu")]
        [Required(ErrorMessage = "Model pojazdu jest wymagany.")]
        [StringLength(50, ErrorMessage = "Nazwa modelu może mieć maksymalnie 50 znaków.")]
        public string ModelName { get; set; } = string.Empty;

        [Display(Name = "Nazwa zajezdni")]
        [Required(ErrorMessage = "Nazwa zajezdni jest wymagana.")]
        public string DepotName { get; set; } = string.Empty;

        [Display(Name = "Rok produkcji")]
        [Range(1900, 2030, ErrorMessage = "Podaj prawidłowy rok produkcji.")]
        public int ProductionYear { get; set; }

        [Display(Name = "Pojemność (liczba miejsc)")]
        [Range(1, 300, ErrorMessage = "Pojemność musi mieścić się w zakresie od 1 do 300.")]
        public int Capacity { get; set; }

        [Display(Name = "Pojazd niskopodłogowy")]
        public bool IsLowFloor { get; set; }

        [Display(Name = "Status serwisowy")]
        public ServiceStatus ServiceStatus { get; set; }
    }
}