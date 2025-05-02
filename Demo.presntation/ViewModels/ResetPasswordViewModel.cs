using System.ComponentModel.DataAnnotations;

namespace Demo.presntation.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password Is Required")]
        public string PassWord { get; set; }


        [DataType(DataType.Password)]
        [Compare(nameof(PassWord))]
        public string ConfrimPassword { get; set; }

    }
}
