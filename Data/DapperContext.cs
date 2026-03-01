using Microsoft.Data.SqlClient;
using System.Data;

namespace SafnamBackend.Data;

public class DapperContext
{
    private readonly IConfiguration _config;

    public DapperContext(IConfiguration config)
    {
        _config = config;
    }

    public IDbConnection CreateConnection()
    => new SqlConnection(_config.GetConnectionString("DefaultConnection"));
}
