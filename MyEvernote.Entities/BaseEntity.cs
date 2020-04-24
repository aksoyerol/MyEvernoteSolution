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
    public class BaseEntity
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Oluşturma T."),Required]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Düzenleme T."), Required]
        public DateTime ModifiedOn { get; set; }

        [DisplayName("Değiştiren K."), Required,StringLength(30),ScaffoldColumn(false)]
        public string ModifiedUserName { get; set; }
    }
}
