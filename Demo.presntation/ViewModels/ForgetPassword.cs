using System.ComponentModel.DataAnnotations;

namespace Demo.presntation.ViewModels
{
    public class ForgetPassword
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Email is Required")]
        public string Email { get; set; } = null;
    }
}
