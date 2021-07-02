using System.ComponentModel.DataAnnotations;

namespace electronics.ViewModel
{
    public class AdminLogin
    {
        [EmailAddress]
        [Required(ErrorMessage = "Մուտքագրեք ձեր էլ․ հասցեն")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Մուտքագրեք գաղտնաբառը")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
