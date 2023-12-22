using System;
using System.Collections.Generic;

namespace SocialNetworkMovies.Models
{
    public partial class Role
    {
        public Role()
        {
            Permissions = new HashSet<Permission>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string StrName { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateLastChanged { get; set; }
        public int? FkIdUserCreated { get; set; }
        public int? FkIdUserLastChanged { get; set; }
        public string StrState { get; set; } = null!;
        public string? HashUrl { get; set; }

        public virtual User? FkIdUserCreatedNavigation { get; set; }
        public virtual User? FkIdUserLastChangedNavigation { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
