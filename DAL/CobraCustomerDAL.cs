using System;
using Model;
using System.Collections.Generic;
using DAL;
using System.Data.SqlClient;



namespace DAL
{
    public class CobraCustomer
    {

        public static List<Customer> FindAllCustomers()
        {
            DBUtil conn = new DBUtil();
            SqlConnection myConnection = conn.Connection();
            try
            {

                List<Customer> customerList = new List<Customer>();
                SqlDataReader myReader = null;
                SqlCommand cmd = new SqlCommand("SELECT * FROM customer", myConnection);
                myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    Customer c = new Customer();
                    c.pnr = myReader["pnr"].ToString();
                    c.name = myReader["name"].ToString();
                    c.email = myReader["email"].ToString();
                    c.phone = myReader["phone"].ToString();
                    c.address = myReader["address"].ToString();


                    customerList.Add(c);
                }



                return customerList;
                //Close connection to DB.
                //conn.closeConn(myConnection);

            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }
    }
}

