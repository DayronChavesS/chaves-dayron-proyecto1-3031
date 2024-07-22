using System.ComponentModel.DataAnnotations;

namespace chaves_dayron_proyecto1_3031.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Complete este campo.")]
        [StringLength(50, ErrorMessage = "El nombre es demasiado extenso. (50 Letras Max.)")]
        [RegularExpression(@"^[a-zA-Z\sÑñÀ-ÿ]*$", ErrorMessage = "Se introdujo un caracter no permitido.")]
        [Display(Name = "Nombre completo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Complete este campo.")]
        [StringLength(50, ErrorMessage = "El correo es demasiado extenso. (50 Letras Max.)")]
        [EmailAddress(ErrorMessage = "El formato ingresado no es correcto, intentelo de nuevo.")]
        [Display(Name = "Correo electronico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Complete este campo.")]
        [StringLength(50, ErrorMessage = "La contraseña es demasiada extensa. (50 Letras Max.)")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}