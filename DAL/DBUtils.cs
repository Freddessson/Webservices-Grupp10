using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
    public class DBUtil
    {
        public SqlConnection Connection()
        {

            SqlConnection myConnection = new SqlConnection("user id=admin;" +
                                           "password=hejsan;Server=localhost;" +
                                           "Trusted_Connection=yes;" +
                                           "database=cobrahotel; " +
                                           "connection timeout=30");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            return myConnection;
        }

        public void CloseConn(SqlConnection myConnection)
        {
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        internal void CloseConn()
        {
            throw new NotImplementedException();
        }
    }
}
