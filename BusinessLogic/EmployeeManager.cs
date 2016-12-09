﻿using DataAccessLayer;
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
        /// <param name="username">Paramter used to extract Employee Details</param>
        /// <returns>Employee Object Containing All Details</returns>
        public Employee RetrieveEmployeeByUsername(string username)
        {
            var employee = new Employee();
            
            //Retrieve all employee information
            var allUserDataInDB = EmployeeAccessor.RetrieveEmployeeByUsername(username);

            employee.Username = allUserDataInDB.Username;
            employee.FirstName = allUserDataInDB.FirstName;
            employee.LastName = allUserDataInDB.LastName;
            employee.OtherNames = allUserDataInDB.OtherNames;
            employee.DepartmentId = allUserDataInDB.DepartmentId;
            employee.Department = allUserDataInDB.Department;
            employee.PhoneNumber = allUserDataInDB.PhoneNumber;
            employee.Email = allUserDataInDB.Email;
            employee.PicUrl = allUserDataInDB.PicUrl;
            employee.isEmployed = allUserDataInDB.isEmployed;
            employee.isBlocked = allUserDataInDB.isBlocked;
            employee.UserRolesId = allUserDataInDB.UserRolesId;
            employee.JobDesignation = allUserDataInDB.JobDesignation;
            employee.ClearanceLevelId = allUserDataInDB.ClearanceLevelId;
            employee.ClearanceLevel = allUserDataInDB.ClearanceLevel;
            employee.userPersonalInfoId = allUserDataInDB.userPersonalInfoId;
            employee.Gender = allUserDataInDB.Gender;
            employee.DateOfBirth = allUserDataInDB.DateOfBirth;
            employee.CountryId = allUserDataInDB.CountryId;
            employee.Nationality = allUserDataInDB.Nationality; //might need to go
            employee.AdditionalInfoId = allUserDataInDB.AdditionalInfoId; //might need to also go
            employee.AdditonalInfo = allUserDataInDB.AdditonalInfo;
            employee.Address = allUserDataInDB.Address; //this is a list
            employee.HireDate = allUserDataInDB.HireDate;
            employee.MaritalStatus = allUserDataInDB.MaritalStatus;
            employee.PersonalEmail = allUserDataInDB.PersonalEmail;
            employee.PersonalPhoneNumber = allUserDataInDB.PersonalPhoneNumber;

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
