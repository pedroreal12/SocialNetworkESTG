using System;
using System.Collections.Generic;

namespace SocialNetworkMovies.Models
{
    public partial class UserList
    {
        public UserList()
        {
            MovieLists = new HashSet<MovieList>();
        }

        public int Id { get; set; }
        public string StrName { get; set; } = null!;
        public int? FkIdUser { get; set; }
        public int? FkIdMovieList { get; set; }
        public string StrState { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateLastChanged { get; set; }
        public int? FkIdUserCreated { get; set; }
        public int? FkIdUserLastChanged { get; set; }
        public string? HashUrl { get; set; }

        public virtual Comment? FkIdMovieListNavigation { get; set; }
        public virtual User? FkIdUserCreatedNavigation { get; set; }
        public virtual User? FkIdUserLastChangedNavigation { get; set; }
        public virtual User? FkIdUserNavigation { get; set; }
        public virtual ICollection<MovieList> MovieLists { get; set; }
    }
}
