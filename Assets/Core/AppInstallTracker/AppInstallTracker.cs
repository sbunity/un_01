using System;
using UnityEngine;

namespace Core.AppInstallTracker
{
    [Serializable]
    public class DateTimeWrapper
    {
        [SerializeField] private int year;
        [SerializeField] private int month;
        [SerializeField] private int day;

        public DateTime Value => new DateTime(year, month, day);

        public void SetDate(DateTime date)
        {
            year = date.Year;
            month = date.Month;
            day = date.Day;
        }
    }
    
    public static class AppInstallTracker
    {
        public static bool HasEnoughDaysPassed()
        {
            var timeSinceInstall = Settings.BirthDate().Value - DateTime.Now;

            if (timeSinceInstall.TotalDays <= 0)
            {
                Debug.Log("The target date has been reached or passed.");
                return true; // Target date has been reached or passed
            }
            else
            {
                var daysLeft = Math.Ceiling(timeSinceInstall.TotalDays);
                Debug.Log($"The target date has not been reached. You need to wait {daysLeft} more days.");
                return false; // Target date has not been reached
            }
        }
    }
}