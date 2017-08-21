using System.Data;
using System.Data.Odbc;

namespace EarlyAlert.Repository
{
    public class CanvasRedShift
    {
        public DataSet GetCanvasData(string sql)
        {
            var ds = new DataSet();
            using (var conn = new OdbcConnection("dsn=Amazon Redshift ODBC DSN"))
            {
                conn.Open();

                var da = new OdbcDataAdapter(sql, conn);
                da.Fill(ds);
                conn.Close();
            }
            return ds;
        }
    }
}
