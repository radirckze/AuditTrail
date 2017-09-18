using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail
{
    /// <summary>
    /// Concrete AuditManager class that appends messages to a text file
    /// </summary>
    public class FileAuditManager : AuditManager
    {
        // File name is hard-coded. Ideally this should be a confiuration option.
        string fileName = "AuditTrail.txt";
        StreamWriter streamWriter = null;

        public FileAuditManager() : base()
        {
            streamWriter = File.AppendText(fileName);
            streamWriter.AutoFlush = true; 
        }

        // This method will append operation details to the text file
        protected override void LogToStore(AuditRecord auditRecord)
        {
            streamWriter.WriteLine(auditRecord.ToString());
        }

        //async version of LogToStore.
        protected override async Task LogToStoreAsync(AuditRecord auditRecord)
        {
            await streamWriter.WriteLineAsync(auditRecord.ToString());
        }

        // dispose the stream writer. 
        public override void Dispose()
        {
            streamWriter.Dispose();
        }
    }
}
