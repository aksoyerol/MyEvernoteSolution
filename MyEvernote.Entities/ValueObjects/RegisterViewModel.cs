using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyEvernote.Entities.ValueObjects
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "Zorunlu Alan ! "), StringLength(30, ErrorMessage = "30 Karakterden az kullanıcı adı girmelisiniz.")]
        public string UserName { get; set; }

        [DisplayName("E-Mail"), Required(ErrorMessage = "E-Mail alanının doldurulması zorunludur."), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "Şifre alanı boş bırakılamaz !"), DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"),
        Required(ErrorMessage = "Şifrenizi tekrar girmeniz gerek !"),
            DataType(DataType.Password),
            Compare("Password", ErrorMessage = "{0} ile {1} aynı olmak zorundadır !")]
        public string RePassword { get; set; }
    }
}