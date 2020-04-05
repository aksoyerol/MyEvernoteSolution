using System;
using System.Collections.Generic;
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
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(30)]
        public string Surname { get; set; }

        [Required, StringLength(30)]
        public string UserName { get; set; }

        [Required, StringLength(30)]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        [Required]
        public Guid ActivateGuid { get; set; }

        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        public virtual List<Note> Note { get; set; }
        public virtual List<Comment> Comment { get; set; }
        public virtual List<Liked> Liked { get; set; }

    }
}
