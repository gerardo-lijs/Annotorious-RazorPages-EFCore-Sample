using System;
using System.Collections.Generic;

#nullable disable

namespace Annotorious_RazorPages_EFCore_Sample.Data.Models
{
    public partial class User
    {
        public User()
        {
            PanoramaAnnotationItems = new HashSet<PanoramaAnnotationItem>();
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserPrincipalName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<PanoramaAnnotationItem> PanoramaAnnotationItems { get; set; }
    }
}