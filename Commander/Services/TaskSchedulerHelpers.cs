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
                TaskDefinition task = ts.NewTask();
                task.RegistrationInfo.Description = $"auto start exe file while logon: {exeName} {paramstring}";
                task.Actions.Add(new ExecAction(exeName, paramstring, null));
                task.Triggers.Add(new LogonTrigger());

                task.Principal.RunLevel = TaskRunLevel.Highest;
                task.Settings.DisallowStartIfOnBatteries = false;
                task.Settings.StopIfGoingOnBatteries = false;

                ts.RootFolder.RegisterTaskDefinition(taskName, task);
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
