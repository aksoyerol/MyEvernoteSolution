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

        BusinessLayerResult<EvernoteUser> businessLayerResult = new BusinessLayerResult<EvernoteUser>();

        public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel model)
        {
            EvernoteUser user = repo_user.Find(x => x.UserName == model.UserName || x.Email == model.Email);
            
            if (user != null)
            {
                if (user.UserName == model.UserName)
                { businessLayerResult.Errors.Add("Kullanıcı adı kayıtlı !"); }

                if (user.Email == model.Email)
                {
                    businessLayerResult.Errors.Add("Bu mail adresi kayıtlı !");
                }
            }
            else
            {
                int dbResult = repo_user.Insert(new EvernoteUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    CreatedOn = DateTime.Now,
                    IsActive = false,
                    IsAdmin = false,
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
    }
}
