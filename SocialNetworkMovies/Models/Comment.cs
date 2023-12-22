using System;
using System.Collections.Generic;

namespace SocialNetworkMovies.Models
{
    public partial class Comment
    {
        public Comment()
        {
            InverseFkIdCommentNavigation = new HashSet<Comment>();
            UserLists = new HashSet<UserList>();
        }

        public int Id { get; set; }
        public string StrName { get; set; } = null!;
        public int? FkIdUser { get; set; }
        public int? FkIdComment { get; set; }
        public string TextComment { get; set; } = null!;
        public string StrState { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateLastChanged { get; set; }
        public int? FkIdUserCreated { get; set; }
        public int? FkIdUserLastChanged { get; set; }
        public string? HashUrl { get; set; }

        public virtual Comment? FkIdCommentNavigation { get; set; }
        public virtual User? FkIdUserCreatedNavigation { get; set; }
        public virtual User? FkIdUserLastChangedNavigation { get; set; }
        public virtual User? FkIdUserNavigation { get; set; }
        public virtual ICollection<Comment> InverseFkIdCommentNavigation { get; set; }
        public virtual ICollection<UserList> UserLists { get; set; }
    }
}
