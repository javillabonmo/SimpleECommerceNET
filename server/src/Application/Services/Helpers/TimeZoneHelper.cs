
using System.Runtime.InteropServices;

namespace Application.Services.Helpers
{
    public static class TimeZoneHelper
    {
        public static bool TryConvertToLocalTime(
            DateTime utcDateTime,
            string timeZoneId,
            out DateTime localTime)
        {
            try
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                localTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZone);
                return true;
            }
            catch (TimeZoneNotFoundException)
            {
                localTime = default;
                return false;
            }
            catch (InvalidTimeZoneException)
            {
                localTime = default;
                return false;
            }
        }

        public static bool TryAssignLocalTime<T>(DateTime utcDateTime, string timeZoneId, T target, Action<T, DateTime> assignAction)
        {
            if (TryConvertToLocalTime(utcDateTime, timeZoneId, out var localTime))
            {
                assignAction(target, localTime);
                return true;
            }

            return false;
        }
    }

}
