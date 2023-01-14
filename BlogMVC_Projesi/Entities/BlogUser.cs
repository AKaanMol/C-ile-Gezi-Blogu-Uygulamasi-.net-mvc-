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
    [Table("BlogUsers")]
    public class BlogUser:BaseEntity
    {
        [DisplayName("Adı"), StringLength(25)]
        public String Name { get; set; }
        [DisplayName("Soyadı"), StringLength(25)]
        public String Surname { get; set; }
		[StringLength(50), DisplayName("Profil Fotoğrafı"), ScaffoldColumn(false)]
		public string UserProfileImage { get; set; }
		[Required, StringLength(50),DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Kullanıcı Adı"), Required, StringLength(25)]
        public String Username { get; set; }
        [DisplayName("Şifre"), Required, StringLength(100)]
        public string Password { get; set; }
        [DisplayName("Aktif mi?")]
        public bool IsActive { get; set; }
        [DisplayName("Admin mi?")]
        public bool IsAdmin { get; set; }
        [ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; } //kullanıcı ıcın ozel 15 hanelı bır kımlık olusturur

        //çok seçenek varsa(bir kullanıcının birden fazla notu veya yorum veya like'ı olabileceği gibi)
        //List gelir ve isim tanımlaması çoğul yapılır
        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual  List<Liked> Likes { get; set; }

    }
}
