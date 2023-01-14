using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entities.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "{0} doldurulmalıdır."), StringLength(25, ErrorMessage = "{0} En fazla 50 karakter girilebilir.")]
		public String Name { get; set; }
		[Required(ErrorMessage = "{0} doldurulmalıdır."), StringLength(25, ErrorMessage = "{0} En fazla 50 karakter girilebilir.")]
		public String Surname { get; set; }

		[DisplayName("E-mail"), Required(ErrorMessage = "{0} doldurulmalıdır."),
			StringLength(50, ErrorMessage = "{0} En fazla 50 karakter girilebilir."), EmailAddress(ErrorMessage ="{0} alanı için geçerli bir e-mail giriniz.")]
		public string Email { get; set; }
		[DisplayName("UserName"), Required(ErrorMessage = "Kullanıcı Adı doldurulmalıdır."),
			DataType(DataType.Password), //şifrenın gorunmesını engeller *** gibi
			StringLength(25, ErrorMessage = "Kullanıcı Adı En fazla 25 karakter girilebilir.")]
		public string UserName { get; set; }
		[DisplayName("Şifre"), Required(ErrorMessage = "Şifre doldurulmalıdır."),
			DataType(DataType.Password), //şifrenın gorunmesını engeller *** gibi
			StringLength(25, ErrorMessage = "Şifre En fazla 25 karakter girilebilir.")]
		public string Password { get; set; }
		[DisplayName("Şifre"), Required(ErrorMessage = "Şifre alanı doldurulmalıdır."),
			DataType(DataType.Password), //şifrenın gorunmesını engeller *** gibi
			StringLength(25, ErrorMessage = "En fazla 25 karakter girilebilir."),Compare("Password", ErrorMessage ="Girilen şifreler eşleşmiyor.")]
		public string RePassword { get; set; }


	}
}