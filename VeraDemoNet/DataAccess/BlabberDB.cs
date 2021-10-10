using System.Data.Entity;
using System.Configuration;

namespace VeraDemoNet.DataAccess
{
    public class BlabberDB : DbContext  
    {
       
        public BlabberDB() : base(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString)  
        {  
        }  
  
        protected override void OnModelCreating(DbModelBuilder modelBuilder)  
        {  
            modelBuilder.Entity<User>().HasKey(x=>x.UserName);
        }


  
        public DbSet<User> Users { get; set; }  
    }  
}