﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public Dictionary<String, String> Roles { get; set; } = new Dictionary<string, string>();

        public string RoleList
        {
            get { return String.Join(", ", Roles.Select(x=>x.Value)); }
        }

    }
}
