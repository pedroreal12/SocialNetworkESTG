using System;
using System.Collections.Generic;

namespace SocialNetworkMovies.Models;

public partial class Discussion
{
    public int Id { get; set; }

    public int FkIdMovie { get; set; }

    public string StrText { get; set; } = null!;

    public string StrState { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateLastChanged { get; set; }

    public string? HashUrl { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
