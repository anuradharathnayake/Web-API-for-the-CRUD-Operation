using AirLine.Models;
using AirLineBussiness;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Xml;

namespace AirLine.Controllers
{
    public class CurrencyController : ApiController
    {
        // GET api/<controller>
        [HttpGet()]
        public IHttpActionResult Get()
        {
            IHttpActionResult ret = null;
            List<CurrencyModel> list = new List<CurrencyModel>();
            CurrencyBL currency = new CurrencyBL();
            list = currency.ReadFile();
            List<CurrencyModel>  dbList = currency.ReadCurrencyFromDB();

            foreach (var item in dbList)
            {
                list.Add(item);
            }
          
            if (list.Count > 0)
            {
                ret = Ok(list);
            }
            else
            {
                ret = NotFound();
            }

            return ret;
        }

        // DELETE api/<controller>/5
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            IHttpActionResult ret = null;

            if (true)
            {
                ret = Ok(true);
            }
            else
            {
                ret = NotFound();
            }

            return ret;
        }

        // POST api/<controller>
        [HttpPost()]
        public IHttpActionResult Post(CurrencyModel currency)
        {
            IHttpActionResult ret = null;

            CurrencyBL currencyBL = new CurrencyBL();
            bool test = currencyBL.SaveCurrency(currency);

            if (true)
            {
                ret = Ok(currency);
            }
            else
            {
                ret = NotFound();
            }

            return ret;
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public IHttpActionResult Put(int id, CurrencyModel currency)
        {
            IHttpActionResult ret = null;

            if (true)
            {
                ret = Ok(currency);
            }
            else
            {
                ret = NotFound();
            }

            return ret;
        }

    }
}
