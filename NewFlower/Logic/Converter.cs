using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewFlower.Models;

namespace NewFlower.Logic
{
    public class Converter
    {
        public static List<ResponseDataModel> Convert(IQueryable<Measurements> measurements)
        {
            List<ResponseDataModel> responseDataModel = new List<ResponseDataModel>();
            foreach(var measurement in measurements)
            {
                var temp = new ResponseDataModel
                {
                    Id = measurement.Id.ToString().ToUpper(),
                    Flower_Id = measurement.Flower_Id,
                    Temperature = measurement.Temperature,
                    Humidity = measurement.Humidity,
                    Moisture = measurement.Moisture,
                    TimeMeasurement = measurement.TimeMeasurement.ToString("yyyy-MM-dd HH:mm:ss")
                };
                responseDataModel.Add(temp);
            }
            return responseDataModel;
        }
    }
}