using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tahri_Company.Models
{


    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Le champs adresse Email est requis")]
        [Display(Name = "Adresse Email")]
        [EmailAddress(ErrorMessage = "L'adresse Email n'est pas valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le champs mot de passe est requis")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string RegisterPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("RegisterPassword", ErrorMessage = "Les champs mot de passe et sa confirmation ne sont pas identiques.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date de naissance")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Numéro de téléphone personnel")]
        public string TelPerso { get; set; }

        [Required]
        [Display(Name = "Adresse")]
        public string Adresse { get; set; }

    }

    public class Commercial
    {
        public Commercial() { }

        // Allow Initialization with an instance of ApplicationUser:
        public Commercial(ApplicationUser user)
        {
            Id = user.Id;
            Email = user.Email;
            Nom = user.Nom;
            Prenom = user.Prenom;
            BirthDate = user.BirthDate;
            TelPerso = user.TelPerso;
            Adresse = user.Adresse;

        }

       public string Id { get; set; }

        [Required(ErrorMessage = "Le champs adresse Email est requis")]
        [Display(Name = "Adresse Email")]
        [EmailAddress(ErrorMessage = "L'adresse Email n'est pas valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le champs mot de passe est requis")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string RegisterPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("RegisterPassword", ErrorMessage = "Les champs mot de passe et sa confirmation ne sont pas identiques.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date de naissance")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Numéro de téléphone personnel")]
        public string TelPerso { get; set; }

        [Required]
        [Display(Name = "Adresse")]
        public string Adresse { get; set; }

    }

    public class Magasinier
    {
        public Magasinier() { }

        // Allow Initialization with an instance of ApplicationUser:
        public Magasinier(ApplicationUser user)
        {
            Id = user.Id;
            Email = user.Email;
            Nom = user.Nom;
            Prenom = user.Prenom;
            BirthDate = user.BirthDate;
            TelPerso = user.TelPerso;
            Adresse = user.Adresse;

        }

        public string Id { get; set; }

        [Required(ErrorMessage = "Le champs adresse Email est requis")]
        [Display(Name = "Adresse Email")]
        [EmailAddress(ErrorMessage = "L'adresse Email n'est pas valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le champs mot de passe est requis")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string RegisterPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("RegisterPassword", ErrorMessage = "Les champs mot de passe et sa confirmation ne sont pas identiques.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date de naissance")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Numéro de téléphone personnel")]
        public string TelPerso { get; set; }

        [Required]
        [Display(Name = "Adresse")]
        public string Adresse { get; set; }

    }

    public class Client
    {
        public Client() { }

        // Allow Initialization with an instance of ApplicationUser:
        public Client(ApplicationUser user)
        {
            Id = user.Id;
            Email = user.Email;
            Nom = user.Nom;
            Prenom = user.Prenom;
            BirthDate = user.BirthDate;
            TelPerso = user.TelPerso;
            Adresse = user.Adresse;
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "Le champs adresse Email est requis")]
        [Display(Name = "Adresse Email")]
        [EmailAddress(ErrorMessage = "L'adresse Email n'est pas valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le champs mot de passe est requis")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string RegisterPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("RegisterPassword", ErrorMessage = "Les champs mot de passe et sa confirmation ne sont pas identiques.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date de naissance")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Numéro de téléphone personnel")]
        public string TelPerso { get; set; }

        [Required]
        [Display(Name = "Adresse")]
        public string Adresse { get; set; }


    }


    

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
