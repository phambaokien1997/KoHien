using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.IdentityServer.Data
{
    public class AppUser : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AutoId { get; set; }
        public int Status { get; set; }
        public bool Gender { get; set; }
        DateTime? BirthDay { get; set; }
        public int MemberType { get; set; }
    }
}
