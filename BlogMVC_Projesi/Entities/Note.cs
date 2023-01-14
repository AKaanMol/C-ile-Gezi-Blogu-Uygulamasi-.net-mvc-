using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Notes")]
    public class Note :BaseEntity
    {
        [Required, StringLength(50)]
        public string Title { get; set; }
        [Required, StringLength(2000)]
        public string Text { get; set; }
        public bool IsDraft { get; set; }
        public int LikeCount { get; set; } //int yanına ? koyarsak boş geçilebilir anlamı taşır. int özelindedir
        public int CategoryId { get; set; }
        


        //ilişki tanımlaması
        public virtual Category Category { get; set; }
        public virtual BlogUser Owner { get; set; }
        public virtual List<Comment> Comments { get; set; } //birden fazla yorum veya beğeni olacağı için list ile tanımlama yapıyoruz
        public virtual List<Liked> Likes { get; set; }//birden fazla yorum veya beğeni olacağı için list ile tanımlama yapıyoruz

        //fakedata olustururken alınan hatadan dolayı eklendi
        public Note()
		{
          Comments = new List<Comment>();
          Likes = new List<Liked>();
            
        }


    }
}
