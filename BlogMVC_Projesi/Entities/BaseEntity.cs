using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity) ]// Id Primary key olacağında daha sağlıklı sonuc olması için ama key yazsak da yeterli olurdu. 
        public int Id { get; set; }
        [DisplayName("Oluşturulma Tarihi"), ScaffoldColumn(false)]
         public DateTime CreatedDate { get; set; }
        [DisplayName("Güncellenme Tarihi"), ScaffoldColumn(false)]
        public DateTime ModifiedDate { get; set; }
        [Required, StringLength(30), DisplayName("Güncelleyen Kullanıcı")]
        public string ModifiedUserName { get; set; }
    }
}
