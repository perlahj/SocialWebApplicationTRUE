using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SozialWebApplication.Models;
using System.Data.Entity;


namespace SozialWebApplication.Models.Initialize
{
    public class GroupInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var groups = new List<Group>
            {
                new Group
                {
                    GroupName = "Tvíund"
                },
                new Group
                {
                    GroupName = "Vefforritun"
                },
            };
            // Asta: s => groups.Add(s) / context.Group.Add(s)
            groups.ForEach(s => groups.Add(s));
            context.SaveChanges();
        }
    }
}