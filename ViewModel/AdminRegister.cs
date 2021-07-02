using System.ComponentModel.DataAnnotations;

namespace electronics.ViewModel
{
    public class AdminRegister
    {
        [EmailAddress]
        [Required(ErrorMessage = "Մուտքագրեք ձեր Էլ․հասցեն")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Մուտքագրեք գաղտնաբառը")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Չի համապատասխանում գաղտնաբառին")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
