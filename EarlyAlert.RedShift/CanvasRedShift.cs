using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;

namespace EarlyAlert.RedShift
{
    public class CanvasRedShift 
    {
        
        public CanvasRedShift()
        {
            
        }

        public DataSet GetCanvasData(string sql)
        {
            string server = ConfigurationManager.AppSettings["RedShiftServer"];
            string port = ConfigurationManager.AppSettings["RedShiftPort"];
            string masterUsername = ConfigurationManager.AppSettings["RedShiftUserName"];
            string masterUserPassword = ConfigurationManager.AppSettings["RedShiftPassword"];
            string dbName = ConfigurationManager.AppSettings["RedShiftDatabase"];

            try
            {

                string connString = "Driver={Amazon Redshift (x86)};" +
                                    String.Format("Server={0};Database={1};" +
                                                  "UID={2};PWD={3};Port={4};SSL=true;Sslmode=Require",
                                        server, dbName, masterUsername,
                                        masterUserPassword, port);
                DataSet ds = new DataSet();
                using (OdbcConnection conn = new OdbcConnection(connString))
                {
                    conn.Open();
                    
                    OdbcDataAdapter da = new OdbcDataAdapter(sql, conn);
                    da.Fill(ds);
                    conn.Close();
                }
                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

       
    }
}
