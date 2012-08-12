using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Scheduler;

namespace GeomindMe.ReminderScheduler
{
    public class ScheduledAgentsManager
    {

        public const string BACKGROUND_TASK_NAME = "BackgroundTask";
        public bool IsBackgroundTaskRegistered()
        {
            var taskName = BACKGROUND_TASK_NAME;

            var oldTask = ScheduledActionService.Find(taskName) as PeriodicTask;
            bool isBackgroundTaskRegistered = (oldTask != null);
            
            return isBackgroundTaskRegistered;
        }

        public void RegisterBackgroundTask()
        {
            // A unique name for your task. It is used to
            // locate it in from the service.
            var taskName = BACKGROUND_TASK_NAME;

            // If the task exists
            var oldTask = ScheduledActionService.Find(taskName) as PeriodicTask;
            if (oldTask != null)
            {
                ScheduledActionService.Remove(taskName);
            }

            // Create the Task
            PeriodicTask task = new PeriodicTask(taskName);

            // Description is required
            task.Description = "Checks for TO DO-s in range of your gps location";

            // Add it to the service to execute
            ScheduledActionService.Add(task);
        }

        public void UnregisterBackgroundTask()
        {
            // A unique name for your task. It is used to
            // locate it in from the service.
            var taskName = BACKGROUND_TASK_NAME;

            // If the task exists
            var oldTask = ScheduledActionService.Find(taskName) as PeriodicTask;
            if (oldTask != null)
            {
                ScheduledActionService.Remove(taskName);
            }
        }

        public void TestBackgroundTask()
        {
            var taskName = BACKGROUND_TASK_NAME;

            var oldTask = ScheduledActionService.Find(taskName) as PeriodicTask;
            if (oldTask != null)
            {
                ScheduledActionService.LaunchForTest(taskName,
                  TimeSpan.FromMilliseconds(3000));
            }
        }
        private static ScheduledAgentsManager _instance;
        public static ScheduledAgentsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScheduledAgentsManager();
                }

                return _instance;
            }

        }

    }
}
