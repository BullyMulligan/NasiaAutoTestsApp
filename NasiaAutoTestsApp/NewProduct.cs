using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;

namespace NasiaAutoTestsApp
{
    public class NewProduct
    {
        private string _buyerNumber;
        public List<bool> result = new List<bool>();
        private VendorTests _setup;
        private VendorTests _auth;
        private VendorTests _newBuyerVendor;
        private VendorTests _newProduct;
        private string _login;
        private string _password;
        private string _buyer;
        private string _card;
        private string _date;
        private string _photo;
        private int _contract;
        private CheckedListBox _check;
        private VendorTests _statusBuyer;
        private bool _avalible;
        public List<Image> screens = new List<Image>();
        private Screen screenshot = new Screen();

        public NewProduct (string login, string password, string buyer, CheckedListBox check,string card,string date,string photo)
        {
            _login = login;
            _password = password;
            _check = check;
            _buyer = buyer;
            _card = card;
            _date = date;
            _photo = photo;
        }
        public NewProduct (string login, string password, string buyer,string card,string date,string photo)
        {
            _login = login;
            _password = password;
            _buyer = buyer;
            _card = card;
            _date = date;
            _photo = photo;
        }

        
        public void StartPositiveTest()
        {
            CreateNewProduct(4,1000000);
            CheckTests("//div[@class='Vue-Toastification__toast Vue-Toastification__toast--success top-right']//div[@role='alert']","Договор успешно создан и отправлен на модерацию");
            screens.Add(screenshot.CreateScreenshot("Possitive",_newProduct));
        }
        public void StartNotVerifyBuyerTest()
        {
            CreateNewProduct(1,0);
            CheckTests("//div[@class='n-alert-body__title']","Необходимо провести регистрацию");
            screens.Add(screenshot.CreateScreenshot("NotVerify",_newProduct));
        }
        public void StartNotHaveLimitTest()
        {
            CreateNewProduct(4,0);
            Thread.Sleep(1000);
            CheckTests("//div[@class='Vue-Toastification__container top-right']//div[@role='alert']","Сумма договора превышает лимит покупки!");
            screens.Add(screenshot.CreateScreenshot("NotHaveLimit",_newProduct));

        }
        public void StartModerateProduct()
        {
            CreateNewProduct(4,1000000,1);
            Thread.Sleep(1000);
            CheckTests("//div[@class='Vue-Toastification__toast Vue-Toastification__toast--success top-right']//div[@role='alert']","Договор успешно создан и отправлен на модерацию");
            screens.Add(screenshot.CreateScreenshot("Moderate",_newProduct));
        }
        public void StartExpiredProduct()
        {
            CreateNewProduct(4,1000000,3);
            Thread.Sleep(1000);
            CheckTests("//div[@class='Vue-Toastification__toast Vue-Toastification__toast--success top-right']//div[@role='alert']","Договор успешно создан и отправлен на модерацию");
            screens.Add(screenshot.CreateScreenshot("Expired",_newProduct));
        }
        public void StartCanceledProduct()
        {
            CreateNewProduct(4,1000000,5);
            Thread.Sleep(1000);
            CheckTests("//div[@class='Vue-Toastification__toast Vue-Toastification__toast--success top-right']//div[@role='alert']","Договор успешно создан и отправлен на модерацию");
            screens.Add(screenshot.CreateScreenshot("Canceled",_newProduct));
        }
        
        public void StartTests()
        {
            if (_check.GetItemChecked(0))
            {
                StartPositiveTest();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(1))
            {
                StartNotVerifyBuyerTest();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(2))
            {
                StartNotHaveLimitTest();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(3))
            {
                StartModerateProduct();
            }
            else
            {
                screens.Add(null);
            }
            if(_check.GetItemChecked(4))
            {
                StartExpiredProduct();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(5))
            {
                StartCanceledProduct();
            }
            else
            {
                screens.Add(null);
            }
        }
        void InitializationSetup()
        {
            _setup = new VendorTests();_setup.Setup();
            _auth = new VendorTests(_login, _password);_auth.Auth();
        }
        void CheckBuyerInDataBase()
        {
            DataBase checkAvailable = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
            checkAvailable.CheckStatusBuyer(_buyer);
            if (checkAvailable.available)
            {
                _avalible = true;
                return;
            }
            _avalible = false;
        }
        void ChangeBuyerInDataBase(int status)
        {
            CheckBuyerInDataBase();
            VendorTests _newBuyer = new VendorTests(_buyer,_card,_date,_photo);
            if (!_avalible)
            {
                //запускаем
                _newBuyer.NewBuyer();
            }
            DataBase setStatus = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
            setStatus.SetStatusBuyer(_buyer,status);
        }
        void CheckTests(string actual, string expected)
        {
            if (_newProduct.ActualExpected(actual, expected))
            {
                result.Add(true);
            }
            else
            {
                result.Add(false);
            }
        }

        public void CreateNewProduct(int status,int limit)
        {
            InitializationSetup();
            _newProduct = new VendorTests(_buyer);
            ChangeBuyerInDataBase(status);
            DataBase setStatus = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
            setStatus.SetLimitBuyer(_buyer,limit);
            _newProduct.NewProduct();
        }
        public void CreateNewProduct(int status,int limit,int productStatus)
        {
            InitializationSetup();
            _newProduct = new VendorTests(_buyer);
            ChangeBuyerInDataBase(status);
            DataBase setStatus = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
            setStatus.SetLimitBuyer(_buyer,limit);
            _newProduct.NewProduct();
            int id = GetLastProduct();
            setStatus.SetProductStatus(id,productStatus);
        }

        private int GetLastProduct()
        {
            var byXpath = By.XPath("//div[@class='contract__top']//div[1]//H5");
            //проходим столько раз, сколько номеров показывает браузер
            var word = Form1.Driver.FindElement(byXpath).Text;
            string digit = "";
            foreach (var ch in word)
            {
                if (Char.IsDigit(ch))
                {
                    digit += ch;
                } 
            }
            return Convert.ToInt32(digit);
        }
        
    }
}