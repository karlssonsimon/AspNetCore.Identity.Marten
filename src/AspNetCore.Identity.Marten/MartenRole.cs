using System;
using System.Collections.Generic;

namespace AspNetCore.Identity.Marten
{
    public class MartenRole : MartenRole<Guid>
    {
        public MartenRole()
        {
            Id = Guid.NewGuid();
        }

        public MartenRole(string roleName) : base()
        {
            Name = roleName;
        }

        public override bool Equals(object obj)
        {
            if (obj is MartenRole other)
            {
                return other.Id == Id;
            }
            
            return false;
        }
    }

    public class MartenRole<TKey> : MartenRole<TKey, MartenRoleClaim>
        where TKey : IEquatable<TKey>
    {
    }

    public class MartenRole<TKey, TRoleClaim>
        where TKey : IEquatable<TKey>
        where TRoleClaim : MartenRoleClaim
    {
        public MartenRole() { }

        public MartenRole(string roleName) : this()
        {
            Name = roleName;
        }

        public IList<TRoleClaim> Claims { get; } = new List<TRoleClaim>();
        public TKey Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        public override string ToString()
        {
            return Name;
        }
    }
}