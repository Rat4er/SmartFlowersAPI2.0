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
            NameComparer nt = new NameComparer();
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
                    TimeMeasurement = measurement.TimeMeasurement
                };
                responseDataModel.Add(temp);
            }
            responseDataModel.Sort(nt);
            return responseDataModel;
        }
    }

    class NameComparer : IComparer<ResponseDataModel>
    {
        public int Compare(ResponseDataModel o1, ResponseDataModel o2)
        {
            if (o1.TimeMeasurement.Ticks > o2.TimeMeasurement.Ticks)
            {
                return 1;
            }
            else if (o1.TimeMeasurement.Ticks < o2.TimeMeasurement.Ticks)
            {
                return -1;
            }

            return 0;
        }
    }
}