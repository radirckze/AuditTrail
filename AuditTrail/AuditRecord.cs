using System;
using System.Collections.Generic;
using System.Text;

namespace AuditTrail
{
    public class AuditRecord
    {

        public string User { get; set; }

        public Operation Operation { get; set; }

        public Type ObjectType { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Details { get; set; }

        public AuditRecord(string user, Operation operation, 
                Type objectType, DateTime timestamp)
        {
            this.User = user;
            this.Operation = operation;
            this.ObjectType = objectType;
            this.TimeStamp = timestamp;
        }

        public override string ToString()
        {
            string theStr = String.Format("User:{0}, Operation:{1}, Type:{2}, " +
                        "Time:{3}", this.User, Enum.GetName(this.Operation.GetType(),
                        this.Operation), this.ObjectType, 
                        this.TimeStamp.ToString("MM:dd:yy HH:mm:ss"));

            if (this.Details != null)
            {
                theStr = String.Concat(theStr, this.Details);
            }

            return theStr;
        }


    }
}
