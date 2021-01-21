using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class PermissionDAO : EmployeeContext
    {
        public static void AddPermission(PERMISSION permission)
        {
            try
            {
                db.PERMISSION.InsertOnSubmit(permission);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<PERMISSIONSTATE> GetStates()
        {
            return db.PERMISSIONSTATE.ToList();
        }

        public static List<PermissionDetailDTO> GetDetails()
        {
            List<PermissionDetailDTO> permissionsList = new List<PermissionDetailDTO>();
            var list = (from p in db.PERMISSION
                        join ps in db.PERMISSIONSTATE on p.PermissionState equals ps.ID
                        join e in db.EMPLOYEE on p.EmployeeID equals e.ID
                        select new
                        {
                            UserNo = e.UserNo,
                            Name = e.Name,
                            Surname = e.Surname,
                            StateName = ps.StateName,
                            StateID = p.PermissionState,
                            EndDate = p.PermissionEndDate,
                            StartDate = p.PermissionStartDate,
                            EmployeeID = p.EmployeeID,
                            PermissionID = p.ID,
                            Explanation = p.PermissionExplanation,
                            DayAmount = p.PermissionDay,
                            DepartmentID = e.DepartmentID,
                            PosistionID = e.PositionID

                        }).OrderBy(x => x.StartDate).ToList();

            foreach (var item in list)
            {
                PermissionDetailDTO dto = new PermissionDetailDTO();
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.StateName = item.StateName;
                dto.State = item.StateID;
                dto.EndDate = item.EndDate;
                dto.StartDate = item.StartDate;
                dto.EmployeeID = item.EmployeeID;
                dto.PermissionID = item.PermissionID;
                dto.Explanation = item.Explanation;
                dto.PermissionDayAmount = item.DayAmount;
                dto.DepartmentID = item.DepartmentID;
                dto.PositionID = item.PosistionID;
                permissionsList.Add(dto);
            }
            return permissionsList;
        }

        public static void UpdatePermission(int permissionID, int approved)
        {
            try
            {
                PERMISSION pr = db.PERMISSION.First(x => x.ID == permissionID);
                pr.PermissionState = approved;
                db.SubmitChanges();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public static void UpdatePermission(PERMISSION permission)
        {
            try
            {
                PERMISSION pr = db.PERMISSION.First(x => x.ID == permission.ID);
                pr.PermissionStartDate = permission.PermissionStartDate;
                pr.PermissionEndDate = permission.PermissionEndDate;
                pr.PermissionDay = permission.PermissionDay;
                pr.PermissionExplanation = permission.PermissionExplanation;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
