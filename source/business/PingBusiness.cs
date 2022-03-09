using Microsoft.Extensions.Configuration;

namespace NewEnglandClassic;

public class PingBusiness
{
    private readonly Database.IDataContext _database;

    public PingBusiness(IConfiguration config)
    {
        _database = new Database.DataContext(config);
    }

    public bool Database()
        => _database.Ping();
}
