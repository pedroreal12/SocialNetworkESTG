﻿using System;
using System.Collections.Generic;

namespace SocialNetworkMovies.Models;

public partial class Student
{
    public int Id { get; set; }

    public string StrName { get; set; } = null!;

    public string StrMail { get; set; } = null!;

    public string StrPhoneNumber { get; set; } = null!;

    public string StrState { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateLastChanged { get; set; }

    public string? HashUrl { get; set; }
}
