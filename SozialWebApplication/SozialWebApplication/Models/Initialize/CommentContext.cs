using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWebApplication.Models;

namespace ApplicationSozialWebApplication.Models.Initialize
{
    public class CommentsContext : DbContext
    {
        public CommentsContext() : base ("CommentsContext")
        {
        }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuileder)
        {
            modelBuileder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}