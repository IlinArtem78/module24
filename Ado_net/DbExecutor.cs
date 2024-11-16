using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado_net
{
    public class DbExecutor
    {
        private MainConnector connector;
        public DbExecutor(MainConnector connector)
        {
            this.connector = connector;
        }


        //работа с отсоединенной моделью Ado.net
        public DataTable SelectAll(string table)
        {

            var ds = new DataSet();
            var selectcommandtext = "select * from " + table;
            var adapter = new SqlDataAdapter(
              selectcommandtext,
              connector.GetConnection()
            );
            adapter.Fill(ds);
            return ds.Tables[0];
        }
    }
}
