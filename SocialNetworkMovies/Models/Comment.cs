using System;
using System.Collections.Generic;

namespace SocialNetworkMovies.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string StrName { get; set; } = null!;

    public int? FkIdComment { get; set; }

    public string TextComment { get; set; } = null!;

    public int? FkIdDiscussion { get; set; }

    public string StrState { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateLastChanged { get; set; }

    public string? HashUrl { get; set; }

    public virtual Comment? FkIdCommentNavigation { get; set; }

    public virtual Discussion? FkIdDiscussionNavigation { get; set; }

    public virtual ICollection<Comment> InverseFkIdCommentNavigation { get; set; } = new List<Comment>();

    public virtual ICollection<UserList> UserLists { get; set; } = new List<UserList>();
}
