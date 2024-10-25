using System.ComponentModel.DataAnnotations;

namespace Application.Web
{
    public class LoginViewModel
    {
        [Required]
        public string UserNameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
