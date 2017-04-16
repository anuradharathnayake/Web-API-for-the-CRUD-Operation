
using AirLine.Models;
using AirLineData;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace AirLineBussiness
{
    public class CurrencyBL
    {
        // Read currency Moke Data 
        public List<CurrencyModel> ReadFile()
        {

            string filePath = HttpContext.Current.Request.MapPath("~/File/Currency.xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/data");

            List<CurrencyModel> currencys = new List<CurrencyModel>();

            foreach (XmlNode node in nodes)
            {
                CurrencyModel currency = new CurrencyModel();

                currency.ID = Convert.ToInt32(node.SelectSingleNode("CurrencyId").InnerText);
                currency.Name = node.SelectSingleNode("Name").InnerText;
                currency.Country = node.SelectSingleNode("Country").InnerText;
                currency.Value = Convert.ToInt32(node.SelectSingleNode("Value").InnerText);

                currencys.Add(currency);
            }

            return currencys;
        }

        public List<CurrencyModel> ReadCurrencyFromDB()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ExchangeRate, CurrencyModel>();
            });


            List<CurrencyModel> currencyModelList = new List<CurrencyModel>();

            using (AirLineEntities entities = new AirLineEntities())
            {
               var currencyList = (from customer in entities.ExchangeRates select customer).ToList();

                foreach (var item in currencyList)
                {
                    CurrencyModel model = Mapper.Map<ExchangeRate, CurrencyModel>(item);
                    currencyModelList.Add(model);
                }
            }

            return currencyModelList;
        }

        public bool SaveCurrency(CurrencyModel currencyModel)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CurrencyModel,ExchangeRate>();
            });

            using (AirLineEntities entities = new AirLineEntities())
            {
                ExchangeRate newRate = new ExchangeRate();
                newRate = Mapper.Map<CurrencyModel, ExchangeRate>(currencyModel);
                entities.ExchangeRates.Add(newRate);
                int result = entities.SaveChanges();

                if (result == 1)
                    return true;
            }
                return false;
        }

        public bool UpdateCurrency(CurrencyModel currencyModel)
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CurrencyModel, ExchangeRate>();
            });

            using (AirLineEntities entities = new AirLineEntities())
            {
                ExchangeRate exchangeRate = (from c in entities.ExchangeRates
                                     where c.ID == currencyModel.ID
                                     select c).FirstOrDefault();
                exchangeRate = Mapper.Map<CurrencyModel, ExchangeRate>(currencyModel);
              //  int result = entities.SaveChanges();

               // ExchangeRate exchangeRate = new ExchangeRate();
                entities.Configuration.ValidateOnSaveEnabled = false;

                exchangeRate.ID = currencyModel.ID;
                entities.Entry(exchangeRate).State = System.Data.Entity.EntityState.Modified;
                entities.SaveChanges();

                entities.Configuration.ValidateOnSaveEnabled = true;
                int result = entities.SaveChanges();

                if (result == 1)
                    return true;
            }

            return false;

        }

        public bool DeleteCurrency(CurrencyModel currencyModel)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CurrencyModel, ExchangeRate>();
            });

            using (AirLineEntities entities = new AirLineEntities())
            {
                ExchangeRate exchangeRate = new ExchangeRate();
                entities.Configuration.ValidateOnSaveEnabled = false;

                exchangeRate.ID = currencyModel.ID;
                entities.Entry(exchangeRate).State = System.Data.Entity.EntityState.Deleted;
                entities.SaveChanges();

                entities.Configuration.ValidateOnSaveEnabled = true;
                int result = entities.SaveChanges();

                return true;
            }
        }
    }
}
