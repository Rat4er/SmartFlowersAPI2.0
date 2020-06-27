using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Configuration;
using System.Web.Http;
using Newtonsoft.Json;
using NewFlower.Logic;
using NewFlower.Models;
using System.Web.Http.Description;

namespace NewFlower.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpPost]
        [Route("api/insert_data")]
        [ResponseType(typeof(Measurements))]
        public HttpResponseMessage Post(GetDataModel json, HttpRequestMessage request = null)
        {
            try
            {
                //AddDataModel addData = JsonConvert.DeserializeObject<AddDataModel>(json.ToString());
                DbWorker db = new DbWorker();
                return request.CreateResponse(HttpStatusCode.OK, db.Insert(json));
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.InternalServerError, "An error has been occured");
            }
        }

        [HttpPost]
        [Route("api/search_bup")]
        [ResponseType(typeof(List<ResponseDataModel>))]
        public HttpResponseMessage SearchBup(GetDataModel json, HttpRequestMessage requestMessage = null)
        {
            DbWorker db = new DbWorker();
            //try
            //{
            //по flower_id
                if (json.First_data == Convert.ToDateTime("01.01.0001 0:00:00") && json.Second_data == Convert.ToDateTime("01.01.0001 0:00:00") && json.Day == Convert.ToDateTime("01.01.0001 0:00:00") && json.Flower_id != 0)
                    return requestMessage.CreateResponse(HttpStatusCode.OK, db.GetByFlowerId(json));
                //по дню
                if (json.First_data == Convert.ToDateTime("01.01.0001 0:00:00") && json.Second_data == Convert.ToDateTime("01.01.0001 0:00:00") && json.Day != Convert.ToDateTime("01.01.0001 0:00:00") && json.Flower_id == 0)
                    return requestMessage.CreateResponse(HttpStatusCode.OK, db.GetByDay(json));
                //по промежутку
                if (json.First_data != Convert.ToDateTime("01.01.0001 0:00:00") && json.Second_data != Convert.ToDateTime("01.01.0001 0:00:00") && json.Day == Convert.ToDateTime("01.01.0001 0:00:00") && json.Flower_id == 0)
                    return requestMessage.CreateResponse(HttpStatusCode.OK, db.GetByTime(json));
                //по промежутку и айди
                if (json.First_data != Convert.ToDateTime("01.01.0001 0:00:00") && json.Second_data != Convert.ToDateTime("01.01.0001 0:00:00") && json.Day == Convert.ToDateTime("01.01.0001 0:00:00") && json.Flower_id != 0)
                    return requestMessage.CreateResponse(HttpStatusCode.OK, db.GetByFlowerIdAndTime(json));
                //по дню и айди
                if (json.First_data == Convert.ToDateTime("01.01.0001 0:00:00") && json.Second_data == Convert.ToDateTime("01.01.0001 0:00:00") && json.Day != Convert.ToDateTime("01.01.0001 0:00:00") && json.Flower_id != 0)
                return requestMessage.CreateResponse(HttpStatusCode.OK, db.GetByFlowerIdAndDay(json));
            return requestMessage.CreateResponse(HttpStatusCode.NotFound);
            //}

            //catch { return requestMessage.CreateResponse(HttpStatusCode.InternalServerError, "An error has been occured"); }
        }

        [HttpPost]
        [Route("api/getall")]
        [ResponseType(typeof(List<ResponseDataModel>))]
        public HttpResponseMessage GetAll(HttpRequestMessage requestMessage = null)
        {
            try
            {
                DbWorker db = new DbWorker();
                return requestMessage.CreateResponse(HttpStatusCode.OK, db.GetAll());
            }
            catch
            {
                return requestMessage.CreateResponse(HttpStatusCode.InternalServerError, "An error has been occured");
            }
        }

    }
}
