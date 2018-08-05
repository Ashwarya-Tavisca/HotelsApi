using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotels.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfAvailableRooms { get; set; }
        public string Address { get; set; }
        public string LocationCode { get; set; }
    }
}