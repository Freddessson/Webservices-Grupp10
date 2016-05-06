using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DAL;

namespace WebService
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://grupp10/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string GetContent(string filename)
        {
            try
            {
                string content = System.IO.File.ReadAllText(@"C:\Users\OttoF\Desktop\" + filename);
                if (content != null)
                {
                    return content;
                }
                else
                {
                    string message = "The File was empty, or we could not find it.";
                    return message;
                }
            }
            catch (Exception e)
            {


                string message = "Choose a file!";
                return message;

            }

        }
        [WebMethod]
        public List<Customer> FindAllCustomers()
        {
            List<Customer> customerList = new List<Customer>();
            customerList = CobraCustomer.FindAllCustomers();

            return customerList;
        }

        [WebMethod]
        public List<List<string>> GetCronusEmployees()
        {
            List<List<string>> columnData = new List<List<string>>();
            columnData = CronusDAL.GetCronusEmployees();
            return columnData;
        }
        [WebMethod]
        public List<List<string>> GetCronusMetadata()
        {
            List<List<string>> columnData = new List<List<string>>();
            columnData = CronusDAL.GetCronusMetadata();
            return columnData;
        }
        [WebMethod]
        public List<List<string>> GetCronus(string parameter)
        {
            List<List<string>> columnData = new List<List<string>>();
            columnData = CronusDAL.GetCronus(parameter);
            return columnData;
        }
        [WebMethod]
        public void CreateEmployee(string NO_, string FN, string LN, string JT)
        {
            CronusDAL.CreateEmployee(NO_, FN, LN, JT);
        }
        [WebMethod]
        public List<List<string>> GetAllEmployees()
        {
            return CronusDAL.GetAllEmployees();
        }
        [WebMethod]
        public void UpdateEmployee(string NO_, string FN, string LN, string JT)
        {
            CronusDAL.UpdateEmployee(NO_, FN, LN, JT);
        }
        [WebMethod]
        public void DeleteEmployee(string NO_)
        {
            CronusDAL.DeleteEmployee(NO_);
        }

    }
}
