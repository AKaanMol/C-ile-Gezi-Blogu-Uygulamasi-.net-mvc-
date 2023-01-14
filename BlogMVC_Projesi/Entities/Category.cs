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
    [Table("Categories")] //table için system.componentmodel eklendi
    public class Category :BaseEntity
    {
        
        [Required, DisplayName("Kategori Adı"), StringLength(50)]
        public string Title { get; set; }
        [StringLength(200), DisplayName("Açıklama")]
        public string Description { get; set; }
       
        //İLİŞKİLİ ALANLARI TANIMLAMA
        public virtual List<Note> Notes { get; set;}

        //fakedata olustururken alınan hatadan dolayı eklendi
		public Category()
		{
            Notes = new List<Note>();
		}

    }
}
