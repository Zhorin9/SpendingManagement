using SpendingManagement.Domain.Abstract;
using SpendingManagement.Domain.Entities;
using SpendingManagement.WebUI.Infrastructure;
using SpendingManagement.WebUI.Models;
using SpendingManagement.WebUI.Models.Account;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SpendingManagement.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository userRepository;
        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegisterViewModel user)
        {
            bool status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                #region Email is allready exist
                var isExist = IsEmailExist(user.Email);
                if (!isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email jest już używany.");
                    return View(user);
                }
                #endregion

                User newUser = new Domain.Entities.User()
                {
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                    UserID = user.UserID,
                    UserLogin = user.UserLogin,
                    UserPassword = user.UserPassword,
                };

                #region Password Hashing
                newUser.UserPassword = Crypto.Hash(user.UserPassword);
                #endregion

                #region Save to database
                userRepository.SaveUser(newUser);
                message = "Rejestracja przebiegła pomyślnie. Dziękujemy za założenie konta.";
                status = true;
                #endregion
            }
            else
            {
                message = "Rejestracja przebiegła niepomyślnie.";
            }
            ViewBag.Message = message;
            ViewBag.Status = status;
            return View(user);
        }
        /*
        public VerifyAccount(string id)
        {
            bool status = false;



            return View();

        }
        */
    
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login)
        {
            string message = "";
            var v = userRepository.Users.Where(p => p.Email == login.Email).FirstOrDefault();
            if(v != null && login.UserPassword != null && login.Email != null)
            {
                if (string.Compare(Crypto.Hash(login.UserPassword), v.UserPassword) == 0)
                {
                    int timeout = login.RememberMe ? 525600 : 20; //525600 min
                    var ticket = new FormsAuthenticationTicket(login.Email, login.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Expense");
                }
                else
                {
                    message = "Wprowadzono błędne dane";
                }
            }
            else
            {
                message = "Wprowadzono błędne dane";
            }
            ViewBag.Message = message;
            return View();

        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }


        [Authorize]
        public ActionResult Details()
        {
            int currentUserID = userRepository.Users.Where(p => p.Email == User.Identity.Name).Select(p => p.UserID).First();
            var currentUser = userRepository.Users.Where(p => p.UserID == currentUserID).First();
            RegisterViewModel currentModel = new RegisterViewModel()
            {
                DateOfBirth = currentUser.DateOfBirth,
                Email = currentUser.Email,
                UserLogin = currentUser.UserLogin,
            };
            return View(currentModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteAccount()
        {
            return View();
        }
        public bool IsLoginExist(string login)
        {
            var isAvailable = userRepository.Users.Where(p => p.UserLogin == login).FirstOrDefault();
            return isAvailable == null ? true : false;
        }
        public bool IsEmailExist(string email)
        {
            var v = userRepository.Users.Where(p => p.Email == email).FirstOrDefault();
            return v == null ? true : false;
        }


    }
}