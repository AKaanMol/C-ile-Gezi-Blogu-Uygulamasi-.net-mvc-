using Blog_DataAccessLayer.EntityFrameworkSQL;
using Entities;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UygulamaHelperMaili.Models;

namespace Blog_BusinessLayer
{
    public class BlogUserManager : BaseManager<BlogUser>
    {

        public BusinessLayerResult<BlogUser> RegisterUser(RegisterViewModel model)
        {
            BlogUser user = Find(x => x.Username == model.UserName || x.Email == model.Email);

            BusinessLayerResult<BlogUser> layerResult = new BusinessLayerResult<BlogUser>();

            if (user != null)
            {
                if (user.Username == model.UserName)
                {
                    layerResult.Erorrs.Add("Bu kullanıcı adı müsait değildir!!!");

                }
                if (user.Email == model.Email)
                {
                    layerResult.Erorrs.Add("Bu E-posta adresi müsait değildir!!!");
                }

            }
            else
            {

                int result = base.Insert(new BlogUser
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    UserProfileImage = "User-Profile.jfif",
                    Username = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    IsActive = false,
                    IsAdmin = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedUserName = model.UserName,
                    ActivateGuid = Guid.NewGuid()
                });
                if (result > 0)
                {
                    layerResult.Result = Find(x => x.Username == model.UserName && x.Email == model.Email);

                    string siteUrl = ConfigHelper.Get<string>("SiteRootUrl");
                    string activateUrl = $"{siteUrl}/Home/UserActivate/{layerResult.Result.ActivateGuid}";
                    string messageBody = $"merhaba hesap aktig olması için <a href='{activateUrl}' target='_blank'> tıklayınız </a>";
                    string subject = "Blog Hesabı Aktivasyon";
                    MailHelper.SendMail(messageBody, layerResult.Result.Email, subject);


                }

            }

            return layerResult;
        }

        public BusinessLayerResult<BlogUser> GetUserById(int id)
        {
            BusinessLayerResult<BlogUser> blResult = new BusinessLayerResult<BlogUser>();
            BlogUser user = Find(x => x.Id == id);
            if (user == null)
            {
                blResult.Erorrs.Add("Kullanıcı bulunamadı");
            }
            else
            {
                blResult.Result = user;
            }
            return blResult;
        }

        public BusinessLayerResult<BlogUser> LoginUser(LoginViewModel model)
        {
            BusinessLayerResult<BlogUser> blResult = new BusinessLayerResult<BlogUser>();
            blResult.Result = Find(x => x.Username == model.UserName && x.Password == model.Password);

            if (blResult.Result != null)
            {

                if (!blResult.Result.IsActive)
                {
                    blResult.Erorrs.Add("Hesabınız aktif değil lütfen e-postanızı kontrol ediniz.");
                }

            }
            else
            {
                blResult.Erorrs.Add("Kullanıcı adı ya da şifreniz hatalı.");
            }
            return blResult;
        }

        public BusinessLayerResult<BlogUser> UserActivate(Guid id)
        {
            BusinessLayerResult<BlogUser> blResult = new BusinessLayerResult<BlogUser>();
            blResult.Result = Find(x => x.ActivateGuid == id);

            if (blResult.Result != null)
            {
                if (blResult.Result.IsActive)
                {
                    blResult.Erorrs.Add("Kullanıcı zaten aktif edilmiştir.");
                }
                else
                {
                    blResult.Result.IsActive = true;
                    Update(blResult.Result);
                }
            }
            else
            {
                blResult.Erorrs.Add("Aktifleştirilecek kullanıcı bulunamadı.");
            }
            return blResult;
        }

        public BusinessLayerResult<BlogUser> DeleteUser(int id)
        {
            BusinessLayerResult<BlogUser> blResult = new BusinessLayerResult<BlogUser>();

            BlogUser user = Find(x => x.Id == id);

            if (user != null)
            {






                if (Delete(user) == 0)
                {
                    blResult.Erorrs.Add("Kullanıcı silinemedi.");
                    return blResult;
                }
            }

            else
            {
                blResult.Erorrs.Add("Kullanıcı bulunamadı");

            }
            return blResult;
        }

