using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SchoolContext>
    {
        //SchoolDbContext 代表的是你的创建失败的那个类型
        public SchoolContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer("Data Source=.;Initial Catalog=school;User Id=sa;Password=pearsink;");
            return new SchoolContext(builder.Options);
        }
    }

}
