using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Note")]
    public class Note : BaseEntity
    {
        [Required,StringLength(50)]
        public string Title { get; set; }

        [Required, StringLength(2000)]
        public string Text { get; set; }

        
        public bool IsDraft { get; set; }
        public int LikeCount { get; set; }

        public int? CategoryId { get; set; }
        public int? EvernoteUserId { get; set; }

        public virtual Category Category { get; set; }
        public virtual EvernoteUser EvernoteUser { get; set; }
        public virtual List<Comment> Comment { get; set; }
        public virtual List<Liked> Liked { get; set; }
    }
}
