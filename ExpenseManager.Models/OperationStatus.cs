﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    [DebuggerDisplay("Status: {Status}")]
    public class OperationStatus
    {
        public bool Status { get; set; }
        public bool IsSystemException { get; set; }
        public int RecordsAffected { get; set; }
        public string Message { get; set; }
        public Object OperationID { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public string ExceptionInnerMessage { get; set; }
        public string ExceptionInnerStackTrace { get; set; }

        public static OperationStatus CreateFromUserException(string message)
        {
            return new OperationStatus
            {
                Status = false,
                Message = message,
                OperationID = null,
                IsSystemException = false
            };
        }

        public static OperationStatus CreateFromSystemException(string message, Exception ex)
        {
            OperationStatus opStatus = new OperationStatus
            {
                Status = false,
                Message = message,
                OperationID = null,
                IsSystemException = true
            };

            if (ex != null)
            {
                opStatus.ExceptionMessage = ex.Message;
                opStatus.ExceptionStackTrace = ex.StackTrace;
                opStatus.ExceptionInnerMessage = (ex.InnerException == null) ? null : ex.InnerException.Message;
                opStatus.ExceptionInnerStackTrace = (ex.InnerException == null) ? null : ex.InnerException.StackTrace;
            }
            return opStatus;
        }
    }
}
