using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class PositionDAO : EmployeeContext
    {
        public static void AddPosition(POSITION position)
        {
            try
            {
                db.POSITION.InsertOnSubmit(position);
                db.SubmitChanges();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PositionDTO> GetPositions()
        {
            try
            {
                var list = (from p in db.POSITION
                            join d in db.DEPARTMENT
                            on p.DepID equals d.ID
                            select new
                            {
                                positionID = p.ID,
                                positionName = p.Position1,
                                departmentName = d.DepartamentName,
                                departmentID = d.ID
                            }).OrderBy(x => x.positionID).ToList();

                List<PositionDTO> positionList = new List<PositionDTO>();
                foreach (var listItem in list)
                {
                    PositionDTO dto = new PositionDTO();
                    dto.ID = listItem.positionID;
                    dto.Position1 = listItem.positionName;
                    dto.DepartmentName = listItem.departmentName;
                    dto.DepID = listItem.departmentID;
                    positionList.Add(dto);
                }
                return positionList;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
