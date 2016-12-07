using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{

    /// <summary>
    /// Manages Employee Related Activities
    /// </summary>
    public class EmployeeManager
    {
       


        /// <summary>
        /// Retrieves all Active Employees From Database
        /// </summary>
        /// <param name="isEmployed"></param>
        /// <returns></returns>
        public List<Employee> retrieveEmployees(bool isEmployed)
        {
            var employeeList = new List<Employee>();

            employeeList = EmployeeAccessor.RetrieveEmployees(isEmployed);

            return employeeList;
        }
    }
}
