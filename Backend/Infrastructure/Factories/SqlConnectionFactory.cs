using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Factories;

public class SqlConnectionFactory
{
    private readonly string _connectionFactory;

    public SqlConnectionFactory(string connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionFactory);
    }
}
