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

        public DateTime Date { get; set; }

        public string Nationality { get; set; }

        public string AdditionalInfoId { get; set; }

        public List<Address> Address { get; set; }

        public DateTime HireDate { get; set; }

        public string DisplayName
        {
            get
            {
                return FirstName + " " + OtherNames + " " + LastName;
            }
        }

    }
}
