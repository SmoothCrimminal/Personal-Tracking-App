using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;
using DAL;

namespace BLL
{
    public class SalaryBLL
    {
        public static SalaryDTO GetAll()
        {
            SalaryDTO salaryDTO = new SalaryDTO();
            salaryDTO.Employees = EmployeeDAO.GetEmployees();
            salaryDTO.Departments = DepartmentDAO.GetDepartments();
            salaryDTO.Positions = PositionDAO.GetPositions();
            salaryDTO.Months = SalaryDAO.GetMonths();
            salaryDTO.Salaries = SalaryDAO.GetSalaries();

            return salaryDTO;
        }

        public static void AddSalary(SALARY salary, bool control)
        {
            SalaryDAO.AddSalary(salary);
            if (control)
                EmployeeDAO.UpdateEmployee(salary.EmployeeID, salary.Amount);
        }

        public static void UpdateSalary(SALARY salary, bool control)
        {
            SalaryDAO.UpdateSalary(salary);
            if (control)
                EmployeeDAO.UpdateEmployee(salary.EmployeeID, salary.Amount);
        }

        public static void DeleteSalary(int salaryID)
        {
            SalaryDAO.DeleteSalary(salaryID);
        }
    }
}
