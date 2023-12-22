using System;
using System.Collections.Generic;

namespace SocialNetworkMovies.Models
{
    public partial class Review
    {
        public int Id { get; set; }
        public int FkIdMovie { get; set; }
        public int? FkIdUser { get; set; }
        public string StrState { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateLastChanged { get; set; }
        public int? FkIdUserCreated { get; set; }
        public int? FkIdUserLastChanged { get; set; }

        public virtual User? FkIdUserCreatedNavigation { get; set; }
        public virtual User? FkIdUserLastChangedNavigation { get; set; }
        public virtual User? FkIdUserNavigation { get; set; }
    }
}
