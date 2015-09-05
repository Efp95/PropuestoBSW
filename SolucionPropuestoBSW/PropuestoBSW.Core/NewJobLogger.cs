using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropuestoBSW.Core
{
    #region Comments
    // Question 2.1. Development
    #endregion

    public class NewJobLogger
    {
        // Removed unnecessary variables

        /// <summary>
        /// This method returns a string value just for some unit tests
        /// </summary>
        public static string LogMessage(string message, MessageType? messageType, bool logToConsole,
                                                    bool logToFile, bool logToDatabase)
        {
            if (String.IsNullOrEmpty(message)) return String.Empty;
            else message.Trim();

            if (!messageType.HasValue)
                throw new InvalidOperationException(Constants.ExceptionMessages.NoLogType);

            if (NoSelection(logToDatabase, logToFile, logToConsole))
                throw new InvalidOperationException(Constants.ExceptionMessages.NoDestinyToLog);

            if (logToConsole)
                LogToConsole(message, messageType.Value);

            if (logToFile)
                LogToFile(message);

            if (logToDatabase)
                LogToDatabase(message, messageType.Value);

            return "success";
        }


        private static bool NoSelection(params bool[] evals)
        {
            return !(evals.FirstOrDefault(e => e == true));
        }
                
                
        private static void LogToDatabase(string message, MessageType messageType)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbSolucion"].ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.USP_InsertLog", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@pMessage", SqlDbType.VarChar, -1).Value = message;
                        cmd.Parameters.Add("@pDescription", SqlDbType.Char, 1).Value = ((int)messageType).ToString();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.ExceptionMessages.ErrorLoggingDB, ex);
            }

            // Manage Disposable Objects
            // Use of Stores Procedures
            // Handle Errors
            // Correct use of configuration file
        }
                
        private static void LogToFile(string message)
        {
            try
            {
                string today = DateTime.Now.ToString("yyyyMMdd");
                string fileName = String.Format("{0}_{1}.{2}", "LogFile", today, "txt");
                string fileRoute = Path.Combine(ConfigurationManager.AppSettings["LogFileDirectory"], fileName);

                using (StreamWriter sw = new StreamWriter(fileRoute, true))
                {
                    sw.WriteLine(String.Format("{0} => {1}", today, message));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.ExceptionMessages.ErrorLoggingIO, ex);
            }

            // Handle Errors
            // Configurable Messages
            // Better way to concatenate
        }
                
        private static void LogToConsole(string message, MessageType messageType)
        {
            if (messageType == MessageType.Message)
                Console.ForegroundColor = ConsoleColor.White;
            if (messageType == MessageType.Warning)
                Console.ForegroundColor = ConsoleColor.Yellow;
            if (messageType == MessageType.Error)
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(String.Concat(DateTime.Now.ToString("yyyyMMdd"), message));

            // Set priority (If there's an error, the background will be red)
        }

    }

    public enum MessageType
    {
        Message = 1,
        Warning = 2,
        Error = 3
    }

}
