using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Server.Configuration;

namespace Server.Log
{
    public interface ILogger
    {
        void Write(string category, string message, Exception ex = null);
        void StartAction(string action);
        void PersistLog();
    }

    public class Logger : ILogger
    {
        private readonly IConfiguration _configuration;

        public Logger(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly List<LogEvent> _logEvents = new List<LogEvent>();
        private long _eventId;
        private string _action;
        private Stopwatch _stopwatch;

        public void StartAction(string action)
        {
            if (_action != null)
                return;

            _stopwatch = Stopwatch.StartNew();
            _action = action;
        }

        public void Write(string category, string message, Exception ex = null)
        {
            _logEvents.Add(new LogEvent { Category = category, Message = message, Exception = ex, When = DateTime.Now });
        }

        public void PersistLog()
        {
            CreateLogEntry();

            using (var connection = new SqlConnection(_configuration.ConnectionString))
            {
                connection.Open();
                foreach (var logEvent in _logEvents)
                    WriteLogDetail(logEvent, connection);
            }
        }

        private void WriteLogDetail(LogEvent logEvent, SqlConnection connection)
        {
            using (var command = new SqlCommand("CreateLogDetail", connection) { CommandType = CommandType.StoredProcedure})
            {
                command.Parameters.Add(new SqlParameter("Category", logEvent.Category));
                command.Parameters.Add(new SqlParameter("Message", logEvent.GetFullMessage()));
                command.Parameters.Add(new SqlParameter("When", logEvent.When > SqlDateTime.MinValue
                                                                    ? logEvent.When
                                                                    : (DateTime?)null));
                command.Parameters.Add(new SqlParameter("Entry", _eventId));
                command.ExecuteNonQuery();
            }
        }

        private void CreateLogEntry()
        {
            if (_stopwatch == null)
                _stopwatch = new Stopwatch();
            _stopwatch.Stop();

            using (var connection = new SqlConnection(_configuration.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("CreateLogEntry", connection) { CommandType = CommandType.StoredProcedure})
                {
                    command.Parameters.Add(new SqlParameter("Username", ServiceSecurityContext.Current.WindowsIdentity.Name));
                    command.Parameters.Add(new SqlParameter("Action", _action ?? ""));
                    command.Parameters.Add(new SqlParameter("ElapsedTime", Convert.ToInt32(_stopwatch.ElapsedMilliseconds)));
                    command.Parameters.Add(new SqlParameter("MachineName", Environment.MachineName));
                    command.Parameters.Add(new SqlParameter("ManagedThreadId", Thread.CurrentThread.ManagedThreadId.ToString()));
                    command.Parameters.Add(new SqlParameter("ProcessId", Process.GetCurrentProcess().Id.ToString()));
                    command.Parameters.Add(new SqlParameter("ProcessName", Process.GetCurrentProcess().ProcessName));

                    _eventId = Convert.ToInt64(command.ExecuteScalar());
                }
            }
        }
    }

    public class LogEvent
    {
        public string Category { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public DateTime When { get; set; }

        public string GetFullMessage()
        {
            var result = new StringBuilder(Message);
            var currentException = Exception;
            while (currentException != null)
            {
                result.AppendLine(currentException.ToString());
                currentException = currentException.InnerException;
            }

            return new string(result.ToString().Take(8000).ToArray());
        } 
    }
}
