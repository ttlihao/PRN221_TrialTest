﻿using System;
using System.Collections.Generic;

namespace Candidate_BussinessObjects.Models
{
    public partial class Hraccount
    {
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public int? MemberRole { get; set; }
    }
}
