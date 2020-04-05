using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Bll.Repository;
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
                int dbResult = repo_user.Insert(new EvernoteUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    IsActive = false,
                    IsAdmin = false,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUserName = "system"

                });

                if (dbResult > 0)
                {
                    businessLayerResult.Result = repo_user.Find(x => x.Email == model.Email && x.UserName == model.Email);
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
    }
}