        public BusinessLayerResult<BlogUser> UpdateProfile(BlogUser userdata)
        {
            BusinessLayerResult<BlogUser> blResult = new BusinessLayerResult<BlogUser>();

            BlogUser userDb = Find(x => x.Id != userdata.Id && (x.Email == userdata.Email || x.Username == userdata.Username));
            if (userDb != null && userDb.Id != userdata.Id)
            {
                if (userDb.Username == userdata.Username)
                {
                    blResult.Erorrs.Add("Girdiğiniz kullanıcı adı başka bir üyemiz tarafından kullanılmaktadır. Lütfen farklı kullanıcı adı girin.");
                }
                if (userDb.Email == userdata.Email)
                {
                    blResult.Erorrs.Add("Girdiğiniz E-mail başka bir üyemiz tarafından kullanılmaktadır. Lütfen farklı E-mail girin.");
                }
                return blResult;
            }
            blResult.Result = Find(x => x.Id == userdata.Id);
            blResult.Result.Name = userdata.Name;
            blResult.Result.Surname = userdata.Surname;
            blResult.Result.Email = userdata.Email;
            blResult.Result.Username = userdata.Username;
            blResult.Result.Password = userdata.Password;

            if (string.IsNullOrEmpty(userdata.UserProfileImage) == false)
            {
                blResult.Result.UserProfileImage = userdata.UserProfileImage;
            }
            if (base.Update(blResult.Result) == 0)
            {
                blResult.Erorrs.Add("Profil güncellenemedi.");
            }

            return blResult;
        }
        public new BusinessLayerResult<BlogUser> Insert(BlogUser data)
        {
            BlogUser user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<BlogUser> layerResult = new BusinessLayerResult<BlogUser>();
            layerResult.Result = data;
            if (user != null)
            {
                if (user.Email == data.Email)
                {
                    layerResult.Erorrs.Add("E-posta adresi kayıtlı.");
                }
                if (user.Username == data.Username)
                {
                    layerResult.Erorrs.Add("Kullanıcı Adı kayıtlı.");
                }

            }
            else
            {
                layerResult.Result.UserProfileImage = "user-profile.jpeg";
                layerResult.Result.ActivateGuid = Guid.NewGuid();
                if (base.Insert(layerResult.Result) == 0)
                {
                    layerResult.Erorrs.Add("Yeni üye kaydedilirken bir hata oluştu.");
                }
            }
            return layerResult;
        }

        public new BusinessLayerResult<BlogUser> Update(BlogUser data)
        {
            BusinessLayerResult<BlogUser> layerResult = new BusinessLayerResult<BlogUser>();
            BlogUser dbUser = Find(x => x.Id != data.Id && (x.Email == data.Email || x.Username == data.Username));
            layerResult.Result = data;
            if (dbUser != null && dbUser.Id != data.Id)
            {
                if (dbUser.Username == data.Username)
                {
                    layerResult.Erorrs.Add("Girdiğiniz kullanıcı başka bir üyemiz tarafından kullanılıyor. Lütfen farklı bir kullanıcı adı girin.");
                }
                if (dbUser.Email == data.Email)
                {
                    layerResult.Erorrs.Add("Girdiğiniz E-Posta başka bir üyemiz tarafından kullanılıyor. Lütfen farklı bir E-Posta girin.");
                }
                return layerResult;

            }
            layerResult.Result = Find(x => x.Id == data.Id);
            layerResult.Result.Email = data.Email;
            layerResult.Result.Name = data.Name;
            layerResult.Result.Surname = data.Surname;
            layerResult.Result.Password = data.Password;
            layerResult.Result.Username = data.Username;
            layerResult.Result.IsActive = data.IsActive;
            layerResult.Result.IsAdmin = data.IsAdmin;

            if (base.Update(layerResult.Result) == 0)
            {
                layerResult.Erorrs.Add("Profil Güncellenirken Bir Hata Oluştu.");
            }

            return layerResult;
        }

    }
}
