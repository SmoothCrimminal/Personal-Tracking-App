using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class SalaryDAO : EmployeeContext
    {
        public static List<MONTH> GetMonths()
        {
            return db.MONTH.ToList();
        }

        public static void AddSalary(SALARY salary)
        {
            try
            {
                db.SALARY.InsertOnSubmit(salary);
                db.SubmitChanges();
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public static List<SalaryDetailDTO> GetSalaries()
        {
            List<SalaryDetailDTO> salaryList = new List<SalaryDetailDTO>();
            var list = (from s in db.SALARY
                        join e in db.EMPLOYEE on s.EmployeeID equals e.ID
                        join m in db.MONTH on s.MonthID equals m.ID
                        select new
                        {
                            UserNo = e.UserNo,
                            Name = e.Name,
                            Surname = e.Surname,
                            EmployeeID = e.ID,
                            Amount = s.Amount,
                            Year = s.Year,
                            MonthName = m.MonthName,
                            MonthID = m.ID,
                            SalaryID = s.ID,
                            DepartmentID = e.DepartmentID,
                            PositionID = e.PositionID
                        }).OrderBy(x => x.Year).ToList();
            foreach (var item in list)
            {
                SalaryDetailDTO dto = new SalaryDetailDTO();

                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.EmployeeID = item.EmployeeID;
                dto.SalaryAmount = item.Amount;
                dto.SalaryYear = item.Year;
                dto.MonthName = item.MonthName;
                dto.MonthID = item.MonthID;
                dto.SalaryID = item.SalaryID;
                dto.DepartmentID = item.DepartmentID;
                dto.PositionID = item.PositionID;
                dto.OldSalary = item.Amount;
                salaryList.Add(dto);
            }
            return salaryList;
        }

        public static void UpdateSalary(SALARY salary)
        {
            try
            {
                SALARY sl = db.SALARY.First(x => x.ID == salary.ID);
                sl.Amount = salary.Amount;
                sl.Year = salary.Year;
                sl.MonthID = salary.MonthID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
