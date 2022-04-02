
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{   // Todo: Fix
    public class GoogleMapModel
    {

    }
    public class Address
    {
        public string str_address { get; set; }
        public string apt_num { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public double zipcode { get; set; }
    }
    public class Location
    {
        public string typeOfPlace { get; set; }


    }

    public class Parsed
    {
        public Location location { get; set; }
    }
    public class Next
    {
        public string title { get; set; }
        public string href { get; set; }
    }
    public class Links
    {
        public Next next { get; set; }
    }
    
    public class Root
    {
        public string text { get; set; }
        public List<Parsed> parsed { get; set; }
        public List<Hint> hints { get; set; }
        public Links _links { get; set; }
    }
}
