using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewFlower.Models
{
    public class GetDataModel
    {
        public int          Id { get; set; }
        public int          Flower_id { get; set; }
        public string       Date { get; set; }
        public float        Temperature { get; set; }
        public float          Moisture { get; set; }
        public float        Humidity { get; set; }
        public DateTime     First_data { get; set; }
        public DateTime     Second_data { get; set; }
        public DateTime     Day { get; set; }
    }
}