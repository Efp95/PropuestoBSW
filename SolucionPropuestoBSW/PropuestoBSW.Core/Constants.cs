using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropuestoBSW.Core
{
    public static class Constants
    {

        public struct ExceptionMessages
        {
            public static string NoDestinyToLog = ConfigurationManager.AppSettings["NoDestinyToLog"].Trim();
            public static string NoLogType = ConfigurationManager.AppSettings["NoLogType"].Trim();
            public static string ErrorLoggingDB = ConfigurationManager.AppSettings["ErrorLoggingDB"].Trim();
            public static string ErrorLoggingIO = ConfigurationManager.AppSettings["ErrorLoggingIO"].Trim();
        }

    }
}
