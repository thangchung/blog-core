using System;
using Microsoft.AspNetCore.Identity;

namespace BlogCore.AccessControlContext.Core.Domain
{
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// The user that associates with specific blog
        /// if the BlogId is null that mean he/she is an administrator of the blog 
        /// </summary>
        public Guid? BlogId { get; set; }
        public string ProfilePhotoPath { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Bio { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
    }
}
