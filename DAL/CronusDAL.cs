using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CronusDAL
    {
        
        public static List<List<string>> GetCronusEmployees()
        {
            DBUtil conn = new DBUtil();
            SqlConnection myConnection = conn.CRONUSDBConnection();
            List<List<string>> GetCronusEmployees = new List<List<string>>();

            List<string> columnData = new List<string>();
            List<string> FirstName = new List<string>();
            List<string> LastName = new List<string>();
            List<string> JobTitle = new List<string>();
            

            
            try
            {
                using (myConnection)
                {
                    string sql = "SELECT TOP 1000 * FROM[CRONUS Sverige AB$Employee]";
                    //SqlCommand cmd = new SqlCommand(sql, myConnection);
                    /*SELECT TOP 1000 * FROM[CRONUS Sverige AB$Employee]
                    SqlCommand cmd = new SqlCommand(sql, myConnection);
                    cmd.Parameters.Add("@pnr", SqlDbType.VarChar).Value = c.pnr;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = c.name;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = c.email;
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar, 50).Value = c.phone;
                    cmd.Parameters.Add("@address", SqlDbType.VarChar, 50).Value = c.address;*/

                    using (SqlCommand cmd = new SqlCommand(sql, myConnection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                List<string> row = new List<string>();
                                row.Add(reader.GetString(1));
                                row.Add(reader.GetString(2));

                                GetCronusEmployees.Add(row);

                                /*columnData.Add(reader.GetString(1));
                                columnData.Add(reader.GetString(2));
                                columnData.Add(reader.GetString(3));
                                columnData.Add(reader.GetString(4));*/
                            }
                        }
                    }
                    
                    return GetCronusEmployees;

                    /*cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();*/
                }
              

            }
            catch (SqlException e)
            {
                //ERROR
                Console.Write("Kunde inte hämta kunder.");
                return null;
            }
            
            conn.CloseCRONUSConn(myConnection);
        }
    }
}
