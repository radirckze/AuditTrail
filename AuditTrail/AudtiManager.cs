using System;
using System.Reflection;

namespace AuditTrail {

    /// <summary>
    /// An abstract AuditManager. 
    /// This class generates the message to be logged. 
    /// Concrete classes must override the LogToStore and Dispose methods.
    /// </summary>
    public abstract class AuditManager : IAuditManager {

        public virtual void LogOperation(string userName, Operation operation,
            object current, object updated = null) {

            // Generate the message to be logged. Then Call(virtual) LogToStore 
            // method to write message to the appropriate store.

            if (String.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("AuditTrail.LogOperaton userName");
            }
            else if (current == null)
            {
                throw new ArgumentException("AuditTrail.LogOperaton current");
            }
            else if (operation == Operation.Update && updated == null)
            {
                throw new ArgumentException("AuditTrail.LogOperaton updated");
            }

            try
            {
                // Construct part of message that is common to all operations.
                string opDetails = String.Format("User:{0}, Operation:{1}, Type:{2}, " +
                        "Time:{3}", userName, Enum.GetName(operation.GetType(),
                        operation), current.GetType(), DateTime.Now.ToShortTimeString());

                if (operation == Operation.Update)
                {
                    // Add the updated property details 
                    opDetails = String.Concat(opDetails, GetObjectUpdateDetails(current, updated));
                }
                else
                {
                    // Add the property details
                    opDetails = String.Concat(opDetails, GetObjectDetails(current));
                }

                // Log to store. 
                this.LogToStore(opDetails);
            }
            catch (Exception ex)
            {
                // handle any unexpected exceptions
                throw ex;
            }
        }

        // Using reflection, create a string listing the updated properties
        private string GetObjectUpdateDetails(object current, object updated)
        {

            string opDetails = ", Updated Properties: ";
            Type typecurrent = current.GetType();
            foreach (PropertyInfo property in typecurrent.GetProperties())
            {
                object currVal = property.GetValue(current);
                object updatedVal = property.GetValue(updated);

                if (!object.Equals(currVal, updatedVal))
                {
                    string currValAsString = currVal == null ? "null" : currVal.ToString();
                    string updatedValAsString = updatedVal == null ? "null" : updatedVal.ToString();
                    string updateDetails = String.Format(" [{0} current: {1}, new: {2}]",
                        property.Name, currValAsString, updatedValAsString);

                    opDetails = String.Concat(opDetails, updateDetails);

                }
            }

            return opDetails;
        }

        // Using reflection, generate a string listing the properties and their values.
        private string GetObjectDetails(object current)
        {
            string opDetails = ", Properties: ";
            Type typecurrent = current.GetType();
            foreach (PropertyInfo property in typecurrent.GetProperties())
            {
                object currVal = property.GetValue(current);
                string currValAsString = currVal == null ? "null" : currVal.ToString();
                string propDetails = String.Format(" [{0} value: {1}]",
                    property.Name, currValAsString);
                opDetails = String.Concat(opDetails, propDetails);
            }

            return opDetails;
        }

        // Virtual log (to persistent store) method.
        // Concrete classes must provide the implementation for this.
        protected virtual void LogToStore(string opDetails)
        {
            throw new NotImplementedException();
        }

        // Virtual dispose. 
        // Concrete classes must provide the implementation for this.
        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

