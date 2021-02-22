using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Annotorious_RazorPages_EFCore_Sample.Data.Models;

namespace Annotorious_RazorPages_EFCore_Sample.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SampleContext context)
        {
            context.Database.EnsureCreated();

            // Look for any panoramas.
            if (context.Panoramas.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Panorama[]
            {
                new Panorama{ PanoramaId = new Guid("9e870062-a1d6-42eb-a1c2-21f8e1d2d295"), Name="Lungern"},
                new Panorama{ PanoramaId = new Guid("ca72e0a4-a10c-4a13-8780-cc280a5ea538"), Name="Lutry port"},
                new Panorama{ PanoramaId = new Guid("a7c67df6-77bf-49ca-a6ce-531f4158f0b2"), Name="Lutry vineyards"},
            };

            context.Panoramas.AddRange(students);
            context.SaveChanges();

            var users = new User[]
            {
                new User{ UserId = new Guid("60c5169b-0120-46dc-8050-ed3d17cf9ade"), UserName="gerardo.lijs", FirstName="Gerardo", LastName="Lijs", DisplayName="Gerardo Lijs"},
            };
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
