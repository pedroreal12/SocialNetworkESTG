using System;
using System.Collections.Generic;

namespace SocialNetworkMovies.Models;

public partial class Review
{
    public int Id { get; set; }

    public int FkIdMovie { get; set; }

    public string StrState { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateLastChanged { get; set; }

    public string? FkIdUserCreated { get; set; }

    public int? IntValue { get; set; }

    public int? FkIdComment { get; set; }

    public string? UserName { get; set; }
}
