using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirLineBussiness;
using AirLine.Models;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethodSave()
        {
            CurrencyBL currencyBL = new CurrencyBL();
            CurrencyModel currecyModel = new CurrencyModel();
            currecyModel.Country = "France1";
            currecyModel.Name = "FR1";
            currecyModel.Value = 2001;
            bool test = currencyBL.SaveCurrency(currecyModel);


        }

        [TestMethod]
        public void TestMethodGet()
        {
            CurrencyBL currencyBL = new CurrencyBL();
            List<CurrencyModel> currencyModelLis = currencyBL.ReadCurrencyFromDB();


        }

        [TestMethod]
        public void TestMethodUpdate()
        {
            CurrencyBL currencyBL = new CurrencyBL();
            CurrencyModel currecyModel = new CurrencyModel();
            currecyModel.ID = 1;
            currecyModel.Country = "France";
            currecyModel.Name = "FR";
            currecyModel.Value = 200;
            bool test = currencyBL.UpdateCurrency(currecyModel);


        }

        [TestMethod]
        public void TestMethodDelete()
        {
            CurrencyBL currencyBL = new CurrencyBL();
            CurrencyModel currecyModel = new CurrencyModel();
            currecyModel.ID = 1008;
            currecyModel.Country = "France1";
            currecyModel.Name = "FR1";
            currecyModel.Value = 2001;
            bool test = currencyBL.DeleteCurrency(currecyModel);


        }
    }
}
