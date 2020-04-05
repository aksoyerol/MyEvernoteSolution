using System.ComponentModel.DataAnnotations;

namespace MyEvernote.Entities.ValueObjects
{
    public class LoginViewModel
    {
        [Display(Name="Kullanıcı Adı"), Required(ErrorMessage = "Lütfen {0} giriniz.")]
        public string UserName { get; set; }

        [Display(Name = "Şifre"), Required(ErrorMessage = "Lütfen {0} alanını boş bırakmayınız."), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}