using System.ComponentModel.DataAnnotations;

namespace MyTurnstileApp.Web.Models
{
    public class SubmissionViewModel
    {
        public SubmissionViewModel()
        {
            UserName = "Usuario1";
            Email = "correo@correo.com";
        }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        public string TurnstileResponse { get; set; }

        public string RecaptchaToken { get; set; }
    }
}
