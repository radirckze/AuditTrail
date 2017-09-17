using AuditTrail;
using WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/student")]
    public class StudentController : Controller
    {
        private IWebApiContext webApiContext = null;
        private IAuditManager auditManager = null;

        // The IWebApicontext and IAuditManager classes will be provided by the
        // ASP DI framework.
        StudentController(WebApiContext webApiContext, IAuditManager auditManager) 
        {
            if (webApiContext == null || auditManager == null)
            {
                throw new ArgumentException();
            }

            this.webApiContext = webApiContext;
            this.auditManager = auditManager;
        }

        // create a new student
        [HttpPost]
        public void Add(Student student)
        {
            // In a real implementation the controller would not directly
            // access the EF classes (i.e., webApiContext). The DAL
            // would be implemented as a service layer that is called from 
            // within the controller, masking the data access / buinsess logic.
            // So adding a student and then logging the operation part would
            // be moved to the DAL layer. 

            try
            {
                string userId = "user001"; //get user based on implementation
                webApiContext.Students.Add(student);
                webApiContext.Save();

                // log the create operation.
                auditManager.LogOperation(userId, Operation.Create, student);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
