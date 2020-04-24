using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEvernote.Bll;
using MyEvernote.Entities;
using MyEvernote.Entities.ValueObjects;


namespace MyEvernote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            NoteManager nm = new NoteManager();
            //Test test = new Test();

            return View(nm.ListNote().OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }

            CategoryManager cm = new CategoryManager();
            Category category = cm.GetCategoryId(id.Value);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View("Index", category.Note);
        }

        public ActionResult MostLiked()
        {
            NoteManager nm = new NoteManager();
            return View("Index", nm.GetAllNotesQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            ViewBag.HataGoster = 1;
            if (ModelState.IsValid)
            {
                ViewBag.HataGoster = 0;
                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> res = eum.LoginUser(model);

                if (res.Errors.Count > 0)
                {
                    ViewBag.HataGoster = 1;

                    res.Errors.ForEach(x => ModelState.AddModelError("", x));

                    if (res.Errors.Contains("Kullanıcı aktifleştirilmemiştir. Lütfen e-posta hesabınıza gelen bağlantıyı doğrulayınız."))
                    {
                        ViewBag.SetLink = "http://akjshdkasdasd.com";
                    }

                    return View(model);
                }

                Session["login"] = res.Result;
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> res = eum.RegisterUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
                return RedirectToAction("RegisterOk");
            }

            return View(model);


        }


        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult UserActivate(Guid id)
        {
            EvernoteUserManager eum = new EvernoteUserManager();
            BusinessLayerResult<EvernoteUser> res = eum.ActivateUser(id);
            if (res.Errors.Count > 0)
            {
                TempData["errors"] = res.Errors;
                return RedirectToAction("UserActivateCancel", "Home");
            }

            return RedirectToAction("UserActivateOk", "Home");
        }

        public ActionResult UserActivateOk()
        {
            return View();

        }

        public ActionResult UserActivateCancel()
        {

            List<string> errorMessages = null;

            if (TempData["errors"] != null)
            {
                errorMessages = (List<string>)TempData["erros"];


            }

            return View(errorMessages);
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ShowProfile()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            EvernoteUser currentUser = Session["login"] as EvernoteUser;

            EvernoteUserManager eum = new EvernoteUserManager();
            BusinessLayerResult<EvernoteUser> res = eum.GetUserById(currentUser.Id);

            if (res.Errors.Count > 0)
            {
                //hata
            }

            return View(res.Result);
        }

        public ActionResult EditProfile()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            EvernoteUser currentUser = Session["login"] as EvernoteUser;
            EvernoteUserManager eum = new EvernoteUserManager();
            BusinessLayerResult<EvernoteUser> res = eum.GetUserById(currentUser.Id);

            if (res.Errors.Count > 0)
            {
                //hata
            }


            return View(res.Result);
        }

        [HttpPost]
        public ActionResult EditProfile(EvernoteUser user, HttpPostedFileBase profileImage)
        {
            ModelState.Remove("ModifiedUserName");
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");

            if (ModelState.IsValid)
            {
                if (profileImage != null && (profileImage.ContentType == "image/jpeg" ||
                                             profileImage.ContentType == "image/jpg" ||
                                             profileImage.ContentType == "image/png"))
                {


                    string fileName = $"user_{user.Id}_profileImage.{profileImage.ContentType.Split('/')[1]}";
                    //string dosyaYolu = Server.MapPath("~/images/");

                    //if (dosyaYolu.Contains(fileName))
                    //{

                    //}
                    profileImage.SaveAs(Server.MapPath($"~/images/{fileName}"));
                    user.ProfileImageFile = fileName;
                }


                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> res = eum.UpdateProfile(user);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(res.Result);
                }

                Session["login"] = res.Result;
            }
            return View(user);
        }


    }
}