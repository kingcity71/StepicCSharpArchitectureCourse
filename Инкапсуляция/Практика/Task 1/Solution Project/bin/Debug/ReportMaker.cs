using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Failures
{
    public enum FailureType
    {
        UnexpectedShutdown,
        ShortNonResponding,
        HardwareFailures,
        ConnectionProblems
    }
    public class Device
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    public class Failure
    {
        public FailureType FailureType { get; set; }
        public bool IsFailureSerious()
            => FailureType == FailureType.UnexpectedShutdown
            || FailureType == FailureType.HardwareFailures;
    }
    public class Common
    {
        public static bool Earlier(object[] v, int day, int month, int year)
        {
            int vYear = (int)v[2];
            int vMonth = (int)v[1];
            int vDay = (int)v[0];
            if (vYear < year) return true;
            if (vYear > year) return false;
            if (vMonth < month) return true;
            if (vMonth > month) return false;
            if (vDay < day) return true;
            return false;
        }
        public static bool Earlier(DateTime date1, DateTime date2)
        => date1 < date2;
    }

    public class ReportMaker
    {
        /// <summary>
        /// </summary>
        /// <param name="day"></param>
        /// <param name="failureTypes">
        /// 0 for unexpected shutdown, 
        /// 1 for short non-responding, 
        /// 2 for hardware failures, 
        /// 3 for connection problems
        /// </param>
        /// <param name="deviceId"></param>
        /// <param name="times"></param>
        /// <param name="devices"></param>
        /// <returns></returns>
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] failureTypes,
            int[] deviceId,
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            var failureDevices = new int[failureTypes.Length][];
            for (var i = 0; i < failureTypes.Length; i++)
                failureDevices[i] = new[] { failureTypes[i], deviceId[i] };

            var devicesArray = devices.Select(x => new Device()
            {
                Id = (int)x["DeviceId"],
                Name = (string)x["Name"]
            }).ToArray();

            return FindDevicesFailedBeforeDate(
                new DateTime(year, month, day),
                failureDevices,
                times,
                devicesArray).ToList();
        }

        public static string[] FindDevicesFailedBeforeDate(
            DateTime dateTime,
            int[][] failureDevice,
            object[][] times,
            Device[] devices)
        {
            var problematicDevices
                = failureDevice
                .Where((x, i) =>
                new Failure() { FailureType = (FailureType)x[0] }.IsFailureSerious()
                    && Common.Earlier(
                        new DateTime((int)times[i][2], (int)times[i][1], (int)times[i][0]),
                        dateTime))
                .Select(x => x[1])
                .ToArray();

            var result = devices
                .Where(x => problematicDevices.Contains(x.Id))
                .Select(x => x.Name)
                .Cast<string>()
                .ToArray();

            return result;
        }
    }
}
