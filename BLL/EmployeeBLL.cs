﻿using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;
using DAL;

namespace BLL
{
    public class EmployeeBLL
    {
        public static EmployeeDTO GetAll()
        {
            EmployeeDTO dto = new EmployeeDTO();
            dto.Departments = DepartmentDAO.GetDepartments();
            dto.Positions = PositionDAO.GetPositions();
            dto.Employees = EmployeeDAO.GetEmployees();
            return dto;

        }

        public static void AddEmployee(EMPLOYEE employee)
        {
            EmployeeDAO.AddEmployee(employee);
        }

        public static bool isUnique(int v)
        {
            List<EMPLOYEE> employeeList = EmployeeDAO.GetUsers(v);
            if (employeeList.Count > 0)
                return false;
            else
                return true;

        }

        public static List<EMPLOYEE> GetEmployees(int userNo, string password)
        {
            return EmployeeDAO.GetEmployees(userNo, password);
        }

        public static void UpdateEmployee(EMPLOYEE employee)
        {
            EmployeeDAO.UpdateEmployee(employee);
        }
    }
}
