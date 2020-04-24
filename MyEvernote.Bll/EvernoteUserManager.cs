using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Bll.Repository;
using MyEvernote.Common.Helpers;
using MyEvernote.Entities;
using MyEvernote.Entities.ValueObjects;

namespace MyEvernote.Bll
{
    public class EvernoteUserManager : IRepository<EvernoteUser>
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
                    ProfileImageFile = "/images/nullimage.jpg",
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

            if (res.Result != null)
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

        public BusinessLayerResult<EvernoteUser> GetUserById(int id)
        {
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
            res.Result = repo_user.Find(x => x.Id == id);

            if (res.Result == null)
            {
                res.Errors.Add("Kullanıcı bulunamadı !");

            }

            return res;
        }

        public BusinessLayerResult<EvernoteUser> UpdateProfile(EvernoteUser data)
        {
            EvernoteUser dbUser = repo_user.Find(x =>(x.UserName == data.UserName || x.Email == data.Email));
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();

            if (dbUser != null && data.Id != dbUser.Id)
            {
                if (dbUser.UserName == data.UserName)
                {
                    res.Errors.Add("Bu kullanıcı adı kayıtlı !");
                }

                if (dbUser.Email == data.Email)
                {
                    res.Errors.Add("Bu mail adresi kayıtlı !");
                }
                return res;
            }

            res.Result = repo_user.Find(x => x.Id == data.Id);
            if (res.Result != null)
            {
                res.Result.Email = data.Email;
                res.Result.UserName = data.UserName;
                res.Result.Name = data.Name;
                res.Result.Surname = data.Surname;
                res.Result.Password = data.Password;
                res.Result.ModifiedOn = DateTime.Now;
                res.Result.ModifiedUserName = data.UserName;

                if (String.IsNullOrEmpty(data.ProfileImageFile) == false)
                {
                    res.Result.ProfileImageFile = data.ProfileImageFile;
                }

                if (repo_user.Update(res.Result) == 0)
                {
                    res.Errors.Add("Profil güncellenemedi ! ");
                }
            }

            return res;
        }

        public BusinessLayerResult<EvernoteUser> UserInsert(EvernoteUser data)
        {
            EvernoteUser user = repo_user.Find(x => x.UserName == data.UserName || x.Email == data.Email);
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
            res.Result = data;

            if (user != null)
            {
                if (user.UserName == data.UserName)
                {
                    res.Errors.Add("Kullanıcı adı kayıtlı !");
                }

                if (user.Email == data.Email)
                {
                    res.Errors.Add("Bu email kayıtlı !");
                }
            }
            else
            {
                res.Result.ActivateGuid = Guid.NewGuid();
                res.Result.ProfileImageFile = "nullimage.jpg";
                res.Result.ModifiedOn = DateTime.Now;
                res.Result.CreatedOn = DateTime.Now;
                res.Result.ModifiedUserName = "system";

                int dbResult = repo_user.Insert(res.Result);

                if (dbResult == 0)
                {
                    res.Errors.Add("Kullanıcı kayıt edilemedi !");
                }
            }

            return res;
        }

        public BusinessLayerResult<EvernoteUser> UserUpdate(EvernoteUser data)
        {
            //EvernoteUser dbUser = repo_user.Find(x => x.UserName == data.UserName || x.Email == data.Email);
            EvernoteUser dbUser = Find(x => x.Id != data.Id && (x.UserName == data.UserName || x.Email == data.Email));
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
            res.Result = dbUser;

            if (dbUser != null && data.Id != dbUser.Id)
            {
                if (dbUser.UserName == data.UserName)
                {
                    res.Errors.Add("Bu kullanıcı adı kayıtlı !");
                }

                if (dbUser.Email == data.Email)
                {
                    res.Errors.Add("Bu mail adresi kayıtlı !");
                }

                return res;
            }

            res.Result = repo_user.Find(x => x.Id == data.Id);
            if (res.Result != null)
            {
                res.Result.Email = data.Email;
                res.Result.UserName = data.UserName;
                res.Result.Name = data.Name;
                res.Result.Surname = data.Surname;
                res.Result.Password = data.Password;
                res.Result.ModifiedOn = DateTime.Now;
                res.Result.ModifiedUserName = data.UserName;
                res.Result.IsAdmin = data.IsAdmin;
                res.Result.IsActive = data.IsActive;

                if (repo_user.Update(res.Result) == 0)
                {
                    res.Errors.Add("Profil güncellenemedi ! ");
                }
            }

            return res;
        }

        public List<EvernoteUser> List()
        {
            return repo_user.List();
        }

        public IQueryable<EvernoteUser> List(Expression<Func<EvernoteUser, bool>> filter)
        {
            return repo_user.List(filter);
        }

        public EvernoteUser Find(Expression<Func<EvernoteUser, bool>> filter)
        {
            return repo_user.Find(filter);
        }

        public int Insert(EvernoteUser entity)
        {
            return repo_user.Insert(entity);
        }

        public int Update(EvernoteUser entity)
        {
            return repo_user.Update(entity);
        }

        public int Delete(EvernoteUser entity)
        {
            return repo_user.Delete(entity);
        }

        public int Save()
        {
            return repo_user.Save();
        }
    }
}
