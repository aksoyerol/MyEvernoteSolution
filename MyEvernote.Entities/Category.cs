using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Entities;

namespace MyEvernote.Entities
{
    [Table(("Category"))]
    public class Category : BaseEntity
    {
        [Required,StringLength(50)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual List<Note> Note { get; set; }

    }
}
