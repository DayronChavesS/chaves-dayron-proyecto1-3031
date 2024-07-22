using System.ComponentModel.DataAnnotations;

namespace chaves_dayron_proyecto1_3031.Models
{
    public class Reserve
    {
        [Display(Name = "Id:")]
        public int RsrvId { get; set; }

        public int UserId { get; set; }

        [Display(Name = "En el aeropuerto:")]
        public string Origin { get; set; }

        [Display(Name = "Con destino a:")]
        public string Destination { get; set; }

        [Display(Name = "El dia:")]
        public string DepartureDate { get; set; }

        [Display(Name = "Para llegar el:")]
        public string ArrivalDate { get; set; }

        [Display(Name = "Clase:")]
        public string Class { get; set; }

        [Display(Name = "Precio:")]
        public int Price { get; set; }
        public bool InCart { get; set; }

        public bool InHistory { get; set; }
    }
}