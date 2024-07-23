using System.ComponentModel.DataAnnotations;

namespace chaves_dayron_proyecto1_3031.Models
{
    public class Preferences
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Complete este campo.")]
        [StringLength(3, ErrorMessage = "El nombre es demasiado extenso. (3 Letras Max.)")]
        [Display(Name = "Aeropuerto preferido: ")]
        public string Origin { get; set; }

        [Required(ErrorMessage = "Complete este campo.")]
        [StringLength(3, ErrorMessage = "El nombre es demasiado extenso. (3 Letras Max.)")]
        [Display(Name = "Destino: ")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Complete este campo.")]
        [StringLength(10, ErrorMessage = "La fecha es demasiada extensa. (10 Letras Max.)")]
        [Display(Name = "Fecha de salida: ")]
        public string DepartureDate { get; set; }

        [StringLength(10, ErrorMessage = "La fecha es demasiada extensa. (10 Letras Max.)")]
        [Display(Name = "Fecha de regreso: ")]
        public string ReturnDate { get; set; }

        [Required(ErrorMessage = "Complete este campo.")]
        [Range(1, 9, ErrorMessage = "Rango admitido: 1 - 9.")]
        [Display(Name = "Numero de adultos: ")]
        public int Adults { get; set; }

        [Required(ErrorMessage = "Complete este campo.")]
        [Range(0, 8, ErrorMessage = "Rango admitido: 0 - 8.")]
        [Display(Name = "Numero de niños: ")]
        public int Children { get; set; }

        [Range(0, 8, ErrorMessage = "Rango admitido: 0 - 8.")]
        [Display(Name = "Numero de bebes: ")]
        public int Infants { get; set; }

        [Display(Name = "Clase: ")]
        public string TravelClass { get; set; }

        [Display(Name = "¿Listar solo vuelos directos?: ")]
        public bool NonStop { get; set; }

        [Required(ErrorMessage = "Complete este campo.")]
        [Range(0, 2147483647, ErrorMessage = "Rango admitido: 0 - 2147483647.")]
        [Display(Name = "Precio maximo: ")]
        public int MaxPrice { get; set; }
    }
}