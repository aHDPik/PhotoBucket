using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoBucket.Entities
{
    public class PhotoContext:DbContext
    {
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<UserPhoto> Photos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PhotoDb;Trusted_Connection=True;");
        }

        public PhotoContext()
        {
            Database.EnsureCreated();
        }
    }
}
