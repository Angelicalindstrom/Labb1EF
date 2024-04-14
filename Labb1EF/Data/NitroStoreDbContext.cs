using Labb1EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Labb1EF.Data
{
    public class NitroStoreDbContext : DbContext
    {
        public NitroStoreDbContext(DbContextOptions<NitroStoreDbContext> options) : base(options) 
        {

        }
        // Här döper jag databaserna i SSMS berättar vad systemet har att ladda upp
        public DbSet<Employee> Employees { get; set; }
        public DbSet <Role> Roles { get; set; }
        public DbSet<Reason> Reasons { get; set; } /*= new DbSet<Reason>();?? VAD GÖR DENNA?*/
        public DbSet <Application> Applications { get; set; }
    }
}
