using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _5_1_stackoverflow.data
{
    public class StackContextFactory : IDesignTimeDbContextFactory<StackContext>
    {
        public StackContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}5-1 stackoverflow.web"))
                 .AddJsonFile("appsettings.json")
                 .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();
            return new StackContext(config.GetConnectionString("ConStr"));
        }
    }
}
