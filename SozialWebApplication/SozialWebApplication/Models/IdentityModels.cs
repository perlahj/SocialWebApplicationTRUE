using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SozialWebApplication.Models;
using SozialWebApplication.Models.Entities;

namespace SozialWebApplication.Models
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

		public string FullName { get; set; }
		public string LineOfStudy { get; set; }
        public string Email { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<FollowerConnection> FollowerConnections { get; set; }
		public DbSet<MatchConnection> MatchConnections { get; set; }
		public DbSet<GroupConnection> GroupConnections { get; set; }
		public DbSet<PostConnection> PostConnections { get; set; }
		public DbSet<CommentConnection> CommentConnections { get; set; }

		public ApplicationDbContext()
			: base("DefaultConnection")
		{
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
    }
}