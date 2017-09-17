using WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Model
{
    //The interface for the DAL
    public interface IWebApiContext
    {

        DbSet<Student> Students { get; set; }

        void Save();
    }
}
