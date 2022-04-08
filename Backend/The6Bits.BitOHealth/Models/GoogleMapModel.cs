
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    public class GoogleMapModel2 
    {
        public List<Location> Locations { get; set; }
        public SelectList? LocationType { get; set; }
        //public string? LocationType { get; set; }
        public string? SearchString { get; set; }
    }

}
