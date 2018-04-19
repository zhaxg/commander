using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commander.Services
{
    public class TaskSchedulerHelpers
    {
        public static void SetAutoStartup(string taskName, string exeName, string paramstring = "")
        {
            using (var ts = new TaskService())
            {
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = $"auto start exe file while logon: {exeName} {paramstring}";
                td.Principal.RunLevel = TaskRunLevel.Highest;
                td.Actions.Add(new ExecAction(exeName, paramstring, null));
                td.Triggers.Add(new LogonTrigger()); 

                ts.RootFolder.RegisterTaskDefinition(taskName, td);
            }
        }

        public static bool IsExist(string taskName)
        {
            using (var ts = new TaskService())
            {
                return ts.FindTask(taskName) != null;
            }
        }

        public static void DeleteTask(string taskName, bool exceptionOnNotExists = false)
        {
            using (var ts = new TaskService())
            {
                ts.RootFolder.DeleteTask(taskName, exceptionOnNotExists);
            }
        }
    }
}
