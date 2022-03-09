using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NewEnglandClassic;

internal class DataContext : DbContext, IDataContext
{
    private readonly string _connectionString;
    internal DataContext(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("tournament-manager");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString, options => options.EnableRetryOnFailure(3, new TimeSpan(0, 0, 1), new List<int>()));

#if DEBUG
        optionsBuilder.UseLoggerFactory(ConsoleLogger);
        optionsBuilder.EnableSensitiveDataLogging(true);
#endif
    }

#if DEBUG
    private static readonly ILoggerFactory ConsoleLogger = LoggerFactory.Create(builder => builder.AddConsole());
#endif

    bool IDataContext.Ping()
        => Database.CanConnect();
}

internal interface IDataContext
{
    bool Ping();
}