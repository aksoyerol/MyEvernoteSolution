using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Entities;

namespace MyEvernote.Dal
{
    public class MyInitalizier : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            //EvernoteUser admin = new EvernoteUser()
            //{
            //    Name = "Erol",
            //    Surname = "Aksoy",
            //    Email = "erolaksoy98@gmail.com",
            //    ActivateGuid = new Guid(),
            //    IsActive = true,
            //    IsAdmin = true,
            //    UserName = "pcparticle",
            //    Password = "123456",
            //    CreatedOn = DateTime.Now,
            //    ModifiedOn = DateTime.Now.AddMinutes(5),
            //    ModifiedUserName = "pcparticle"

            //};

            //EvernoteUser standartUser = new EvernoteUser()
            //{
            //    Name = "Selin",
            //    Surname = "Güçlü",
            //    Email = "slngcl163@gmail.com",
            //    ActivateGuid = new Guid(),
            //    IsActive = true,
            //    IsAdmin = false,
            //    UserName = "slngcl163",
            //    Password = "123456",
            //    CreatedOn = DateTime.Now,
            //    ModifiedOn = DateTime.Now.AddMinutes(5),
            //    ModifiedUserName = "pcparticle"
            //};

            //context.EvernoteUser.Add(admin);
            //context.EvernoteUser.Add(standartUser);
            //context.SaveChanges();
        }

        public override void InitializeDatabase(DatabaseContext context)
        {
            base.InitializeDatabase(context);
        }
    }
}
