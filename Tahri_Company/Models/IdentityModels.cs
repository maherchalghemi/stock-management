using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Tahri_Company.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string Nom { get; set; }

        public string Prenom { get; set; }

        public System.DateTime BirthDate { get; set; }

        public string TelPerso { get; set; }

        public string JoinDate { get; set; }

        public string EmailLinkDate { get; set; }

        public string LastLoginDate { get; set; }

        public string Adresse { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Impression> Impressions { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Depot> Depots { get; set; }
        public DbSet<StockDepot> StockDepots { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<ItemCmd> ItemCmds { get; set; }
    }


    
}