using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SampleApp.Models
{
    public class SampleAppContext : DbContext
    {

        public SampleAppContext(DbContextOptions<SampleAppContext> options)
            : base(options)
        { }
   

        public DbSet<Student> Students { get; set; }
   
    }
}
