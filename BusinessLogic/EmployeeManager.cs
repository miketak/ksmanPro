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
        /// <param name="isEmployed">True or False boolean paramter to retrieve active or inactive employees respecetively</param>
        /// <returns>A list of active or inactive Employee objects from Database.</returns>
        public List<Employee> RetrieveEmployees(bool isEmployed)
        {
            var employeeList = new List<Employee>();

            employeeList = EmployeeAccessor.RetrieveEmployees(isEmployed);

            return employeeList;
        }


        /// <summary>
        /// Retrieves Employee from database using Employee Id or User Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public Employee RetrieveEmployeeByUsername(string employeeId)
        {
            var employee = new Employee();
            
            //Basic employee informaation
            var userBasicDataInDB = UserAccessor.RetrieveUserByUsername(employeeId);
            employee.Username = userBasicDataInDB.Username;
            employee.FirstName = userBasicDataInDB.FirstName;
            employee.LastName = userBasicDataInDB.LastName;
            employee.OtherNames = userBasicDataInDB.OtherNames;
            employee.DepartmentId = userBasicDataInDB.DepartmentId;
            employee.UserRolesId = userBasicDataInDB.UserRolesId;
            employee.Department = userBasicDataInDB.Department;
            employee.isEmployed = userBasicDataInDB.isEmployed;


            return employee;
        }

        /// <summary>
        /// Retrieves Department by visibility
        /// </summary>
        /// <param name="p">Visibility indicator</param>
        /// <returns>Returns a list of departments</returns>
        public List<Department> RetrieveDepartmentsByVisibility(bool p)
        {
            var departments = new List<Department>();

            var departmentAccess = new EmployeeAccessor();
            departments = departmentAccess.RetrieveDepartments(p);


            return departments;
        }

        /// <summary>
        /// Retrieves Job Positions based on DepartmentID
        /// </summary>
        /// <param name="departmentId">Retrieves ID</param>
        /// <param name="isRetrieveAll">Paramter to indicate if all should be retrieved</param>
        /// <returns>List of User Roles</returns>
        public List<UserRoles> RetrieveJobPositionByDeptId(string departmentId, bool isRetrieveAll)
        {
            var jobPositions = new List<UserRoles>();

            var jobPositionAccess = new EmployeeAccessor();
            jobPositions = jobPositionAccess.RetrieveUserRoles(departmentId, isRetrieveAll);

            return jobPositions;
        }
    }
}
