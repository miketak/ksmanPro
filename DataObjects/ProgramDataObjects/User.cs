using System;
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

        public string DepartmentId { get; set; }

        public string Department { get; set; } //new guy //Shall be scrapped

        public string PasswordHash { get; set; }

        public string SSNo { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string PicUrl { get; set; }

        public bool isEmployed { get; set; } //might change back to bool //placeholder of isActive in database

        public bool isBlocked { get; set; } //might change back to bool

        public string UserRolesId { get; set; }

        public string JobDesignation { get; set; } //new guy  //Shall be Scrapped

        public int ClearanceLevelId { get; set; }

        public string ClearanceLevel { get; set; } //new guy //Shall be scrapped

    }
}
