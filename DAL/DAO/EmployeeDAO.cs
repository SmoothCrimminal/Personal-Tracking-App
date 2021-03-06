﻿using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class EmployeeDAO : EmployeeContext
    {
        public static void AddEmployee(EMPLOYEE employee)
        {
            try
            {
                db.EMPLOYEE.InsertOnSubmit(employee);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<EmployeeDetailDTO> GetEmployees()
        {
            List<EmployeeDetailDTO> employeeList = new List<EmployeeDetailDTO>();
            var list = (from e in db.EMPLOYEE
                        join d in db.DEPARTMENT on e.DepartmentID equals d.ID
                        join p in db.POSITION on e.PositionID equals p.ID
                        select new
                        {
                            UserNo = e.UserNo,
                            Name = e.Name,
                            Surname = e.Surname,
                            EmployeeID = e.ID,
                            Password = e.Password,
                            DepartmentName = d.DepartamentName,
                            PositionName = p.Position1,
                            DepartmentID = d.ID,
                            PositionID = p.ID,
                            isAdmin = e.isAdmin,
                            Salary = e.Salary,
                            ImagePath = e.ImagePath,
                            Address = e.Address,
                            Birthday = e.Birthday
                        }).OrderBy(x => x.UserNo).ToList();

            foreach (var item in list)
            {
                EmployeeDetailDTO dto = new EmployeeDetailDTO();
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.EmployeeID = item.EmployeeID;
                dto.Password = item.Password;
                dto.DepartmentName = item.DepartmentName;
                dto.PositionName = item.PositionName;
                dto.DepartmentID = item.DepartmentID;
                dto.PositionID = item.PositionID;
                dto.isAdmin = item.isAdmin;
                dto.Salary = item.Salary;
                dto.ImagePath = item.ImagePath;
                dto.Address = item.Address;
                dto.Birthday = item.Birthday;
                employeeList.Add(dto);
            }

            return employeeList;
        }

        public static void DeleteEmployee(int employeeID)
        {
            try
            {
                EMPLOYEE emp = db.EMPLOYEE.First(x => x.ID == employeeID);
                db.EMPLOYEE.DeleteOnSubmit(emp);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateEmployee(POSITION position)
        {
            List<EMPLOYEE> list = db.EMPLOYEE.Where(x => x.PositionID == position.ID).ToList();
            foreach (var item in list)
            {
                item.DepartmentID = position.DepID;
            }
            db.SubmitChanges();
        }

        public static void UpdateEmployee(EMPLOYEE employee)
        {
            try
            {
                EMPLOYEE empl = db.EMPLOYEE.First(x => x.ID == employee.ID);
                empl.UserNo = employee.UserNo;
                empl.Address = employee.Address;
                empl.Birthday = employee.Birthday;
                empl.DepartmentID = employee.DepartmentID;
                empl.isAdmin = employee.isAdmin;
                empl.Name = employee.Name;
                empl.Password = employee.Password;
                empl.PositionID = employee.PositionID;
                empl.Salary = employee.Salary;
                empl.Surname = employee.Surname;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public static void UpdateEmployee(int employeeID, int amount)
        {
            try
            {
                EMPLOYEE empl = db.EMPLOYEE.First(x => x.ID == employeeID);
                empl.Salary = amount;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<EMPLOYEE> GetEmployees(int userNo, string password)
        {
            try
            {
                List<EMPLOYEE> list = db.EMPLOYEE.Where(x => x.UserNo == userNo && x.Password == password).ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<EMPLOYEE> GetUsers(int v)
        {
            return db.EMPLOYEE.Where(x => x.UserNo == v).ToList();
        }
    }
}
