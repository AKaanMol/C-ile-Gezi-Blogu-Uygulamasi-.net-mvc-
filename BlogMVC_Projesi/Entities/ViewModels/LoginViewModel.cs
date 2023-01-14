using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entities.ViewModels
{
	public class LoginViewModel
	{
		[DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} doldurulmalıdır."), StringLength(25, ErrorMessage = "{0} En fazla 25 karakter girilebilir.")]
		public string UserName { get; set; }
		[DisplayName("Şifre"), Required(ErrorMessage = "Şifre doldurulmalıdır."),
			DataType(DataType.Password), //şifrenın gorunmesını engeller *** gibi
			StringLength(25, ErrorMessage = "Şifre En fazla 25 karakter girilebilir.")]
		public string Password { get; set; }
		
	}
}