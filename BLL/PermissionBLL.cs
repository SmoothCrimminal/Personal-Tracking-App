﻿using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;
using DAL.DTO;

namespace BLL
{
    public class PermissionBLL
    {
        public static void AddPermission(PERMISSION permission)
        {
            PermissionDAO.AddPermission(permission);
        }

        public static PermissionDTO GetAll()
        {
            PermissionDTO dto = new PermissionDTO();
            dto.Departments = DepartmentDAO.GetDepartments();
            dto.Positions = PositionDAO.GetPositions();
            dto.PermissionStates = PermissionDAO.GetStates();
            dto.PermissionDetails = PermissionDAO.GetDetails();
            return dto;
        }

        public static void UpdatePermission(PERMISSION permission)
        {
            PermissionDAO.UpdatePermission(permission);
        }

        public static void UpdatePermission(int permissionID, int approved)
        {
            PermissionDAO.UpdatePermission(permissionID, approved);
        }

        public static void DeletePermision(int permissionID)
        {
            PermissionDAO.DeletePermision(permissionID);
        }
    }
}
