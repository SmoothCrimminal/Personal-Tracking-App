using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class TaskDAO : EmployeeContext
    {
        public static List<TASKSTATE> GetTaskStates()
        {
            return db.TASKSTATE.ToList();
        }

        public static void AddTask(TASK task)
        {
            try
            {
                db.TASK.InsertOnSubmit(task);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<TaskDetailDTO> getTasks()
        {
            List<TaskDetailDTO> taskList = new List<TaskDetailDTO>();

            var list = (from t in db.TASK
                        join s in db.TASKSTATE on t.TaskState equals s.ID
                        join e in db.EMPLOYEE on t.EmpolyeeID equals e.ID
                        join d in db.DEPARTMENT on e.DepartmentID equals d.ID
                        join p in db.POSITION on e.PositionID equals p.ID
                        select new
                        {
                            taskID = t.ID,
                            title = t.TaskTitle,
                            content = t.TaskContent,
                            startDate = t.TaskStartDate,
                            deliveryDate = t.TaskDeliveryDate,
                            taskStateName = s.StateName,
                            taskStateID = t.TaskState,
                            UserNo = e.UserNo,
                            Name = e.Name,
                            EmployeeID = e.ID,
                            Surname = e.Surname,
                            PositionName = p.Position1,
                            DepartmentName = d.DepartamentName,
                            PositionID = e.PositionID,
                            DepartmentID = e.DepartmentID
                        }).OrderBy(x => x.startDate).ToList();

            foreach (var item in list)
            {
                TaskDetailDTO dto = new TaskDetailDTO();

                dto.TaskID = item.taskID;
                dto.TaskTitle = item.title;
                dto.Content = item.content;
                dto.TaskStartDate = item.startDate;
                dto.TaskEndDate = item.deliveryDate;
                dto.TaskStateName = item.taskStateName;
                dto.TaskStateID = item.taskStateID;
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.EmployeeID = item.EmployeeID;
                dto.Surname = item.Surname;
                dto.PositionName = item.PositionName;
                dto.DepartmentName = item.DepartmentName;
                dto.PositionID = item.PositionID;
                dto.DepartmentID = item.DepartmentID;

                taskList.Add(dto);
            }
            return taskList;
        }

        public static void ApproveTask(int taskID, bool isAdmin)
        {
            try
            {
                TASK tsk = db.TASK.First(x => x.ID == taskID);
                if (isAdmin)
                    tsk.TaskState = TaskStates.Approved;
                else
                    tsk.TaskState = TaskStates.Delivered;
                tsk.TaskDeliveryDate = DateTime.Today;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void DeleteTask(int taskID)
        {
            try
            {
                TASK tsk = db.TASK.First(x => x.ID == taskID);
                db.TASK.DeleteOnSubmit(tsk);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static void UpdateTask(TASK task)
        {
            try
            {
                TASK tsk = db.TASK.First(x => x.ID == task.ID);
                tsk.TaskTitle = task.TaskTitle;
                tsk.TaskContent = task.TaskContent;
                tsk.TaskState = task.TaskState;
                tsk.EmpolyeeID = task.EmpolyeeID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
