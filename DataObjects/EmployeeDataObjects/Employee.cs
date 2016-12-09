using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Employee : User
    {
        public string userPersonalInfoId { get; set; }

        public bool Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int CountryId { get; set; }

        public string Nationality { get; set; }

        public string AdditionalInfoId { get; set; }

        public string AdditonalInfo { get; set; }

        public List<Address> Address { get; set; }

        public DateTime HireDate { get; set; }

        public bool MaritalStatus { get; set; }

        public string PersonalEmail { get; set; }

        public string PersonalPhoneNumber { get; set; }

        public string DisplayName
        {
            get
            {
                return FirstName + " " + OtherNames + " " + LastName;
            }
        }

    }
}
