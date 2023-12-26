using System;
using System.Collections.Generic;

namespace SocialNetworkMovies.Models;

public partial class UserList
{
    public int Id { get; set; }

    public string StrName { get; set; } = null!;

    public int? FkIdMovieList { get; set; }

    public string StrState { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateLastChanged { get; set; }

    public string? HashUrl { get; set; }

    public virtual Comment? FkIdMovieListNavigation { get; set; }

    public virtual ICollection<MovieList> MovieLists { get; set; } = new List<MovieList>();
}
