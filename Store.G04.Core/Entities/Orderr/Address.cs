using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Entities.Orderr
{
    public class Address
    {
        public Address()
        {
        }

        public Address(string fName, string lName, string city, string steet, string country)
        {
            FName = fName;
            LName = lName;
            City = city;
            Steet = steet;
            Country = country;
        }

        public string FName  { get; set; }
        public string LName { get; set; }

        public string City { get; set; }
        public string Steet {  get; set; }
        
        public string Country { get; set; }



    }
}
