using System.ComponentModel.DataAnnotations;

namespace chaves_dayron_proyecto1_3031.Models
{
    public class Flight
    {

        //https://www.iata.org/en/publications/directories/code-search/
        [Display(Name = "Aeropuerto:")]
        public string departureLocation { get; set; }


        [Display(Name = "Dia de salida:")]
        public string departureAt { get; set; }


        [Display(Name = "Destino:")]
        public string arrivalLocation { get; set; }


        [Display(Name = "Dia de llegada:")]
        public string arrivalAt { get; set; }


        [Display(Name = "Clase:")]
        public string cabinType { get; set; }


        [Display(Name = "Campos:")]
        public int numberOfBookableSeats { get; set; }


        [Display(Name = "Retorna:")]
        public bool oneWay { get; set; }


        [Display(Name = "Precio:")]
        public double grandTotal { get; set; }
    }

}
