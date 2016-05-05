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

            try
            {
                using (myConnection)
                {
                    string sql = "SELECT TOP 1000 * FROM[CRONUS Sverige AB$Employee]";
                    
                    using (SqlCommand cmd = new SqlCommand(sql, myConnection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                List<string> row = new List<string>();
                                row.Add(reader.GetString(1));
                                row.Add(reader.GetString(2));
                                row.Add(reader.GetString(4));
                                row.Add(reader.GetString(6));
                                row.Add(reader.GetString(8));

                                GetCronusEmployees.Add(row);

                            }
                        }
                    }
                    
                    return GetCronusEmployees;
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

        public static List<List<string>> GetCronusMetadata()
        {
            DBUtil conn = new DBUtil();
            SqlConnection myConnection = conn.CRONUSDBConnection();
            List<List<string>> GetCronusMetadata = new List<List<string>>();

            try
            {
                using (myConnection)
                {
                    string sql = "SELECT TOP 1000 * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE '%' CRONUS Sverige AB$Employee";

                    using (SqlCommand cmd = new SqlCommand(sql, myConnection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                List<string> row = new List<string>();
                                row.Add(reader.GetString(0));
                                row.Add(reader.GetString(1));
                                row.Add(reader.GetString(2));
                                row.Add(reader.GetString(3));
                                row.Add(reader.GetString(4));

                                GetCronusMetadata.Add(row);

                            }
                        }
                    }

                    return GetCronusMetadata;
                }
            }
            catch (SqlException e)
            {
                //ERROR
                Console.Write("Kunde inte hämta metadata.");
                return null;
            }

            conn.CloseCRONUSConn(myConnection);
        }

        public static List<List<string>> GetCronus(string parameter)
        {
            DBUtil conn = new DBUtil();
            SqlConnection myConnection = conn.CRONUSDBConnection();
            List<List<string>> GetCronus = new List<List<string>>();
            string sql = "";
            int max = 9;
            int count = 0;
            string parameter1 = "";
            string parameter2 = "";
            /*if (parameter.Equals("employees"))
            {
                count = 1;
                //Kolla över!
                sql = "SELECT TOP 1000 * FROM[CRONUS Sverige AB$Employee]";
            }*/
            if (parameter.Equals("metadata/employee"))
            {
                //parameter1 = "Employee";
                //sql = "SELECT * FROM [CRONUS Sverige AB$Employee], INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE '%Employee%'";
                sql = "SELECT No_, [First Name], [Last Name], [Job Title], IS_NULLABLE, DATA_TYPE FROM[CRONUS Sverige AB$Employee], INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE '%Employee%'";
                max = 6;
            }
            else if (parameter.Equals("relatives"))
            {
                max = 6;
                //sql = "SELECT TOP 1000 * FROM[CRONUS Sverige AB$Employee]";
                sql = "SELECT [Employee No_], [CRONUS Sverige AB$Employee].[First Name], [CRONUS Sverige AB$Employee].[Last Name], [CRONUS Sverige AB$Employee Relative].[Relative Code], [CRONUS Sverige AB$Employee Relative].[First Name], [CRONUS Sverige AB$Employee Relative].[Last Name]  FROM [CRONUS Sverige AB$Employee], [CRONUS Sverige AB$Employee Relative]";
            }
            else if (parameter.Equals("sickEmployees"))
            {
                //sql = "SELECT[Employee No_], [First Name], [Last Name], [From Date], Description, Quantity, [Unit of Measure Code] FROM[CRONUS Sverige AB$Employee Absence], [CRONUS Sverige AB$Employee] WHERE [From Date] LIKE '%' + @parameter1 + '%' AND Description LIKE @parameter2";
                sql ="SELECT [Employee No_], [First Name], [Last Name], Description FROM[CRONUS Sverige AB$Employee Absence], [CRONUS Sverige AB$Employee] WHERE[From Date] LIKE '%2004%' AND Description = 'Sjuk'";
                max = 4;
                //parameter1 = "2004";
                //parameter2 = "sjuk";
            }
            else if (parameter.Equals("sickEmployeesFirstName"))
            {
                //sql = "SELECT [CRONUS Sverige AB$Employee Absence].[Employee No_], COUNT(Quantity) as 'Antal sjuktimmar' FROM[CRONUS Sverige AB$Employee Absence], [CRONUS Sverige AB$Employee] WHERE Description LIKE 'Sjuk' GROUP BY[CRONUS Sverige AB$Employee Absence].[Employee No_]";
                sql = "SELECT[CRONUS Sverige AB$Employee].[First Name] FROM[CRONUS Sverige AB$Employee] JOIN[CRONUS Sverige AB$Employee Absence] ON[CRONUS Sverige AB$Employee Absence].[Employee No_] =[CRONUS Sverige AB$Employee].No_ WHERE Description LIKE 'Sjuk' GROUP BY[CRONUS Sverige AB$Employee Absence].[Employee No_], [CRONUS Sverige AB$Employee].[First Name] ORDER BY COUNT(Quantity) DESC";
                max = 1;
            }
            else if (parameter.Equals("allKeys"))
            {
                sql = "SELECT TABLE_NAME, COLUMN_NAME  FROM .INFORMATION_SCHEMA.KEY_COLUMN_USAGE";
                max = 2;

            }
            else if (parameter.Equals("allIndexes"))
            {
                sql = "SELECT CAST((object_id) AS VARCHAR(255)), CAST((index_id) AS VARCHAR(255)) FROM sys.indexes";
                max = 2;
            }
            else if (parameter.Equals("allTableConstraints"))
            {
                sql = "SELECT CONSTRAINT_NAME FROM.INFORMATION_SCHEMA.TABLE_CONSTRAINTS";
                max = 1;
            }
            else if (parameter.Equals("allTables"))
            {
                sql = "SELECT TABLE_NAME FROM.INFORMATION_SCHEMA.TABLES";
                max = 1;

            }
            else if (parameter.Equals("allTables2"))
            {
                sql = "SELECT name FROM sysobjects WHERE xtype = 'U'";
                max = 1;
            }
            else if (parameter.Equals("allColumnsEmployees"))
            {
                sql = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'CRONUS Sverige AB$Employee'";
                max = 1;
            }
            else if (parameter.Equals("allColumnsEmployees2"))
            {
                sql = "SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('CRONUS Sverige AB$Employee')";
                max = 1;
            }
            else if (parameter.Equals("wrong"))
            {
                return GetCronus;
            }
            try
            {
                using (myConnection)
                {
                    Console.WriteLine(sql);
                    using (SqlCommand cmd = new SqlCommand(sql, myConnection))
                    {
                        cmd.Parameters.Add("@parameter1", SqlDbType.VarChar).Value = parameter1;
                        cmd.Parameters.Add("@parameter2", SqlDbType.VarChar).Value = parameter2;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                List<string> row = new List<string>();

                                for (int i = count; i < max; i++)
                                {
                                    
                                    
                                    row.Add(reader.GetString(i));
                                    

                                }
                                GetCronus.Add(row);

                                /*if (count == 123123)
                                {
                                    List<string> row = new List<string>();
                                    row.Add(reader.GetString(0));
                                    row.Add(reader.GetString(1));
                                    row.Add(reader.GetString(2));
                                    row.Add(reader.GetString(3));
                                    row.Add(reader.GetString(4));

                                    GetCronus.Add(row);
                                }
                                if (count == 5)
                                {
                                    List<string> row = new List<string>();
                                    for (int i = 0; i < count; i++)
                                    {
                                    row.Add(reader.GetString(i));
                                    
                                    }
                                    GetCronus.Add(row);
                                }
                                if (parameter.Equals("employees"))
                                {
                                    List<string> row = new List<string>();
                                    row.Add(reader.GetString(1));
                                    row.Add(reader.GetString(2));
                                    row.Add(reader.GetString(3));
                                    row.Add(reader.GetString(4));
                                    row.Add(reader.GetString(5));
                                    row.Add(reader.GetString(6));
                                    row.Add(reader.GetString(7));
                                    row.Add(reader.GetString(8));
                                    row.Add(reader.GetString(9));

                                    GetCronus.Add(row);
                                }*/



                                /*else
                                { 
                                List<string> row = new List<string>();
                                row.Add(reader.GetString(0));
                                row.Add(reader.GetString(1));
                                row.Add(reader.GetString(2));
                                row.Add(reader.GetString(3));
                                row.Add(reader.GetString(4));
                                row.Add(reader.GetString(5));
                                row.Add(reader.GetString(6));
                                row.Add(reader.GetString(7));
                                row.Add(reader.GetString(8));
                                row.Add(reader.GetString(9));

                                GetCronus.Add(row);
                            }*/
                            }
                        }
                    }

                    return GetCronus;
                }
            }
            catch (SqlException e)
            {
                //ERROR
                Console.Write("Kunde inte hämta data.");
                return null;
            }

            conn.CloseCRONUSConn(myConnection);
        }
    }
}
