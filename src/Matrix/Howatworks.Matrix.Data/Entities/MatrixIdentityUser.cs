﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Howatworks.Matrix.Data.Entities
{
    public class MatrixIdentityUser : IdentityUser
    {
        public string CommanderName { get; set; }

        public MatrixIdentityUser()
        {
        }

        public MatrixIdentityUser(string username)
            : base(username)
        {
        }
    }
}
