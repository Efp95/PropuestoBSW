using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropuestoBSW.Core
{
    #region Comments
    // Question 1
    // Some pieces of code will modified because of needs.
    // Errors will be enumerated. <@0x>
    #endregion

    public class JobLogger
    {

        private static bool _logToFile;
        private static bool _logToConsole;
        private static bool _logMessage;    // @01 - Variable is not really necessary (just from LogMessage method)
        private static bool _logWarning;    // @02 - Variable is not really necessary (just from LogMessage method)
        private static bool _logError;      // @03 - Variable is not really necessary (just from LogMessage method)
        private static bool LogToDatabase;
        private bool _initialized;          // @04 - Variable that is never used

        public JobLogger(bool logToFile, bool logToConsole, bool logToDatabase, bool logMessage, bool logWarning, bool logError)
        {
            _logError = logError;           //  Marked.
            _logMessage = logMessage;       //  Marked.
            _logWarning = logWarning;       //  Marked.
            LogToDatabase = logToDatabase;
            _logToFile = logToFile;
            _logToConsole = logToConsole;
            // @05 - Marked parameters should be removed because of static variables.
        }

        
        public static void LogMessage(string message, bool bmessage, bool warning, bool error)
        {
            // #Obs: bool variables "message" is changed to "bmessage" to avoid duplicated parameter names
            message.Trim();     //  @06 - Can throw an exception if "message" is null (NullReferenceException)

            #region @07 - Block of code can be refactored
            if (message == null || message.Length == 0)
            {
                return;
            }
            if (!_logToConsole && !_logToFile && !LogToDatabase)
            {
                throw new Exception("Invalid configuration");
            }
            if ((!_logError && !_logMessage && !_logWarning) || (!bmessage && !warning
            && !error))
            {
                throw new Exception("Error or Warning or Message must be specified");
            }
            #endregion

            #region @08 Block of code can be refactored
            System.Data.SqlClient.SqlConnection connection = new
            System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            // @09 - Instead of AppSettings property, use ConnectionStrings to initialize a connection
            connection.Open();
            
            int t = 0;
            if (bmessage && _logMessage)
            {
                t = 1;
            }
            if (error && _logError)
            {
                t = 2;
            }
            if (warning && _logWarning)
            {
                t = 3;
            }

            System.Data.SqlClient.SqlCommand command = new
            System.Data.SqlClient.SqlCommand("Insert into Log Values('" + message + "', " + t.ToString() + ")");
            //  @10 - It's better to use Stored Procedures because of maintainability, security, etc.
            command.ExecuteNonQuery();
            //  @11 - This block of code can throw an exception (SqlException)
            #endregion

            #region @12 Block of code can be refactored
            string l = String.Empty;
            // @13 - Use "StringBuilder" class or "String.Format" or "String.Concat" methods to concatenate string variables
            if
            (!System.IO.File.Exists(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt"))
            {
                l =
                System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt");
            }
            if (error && _logError)
            {
                l = l + DateTime.Now.ToShortDateString() + message;
            }
            if (warning && _logWarning)
            {
                l = l + DateTime.Now.ToShortDateString() + message;
            }
            if (bmessage && _logMessage)
            {
                l = l + DateTime.Now.ToShortDateString() + message;
            }

            System.IO.File.WriteAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt", l);
            //  @14 - Can throw an exception if the file is in use or permissions (IOException)
            #endregion

            #region @15 Block of code can be refactores
            if (error && _logError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (warning && _logWarning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            if (bmessage && _logMessage)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine(DateTime.Now.ToShortDateString() + message);
            #endregion

            //  @16 - Using directive provides an easier use of classes ([using] [namespace])
            //  @17 - Set priority to logTypes (Message - Warning - Error)
        }
        

    }
}
