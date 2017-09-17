using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Model
{
    // The Entity Frameowrk based DAL repository implementation for Student
    public class WebApiContext : DbContext, IWebApiContext
    {
        public virtual DbSet<Student> Students { get; set; }

        public void Save()
        {
            this.Save();
        }
    }
}
