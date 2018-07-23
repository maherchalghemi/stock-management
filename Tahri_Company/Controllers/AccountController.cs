using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Tahri_Company.Models;
using System.Data.SqlClient;
using System.Web.Hosting;
using System.IO;
using System.Configuration;


namespace Tahri_Company.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #region Variables
        //To assign username if user's account has not been confirmed
        public static string EConfUser { get; set; }
        //To get connectionstring from config file
        public static string connection = GetConnectionString("DefaultConnection");
        //Sql Command and parameter
        public static string command = null;
        public static string parameterName = null;
        //The method we're gonna use when we call our helper method
        public static string methodName = null;
        //The type of the token we want to generate
        string codeType = null;

        #endregion Variables

        //Redirection lors du Login selon le role( Admin => Espace Administrateur , Agent => Espace Agent ) 
        public ActionResult Profil_Redirection()
        {
            if (User.IsInRole("Administrateur"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (User.IsInRole("Commercial"))
            {
                return RedirectToAction("Index", "EspaceCommercial");
            }
            else if (User.IsInRole("Animateur"))
            {
                return RedirectToAction("Index", "EspaceAnimateur");
            }
            else if (User.IsInRole("Magasinier"))
            {
                return RedirectToAction("Index", "EspaceMagasiner");
            }
            else
            {
                return View();
            }
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var custEmailConf = EmailConfirmation(model.Email);

                ApplicationUser signedUser = UserManager.FindByEmail(model.Email);
                var result = await SignInManager.PasswordSignInAsync(signedUser.UserName, model.Password, model.RememberMe, shouldLockout: false);

                if (custEmailConf == false && result.ToString() == "Success" && User.IsInRole("Client"))
                { 
                    
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    EConfUser = model.Email;
                    return RedirectToAction("EmailConfirmationFailed", "Account");

                }
                else
                {
                    ViewBag.ReturnUrl = returnUrl;
                    if (ModelState.IsValid)
                    {
                        switch (result)
                        {
                            case SignInStatus.Success:
                                UpdateLastLoginDate(model.Email);
                                return RedirectToAction("Profil_Redirection", "Account");

                            case SignInStatus.LockedOut:
                                return View("Lockout");
                            case SignInStatus.RequiresVerification:
                                return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                            case SignInStatus.Failure:
                            default:
                                ModelState.AddModelError("", "Le nom d'utilisateur ou le mot de passe est incorrect!");
                                return View("Login");
                        }
                    }
                    // If we got this far, something failed, redisplay form
                    return View("Login");
                }
            }
            else
            {
                return View("Login");

            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model,string statut)
        {
            if (ModelState.IsValid)
            {
                var custEmail = FindEmail(model.Email);
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    Nom = model.Nom,
                    Prenom = model.Prenom,
                    BirthDate = model.BirthDate,
                    TelPerso = model.TelPerso,
                    Adresse = model.Adresse,
                    JoinDate = DateTime.Now.ToString(),
                    EmailLinkDate = DateTime.Now.ToString(),
                    LastLoginDate = DateTime.Now.ToString(),
                    UserName = model.Nom + " " + model.Prenom,         
                };
                 

                if (custEmail == null)
                {
                    var result = await UserManager.CreateAsync(user, model.RegisterPassword);
                    if (result.Succeeded)
                    {
                        if (statut == "Commercial")
                        {
                            UserManager.AddToRole(user.Id, "Commercial");
                            return RedirectToAction("CommercialsList", "Admin");
                        }
                        if (statut == "Magasinier")
                        {
                            UserManager.AddToRole(user.Id, "Magasinier");
                            return RedirectToAction("MagasiniersList", "Admin");
                        }
                        else
                        {
                            UserManager.AddToRole(user.Id, "Client");
                            // Send an email with this link
                            codeType = "EmailConfirmation";
                            await SendEmail("ConfirmEmail", "Account", user, model.Email, "WelcomeEmail", "Confirmer Votre Compte");
                            return RedirectToAction("ConfirmationEmailSent", "Account");
                        }
                    }
                    AddErrors(result);
                }
                else
                {
                    if (custEmail != null)
                    {
                        ModelState.AddModelError("", "L'adresse mail est déjà utilisée.");
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }




        #region ConfirmEmail

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, DateTime date, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("ConfirmationLinkExpired", "Account");
            }
            var emailConf = EmailConfirmationById(userId);
            if (emailConf == true)
            {
                return RedirectToAction("ConfirmationLinkUsed", "Account");
            }
            if (date != null)
            {
                if (date.AddMinutes(2880) < DateTime.Now)//2880 équivalant à deux jours
                {
                    return RedirectToAction("ConfirmationLinkExpired", "Account");
                }
                else
                {
                    var result = await UserManager.ConfirmEmailAsync(userId, code);
                    return View(result.Succeeded ? "ConfirmEmail" : "Error");
                }
            }
            else
            {
                return View("Error");
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendConfirmationMail()
        {
            string res = null;
            string connection = GetConnectionString("DefaultConnection");
            using (SqlConnection myConnection = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand("SELECT Email AS Email FROM AspNetUsers WHERE Email = @Email", myConnection))
            {
                cmd.Parameters.AddWithValue("@Email", EConfUser);
                myConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        if (reader.Read())
                        {
                            // To avoid unexpected bugs access columns by name.
                            res = reader["Email"].ToString();
                            var user = await UserManager.FindByEmailAsync(res);
                            UpdateEmailLinkDate(EConfUser);
                            codeType = "EmailConfirmation";
                            await SendEmail("ConfirmEmail", "Account", user, res, "WelcomeEmail", "Confirm your account");
                        }
                        myConnection.Close();
                    }
                }
            }
            return RedirectToAction("ConfirmationEmailSent", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult EmailConfirmationFailed()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ConfirmationEmailSent()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ConfirmationLinkExpired()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ConfirmationLinkUsed()
        {
            return View();
        }

        #endregion ConfirmEmail

        #region SendEmail

        public async Task SendEmail(string actionName, string controllerName, ApplicationUser user, string email, string emailTemplate, string emailSubject)
        {
            string code = null;
            if (codeType == "EmailConfirmation")
            {
                code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

            }
            else if (codeType == "ResetPassword")
            {
                code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            }

            //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            var callbackUrl = Url.Action(actionName, controllerName, new { userId = user.Id, date = DateTime.Now, code = code }, protocol: Request.Url.Scheme);
            var message = await EMailTemplate(emailTemplate);
            message = message.Replace("@ViewBag.Name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.Prenom));
            message = message.Replace("@ViewBag.Link", callbackUrl);
            await MessageServices.SendEmailAsync(email, emailSubject, message);
        }


        #endregion SendEmail


        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Db = new ApplicationDbContext();
                ApplicationUser currentUser = Db.Users.FirstOrDefault(x => x.Email == model.Email);
                var user = await UserManager.FindByNameAsync(currentUser.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }
                // Send an email with this link
                codeType = "ResetPassword";
                await SendEmail("ResetPassword", "Account", user, model.Email, "MotDePasse", "Réinitialisation du mot de passe");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }



        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }


        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        
   




        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        public static async Task<string> EMailTemplate(string template)
        {
            var templateFilePath = HostingEnvironment.MapPath("~/Content/templates/") + template + ".cshtml";
            StreamReader objstreamreaderfile = new StreamReader(templateFilePath);
            var body = await objstreamreaderfile.ReadToEndAsync();
            objstreamreaderfile.Close();
            return body;
        }

        //Method To get ConnectionString
        public static string GetConnectionString(string connection)
        {
            return ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        }

        //Method To check userName or email in our database and return them as string
        public static string ReturnString(string str)
        {
            string strOut = null;
            using (SqlConnection myConnection = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(command, myConnection))
            {
                cmd.Parameters.AddWithValue(parameterName, str);
                myConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            if (methodName == "FindEmail")
                            {
                                strOut = reader["Email"].ToString();
                            }

                            else if (methodName == "FindUserId")
                            {
                                strOut = reader["UserId"].ToString();
                            }
                        }
                        myConnection.Close();
                    }
                    return strOut;
                }
            }
        }

        public static string FindEmail(string email)
        {
            command = "SELECT Email AS Email FROM AspNetUsers WHERE Email = @Email";
            parameterName = "@Email";
            methodName = "FindEmail";
            return ReturnString(email);
        }



        public string FindUserId(string userprokey)
        {
            command = "SELECT UserId AS UserId FROM AspNetUserLogins WHERE ProviderKey = @ProviderKey";
            parameterName = "@ProviderKey";
            methodName = "FindUserId";
            return ReturnString(userprokey);
        }

        //Method to check if user's email is confirmed and return a boolean
        public bool ReturnBool(string str)
        {
            bool econfOut = false;
            string res = null;
            using (SqlConnection myConnection = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(command, myConnection))
            {
                cmd.Parameters.AddWithValue(parameterName, str);
                myConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            res = reader["EmailConfirmed"].ToString();
                            if (res == "False")
                            {
                                econfOut = false;
                            }
                            else
                            {
                                econfOut = true;
                            }
                        }
                        myConnection.Close();
                    }
                    return econfOut;
                }
            }
        }

        //In this method we pass Email as parameter to check our database
        public bool EmailConfirmation(string Email)
        {
            command = "SELECT EmailConfirmed AS EmailConfirmed FROM AspNetUsers WHERE Email = @Email";
            parameterName = "@Email";
            return ReturnBool(Email);
        }

        //In this method we pass userId as parameter to check our database
        public bool EmailConfirmationById(string userid)
        {
            command = "SELECT EmailConfirmed AS EmailConfirmed FROM AspNetUsers WHERE Id = @Id";
            parameterName = "@Id";
            return ReturnBool(userid);
        }

        //Update The database
        public static int UpdateDatabase(string Email)
        {
            using (SqlConnection myConnection = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(command, myConnection))
            {
                cmd.Parameters.AddWithValue(parameterName, Email);
                myConnection.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // When a user with an unconfirmed account resends the confirmation email this method will update EmailLinkDate to the current time and date
        public static int UpdateEmailLinkDate(string Email)
        {
            command = "UPDATE AspNetUsers SET EmailLinkDate = '" + DateTime.Now.ToString() + "' WHERE Email = @Email";
            parameterName = "@Email";
            return UpdateDatabase(Email);
        }

        //When a user with a confirmed account logs in this method will update LastLoginDate to the current time and date
        public static int UpdateLastLoginDate(string Email)
        {
            command = "UPDATE AspNetUsers SET LastLoginDate = '" + DateTime.Now.ToString() + "' WHERE Email = @Email";
            parameterName = "@Email";
            return UpdateDatabase(Email);
        }
    }
}