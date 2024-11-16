using Microsoft.Data.SqlClient;

namespace Ado_net
{

    public static class ConnectionString
    {
        public static string MsSqlConnection => @"Server=localhost\SQLEXPRESS;Database=testing;Trusted_Connection=True;TrustServerCertificate=True;";

        


        //"Server=192.168.1.119/WIN10NOF;Database=testing;User ID=admin;Password=123"

    }


}
