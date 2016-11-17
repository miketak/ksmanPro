﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string OtherNames { get; set; }

        public string Department { get; set; }

        public string SSNo { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string PicUrl { get; set; }

        public int isEmployed { get; set; } //might change back to bool

        public int isBlocked { get; set; } //might change back to bool

        public string UserRolesId { get; set; }

        public int ClearanceLevelId { get; set; }

    }
}
