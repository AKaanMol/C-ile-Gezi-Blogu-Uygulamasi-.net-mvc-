using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Likes")]
    public class Liked
    {
          [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity) ]// Id Primary key olacağında daha sağlıklı sonuc olması için ama key yazsak da yeterli olurdu. 
        public int Id { get; set; }


        public virtual Note Note { get; set; }
        public virtual BlogUser LikedUser { get; set; }
        
        
       

    }
}
