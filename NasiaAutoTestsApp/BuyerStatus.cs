using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenQA.Selenium;

namespace NasiaAutoTestsApp
{
    public class BuyerStatus
    {
        private IWebDriver _driver;
        private string _buyerNumber;
        public List<bool> result = new List<bool>();
        private VendorTests _setup;
        private VendorTests _auth;
        private VendorTests _NewBuyerVendor;
        private VendorTests _BuyerStatus;
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
        

        public BuyerStatus(string login, string password, string buyer, CheckedListBox check,string card,string date,string photo)
        {
            _login = login;
            _password = password;
            _check = check;
            _buyer = buyer;
            _card = card;
            _date = date;
            _photo = photo;
        }

        public void StartPositiveTest()
        {
            InitializationSetup();
            _statusBuyer = new VendorTests(_buyer,_contract);
            ChangeBuyerInDataBase(1);
            _statusBuyer.BuyerStatus();
            CheckTests("//div[@class='client-check__list']//span[@class='n-tag__content']", "Необходимо провести регистрацию");
            screens.Add(screenshot.CreateScreenshot("Positive",_statusBuyer));
        }
        public void StartVerifyBuyer()
        {
            InitializationSetup();
            _statusBuyer = new VendorTests(_buyer,_contract);
            ChangeBuyerInDataBase(4);
            _statusBuyer.BuyerStatus();
            CheckTests("//div[@class='client-check__list']//span[@class='n-tag__content']", "Верифицирован");
            screens.Add(screenshot.CreateScreenshot("Verify",_statusBuyer));
        }
        public void StartBlockedBuyer()
        {
            InitializationSetup();
            _statusBuyer = new VendorTests(_buyer,_contract);
            ChangeBuyerInDataBase(8);
            _statusBuyer.BuyerStatus();
            CheckTests("//div[@class='client-check__list']//span[@class='n-tag__content']", "Отказ в верификации");
            screens.Add(screenshot.CreateScreenshot("Blocked",_statusBuyer));
        }
        public void StartNotFoundTest()
        {
            InitializationSetup();
            _statusBuyer = new VendorTests("940937094",_contract);
            CheckBuyerInDataBase();
            _statusBuyer.BuyerStatus();
            CheckTests("//div[@class='client-check__list']//span", "Пользователь не найден!");
            screens.Add(screenshot.CreateScreenshot("NotFound",_statusBuyer));
        }
        public void StartAddGuarantBuyer()
        {
            InitializationSetup();
            _statusBuyer = new VendorTests(_buyer,_contract);
            ChangeBuyerInDataBase(12);
            _statusBuyer.BuyerStatus();
            CheckTests("//div[@class='client-check__list']//span[@class='n-tag__content']", "Отказано: Необходимо добавить контактное лицо");
            screens.Add(screenshot.CreateScreenshot("AddGuarant",_statusBuyer));
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
                StartVerifyBuyer();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(2))
            {
                StartBlockedBuyer();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(3))
            {
                StartNotFoundTest();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(4))
            {
                StartAddGuarantBuyer();
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

        //проверяем наличие клиента в базе
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
        //меняем статус клиента
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
            if (_statusBuyer.ActualExpected(actual, expected))
            {
                result.Add(true);
            }
            else
            {
                result.Add(false);
            }
        }
        
    }
}