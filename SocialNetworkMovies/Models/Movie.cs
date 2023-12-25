using System;
using System.Collections.Generic;

namespace SocialNetworkMovies.Models
{
    public partial class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public bool IsAdultMovie { get; set; }
        public string HomePageUrl { get; set; } = null!;
        public string PosterPath { get; set; } = null!;
        public string Budget { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Tagline { get; set; } = null!;
        public string Overview { get; set; } = null!;
        public float? VoteAverage { get; set; } = null;
        public float? VoteCount { get; set; } = null;
    }
}
