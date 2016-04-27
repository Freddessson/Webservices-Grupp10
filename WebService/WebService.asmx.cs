using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

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
            catch (System.IO.FileNotFoundException e)
            {

                throw e;
                Console.WriteLine(e.Message);
            }

        }
    }
}
