using AzureB2C.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureB2C.API.Services
{
    public class DatabaseContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "AzureB2CDB");
        }
    }
}
