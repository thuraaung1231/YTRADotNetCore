using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTRADotNetCore.ConsoleApp.Models;

namespace YTRADotNetCore.ConsoleApp
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=THURA\\MSSQLSERVER2;Initial Catalog=DotNetTrainingBatch5;User Id=sa;Password=sasa;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<BlogDataModel> blogData { get; set; }
    }
}