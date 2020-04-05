using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Comment")]
    public class Comment : BaseEntity
    {
        [Required,StringLength(200)]
        public string Text { get; set; }

        public int? NoteId { get; set; }
        public int? EvernoteUserId { get; set; }

        public virtual Note Note { get; set; }
        public virtual EvernoteUser EvernoteUser { get; set; }
    }
}
