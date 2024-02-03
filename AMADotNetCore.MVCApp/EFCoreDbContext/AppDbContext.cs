using AMADotNetCore.MVCApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMADotNetCore.MVCApp.EFCoreDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        //    {
        //        DataSource = ".",
        //        InitialCatalog = "AMADotNetCore",
        //        UserID = "sa",
        //        Password = "sa@123",
        //        TrustServerCertificate= true
        //    };
        //    optionsBuilder.UseSqlServer(_sqlConnectionStringBuilder.ConnectionString);
        //}


        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
