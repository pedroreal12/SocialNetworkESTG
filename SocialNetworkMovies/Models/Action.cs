using System;
using System.Collections.Generic;

namespace SocialNetworkMovies.Models
{
    public partial class Action
    {
        public Action()
        {
            Permissions = new HashSet<Permission>();
        }

        public int Id { get; set; }
        public string StrName { get; set; } = null!;
        public string StrController { get; set; } = null!;
        public string StrState { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateLastChanged { get; set; }
        public int? FkIdUserCreated { get; set; }
        public int? FkIdUserLastChanged { get; set; }
        public string? HashUrl { get; set; }

        public virtual User? FkIdUserCreatedNavigation { get; set; }
        public virtual User? FkIdUserLastChangedNavigation { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
