using Microsoft.Data.SqlClient;

namespace Ado_net
{

    public static class ConnectionString
    {
        public static string MsSqlConnection => @"Server=192.168.101.97\WIN10NOF;Database=testing;Trusted_Connection=True;TrustServerCertificate=True;";


        //Server=localhost\SQLEXPRESS;Database=testing

        //"Server=192.168.101.97\WIN10NOF;Database=testing;User ID=admin;Password=123"

    }


}
