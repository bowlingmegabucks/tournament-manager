using Microsoft.Extensions.Configuration;

namespace NewEnglandClassic;

public class PingBusiness
{
    private readonly IDataContext _database;

    public PingBusiness(IConfiguration config)
    {
        _database = new DataContext(config);
    }

    public bool DatabaseAsync()
        => _database.Ping();
}
