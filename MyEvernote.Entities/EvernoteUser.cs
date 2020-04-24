using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("EvernoteUser")]
    public class EvernoteUser : BaseEntity 
    {
        [DisplayName("İsim"),StringLength(30)]
        public string Name { get; set; }

        [DisplayName("Soyisim"), StringLength(30)]
        public string Surname { get; set; }

        [DisplayName("Kullanıcı Adı"), Required, StringLength(30)]
        public string UserName { get; set; }

        [DisplayName("E-Mail"), Required, StringLength(30)]
        public string Email { get; set; }

        [DisplayName("Şifre"), Required, StringLength(30)]
        public string Password { get; set; }

        [StringLength(100),ScaffoldColumn(false)]
        public string ProfileImageFile { get; set; }

        [Required,ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; }

        [DisplayName("Aktif?")]
        public bool IsActive { get; set; }

        [DisplayName("Admin?")]
        public bool IsAdmin { get; set; }

        public virtual List<Note> Note { get; set; }
        public virtual List<Comment> Comment { get; set; }
        public virtual List<Liked> Liked { get; set; }

    }
}
