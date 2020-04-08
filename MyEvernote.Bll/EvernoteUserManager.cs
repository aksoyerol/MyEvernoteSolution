using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Bll.Repository;
using MyEvernote.Common.Helpers;
using MyEvernote.Entities;
using MyEvernote.Entities.ValueObjects;

namespace MyEvernote.Bll
{
    public class EvernoteUserManager
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();



        public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel model)
        {
            EvernoteUser user = repo_user.Find(x => x.UserName == model.UserName || x.Email == model.Email);
            BusinessLayerResult<EvernoteUser> businessLayerResult = new BusinessLayerResult<EvernoteUser>();

            if (user != null)
            {
                if (user.UserName == model.UserName)
                {
                    businessLayerResult.Errors.Add("Kullanıcı adı kayıtlı !");
                }

                if (user.Email == model.Email)
                {
                    businessLayerResult.Errors.Add("Bu mail adresi kayıtlı !");
                }

                if (model.Password != model.RePassword)
                {
                    businessLayerResult.Errors.Add("Parolalar eşleşmiyor !");
                }

            }
            else
            {
                Guid guid = new Guid();
                guid = Guid.NewGuid();
                int dbResult = repo_user.Insert(new EvernoteUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    IsActive = false,
                    ActivateGuid = guid,
                    IsAdmin = false,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUserName = "system",
                    Name = "",
                    Surname = "",
                   
                });

                if (dbResult > 0)
                {

                    //BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
                    BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
                    res.Result = repo_user.ListQueryable().FirstOrDefault(x => x.UserName == model.UserName && x.Email == model.Email);

                    // string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"http://localhost:60660/Home/UserActivate/{res.Result.ActivateGuid}/";
                    string body =
                        $"Merhaba sitemize hoşgeldiniz. Linke tıklayarak üyeliğinizi aktifleştirebilirsiniz. <a class='btn btn-primary' target='_blank' href='{activateUri}'>Tıklayınız</a>";
                    MailHelper.SendMail(body, res.Result.Email, "MyEvernote Üyelik Aktifleştirme");

                    //aktivasyon maili kullanılacak
                    //businessLayerResult.Result.ActivateGuid
                }
            }
            return businessLayerResult;
        }

        public BusinessLayerResult<EvernoteUser> LoginUser(LoginViewModel model)
        {
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();
            layerResult.Result = repo_user.Find(x => x.UserName == model.UserName && x.Password == model.Password);

            if (layerResult.Result != null)
            {
                if (!layerResult.Result.IsActive)
                {
                    layerResult.Errors.Add("Kullanıcı aktifleştirilmemiştir. Lütfen e-posta hesabınıza gelen bağlantıyı doğrulayınız.");
                }
            }
            else
            {
                layerResult.Errors.Add("Kullanıcı adı veya şifre eşleşmiyor !");
            }

            return layerResult;
        }


        public BusinessLayerResult<EvernoteUser> ActivateUser(Guid id)
        {
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
            res.Result = repo_user.Find(x => x.ActivateGuid == id);

            if (res.Result!=null)
            {
                if (res.Result.IsActive)
                {
                    res.Errors.Add("Bu kullanıcı zaten aktif edilmiş !");
                    return res;
                }

                res.Result.IsActive = true;
                repo_user.Update(res.Result);
            }
            else
            {
                res.Errors.Add("Bu kullanıcı zaten aktif edilmiş ! ");
            }

            return res;
        }
    }
}
