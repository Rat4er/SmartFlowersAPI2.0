using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewFlower.Models
{
    public class ResponseDataModel
    {
        public string Id { get; set; }
        public int Flower_Id { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Moisture { get; set; }
        public DateTime TimeMeasurement { get; set; }
    }
}