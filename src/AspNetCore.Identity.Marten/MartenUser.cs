using System;
using System.Collections.Generic;

namespace AspNetCore.Identity.Marten
{
    public class MartenUser : MartenUser<Guid>
    {
        public MartenUser()
        {
            Id = Guid.NewGuid();
        }
    }

    public class MartenUser<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; }

        public IList<string> Roles { get; set; } = new List<string>();
        public IList<MartenUserClaim> Claims { get; } = new List<MartenUserClaim>();
        public IList<MartenUserLogin> Logins { get; } = new List<MartenUserLogin>();
    }
}