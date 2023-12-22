using System;
using System.Collections.Generic;

namespace SocialNetworkMovies.Models
{
    public partial class Permission
    {
        public int Id { get; set; }
        public int? FkIdRoleAllowed { get; set; }
        public int? FkIdAction { get; set; }
        public string StrState { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateLastChanged { get; set; }
        public int? FkIdUserCreated { get; set; }
        public int? FkIdUserLastChanged { get; set; }
        public string? HashUrl { get; set; }

        public virtual Action? FkIdActionNavigation { get; set; }
        public virtual Role? FkIdRoleAllowedNavigation { get; set; }
        public virtual User? FkIdUserCreatedNavigation { get; set; }
        public virtual User? FkIdUserLastChangedNavigation { get; set; }
    }
}
