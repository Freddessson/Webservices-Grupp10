using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Customer
    {
        public Customer()
        {
        }
        public Customer(string name, string pnr, string email, string phone, string address)
        {
            this.name = name;
            this.pnr = pnr;
            this.email = email;
            this.phone = phone;
            this.address = address;
        }
        public string name { get; set; }
        public string pnr { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
    }
}
