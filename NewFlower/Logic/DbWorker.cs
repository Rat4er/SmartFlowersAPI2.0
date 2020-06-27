using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net;
using NewFlower.Models;
using System.Diagnostics;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace NewFlower.Logic
{
    public class DbWorker
    {
        DataContext db { get { return new DataContext(); } }
        
        internal Measurements Insert(GetDataModel getModel)
        {
            DataContext db = this.db;
            Measurements measurement = new Measurements()
            {
                Id = Guid.NewGuid(),
                Flower_Id = getModel.Flower_id,
                Temperature = getModel.Temperature,
                Humidity = getModel.Humidity,
                Moisture = getModel.Moisture,
                TimeMeasurement = DateTime.Now
            };
            
            db.Measurements.Add(measurement);
            try
            {
                db.SaveChanges();
                return measurement;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return new Measurements { };
                throw;

            }
        }

        public List<ResponseDataModel> GetAll()
        {
            
            var measurements = Converter.Convert(db.Measurements);
            return measurements;
        }

        public List<ResponseDataModel> GetByFlowerId(GetDataModel getModel)
        {
            
            var tempMeasurements = db.Measurements
                .Where(c => c.Flower_Id == getModel.Flower_id)
                .OrderBy(o => o.TimeMeasurement);
            return Converter.Convert(tempMeasurements);
        }

        public List<ResponseDataModel> GetByTime(GetDataModel getModel)
        {
            
            var tempMeasurements = db.Measurements
                .Where(c => c.TimeMeasurement >= getModel.First_data && c.TimeMeasurement <= getModel.Second_data)
                .OrderBy(o => o.TimeMeasurement);
            return Converter.Convert(tempMeasurements);
        }

        public List<ResponseDataModel> GetByDay(GetDataModel getModel)
        {
            
            var tempMeasurements = db.Measurements
                .Where(c => DbFunctions.TruncateTime(c.TimeMeasurement) == getModel.Day.Date)
                .OrderBy(o => o.TimeMeasurement);
            return Converter.Convert(tempMeasurements);
        }

        public List<ResponseDataModel> GetByFlowerIdAndTime(GetDataModel getModel)
        {
            
            var tempMeasurements = db.Measurements
                .Where(c => (c.Flower_Id == getModel.Flower_id) && (c.TimeMeasurement >= getModel.First_data && c.TimeMeasurement <= getModel.Second_data))
                .OrderBy(o => o.TimeMeasurement);
            return Converter.Convert(tempMeasurements);
        }

        public List<ResponseDataModel> GetByFlowerIdAndDay(GetDataModel getModel)
        {
            
            var tempMeasurements = db.Measurements
                .Where(c => (c.Flower_Id == getModel.Flower_id) && (DbFunctions.TruncateTime(c.TimeMeasurement) == getModel.Day.Date))
                .OrderBy(o => o.TimeMeasurement);
            return Converter.Convert(tempMeasurements);
        }
    }
}