using System.Collections.Generic;
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

        }
        public void StartNotVerifyBuyerTest()
        {
            CreateNewProduct(1,0);

            CheckTests("//div[@class='n-alert-body__title']","Необходимо провести регистрацию");

        }
        public void StartNotHaveLimitTest()
        {
            CreateNewProduct(4,0);

            CheckTests("//div[@class='Vue-Toastification__container top-right']//div[@role='alert']","Сумма договора превышает лимит покупки!");

        }
        public void StartTests()
        {
            if (_check.GetItemChecked(0))
            {
                StartPositiveTest();
            }
            if (_check.GetItemChecked(1))
            {
                StartNotVerifyBuyerTest();
            }
            if (_check.GetItemChecked(2))
            {
                StartNotHaveLimitTest();
            }
            /*if (_check.GetItemChecked(3))
            {
            }
            if (_check.GetItemChecked(4))
            {
            }*/
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
        
    }
}