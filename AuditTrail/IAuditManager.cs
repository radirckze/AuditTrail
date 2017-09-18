using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail
{

    public enum Operation { Create, Read, Update, Delete };

    /// <summary>
    /// The interface allows us to support / swap different AuditManager implementations
    /// in an applications
    /// A file based implementation of AuditManager is provided. Could easily
    /// provide / swap an AuditManager that writes to a database. 
    /// </summary>
    public interface IAuditManager : IDisposable
    {
        /// <summary>
        /// Log database operation information
        /// </summary>
        /// <param name="current">The object being created, read, updated or deleted</param>
        /// <param name="updated">If operation is update, the updated object</param>
        void LogOperation(string userName, Operation operation, object current, 
                object updated = null);

        /// <summary>
        /// Async version of LogOperation
        /// </summary>
        Task LogOperationAsync(string userName, Operation operation, object current,
                object updated = null);
    }
}
