using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace NasiaAutoTestsApp
{
    
    public class VendorTests:PagesElements
    {
        private DataBase _responce;
        Log log = new Log();
        private string _instance;
        private string _login;
        private string _password;
        private string _buyer;
        private IWebDriver driver;
        private Overloads _test;
        public string _otpCode;
        private string _card;
        private string _date;
        private string _photo;
        private bool _equal;
        private WebDriverWait wait;
        public bool result;
        
        public VendorTests(string instance,string login,string password)
        {
            _login = login;
            _password = password;
            _instance = instance;
        }
        public VendorTests(string instance,string login,string password,string buyer,string card,string date,string photo,bool equal)
        {
            _login = login;
            _password = password;
            _instance = instance;
            _buyer = buyer;
            _card = card;
            _date = date;
            _photo = photo;
            _equal = equal;

        }
        public VendorTests(string instance,string login,string password,string buyer, string otpCode,string card,string date,string photo)
        {
            _login = login;
            _password = password;
            _instance = instance;
            _buyer = buyer;
            _otpCode = otpCode;
            _card = card;
            _date = date;
            _photo = photo;

        }
        public VendorTests(string instance,string login,string password,string buyer,string card,string date,string photo)
        {
            _login = login;
            _password = password;
            _instance = instance;
            _buyer = buyer;
            _card = card;
            _date = date;
            _photo = photo;
        }

        private void Setup()
        {
            log.LogMessage = new List<string>();
            log.TimeList = new List<string>();
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();//открываем Хром
            _test = new Overloads(driver, log);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl($"https://{_instance}.paymart.uz/ru");//вводим адрес сайта
            driver.Manage().Window.Maximize();//открыть в полном окне
            driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
        }

        public void  Auth()
        {
            Setup();
            _test.SendKeys(_fieldIdVendor,_login);
            _test.SendKeys(_fieldPasswordVendor,_password);
            _test.Click(_authButton);
        }

        public void NewBuyer()
        {
            try
            {
                //удаляем пользователя из базы
                _responce = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
                _responce.DeleteUser(_buyer);
                MessageBox.Show("Пользователь удален!");
                
                //проходим авторизацию
                Auth();
                
                //нажимаем на кнопку сайдабара(3), вводим номер покупателя и жмем кнопку "отправить ОТП-код"
                _test.Click(_listButtonsSideBar,3);
                _test.SendKeys(_fieldNewBuyerVendor,_buyer);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_buttonNewBuyerVendor));
                _test.Click(_buttonNewBuyerVendor);

                //если мы не передаем конкретный отп в тест, то берем его из базы
                if (_otpCode==null)
                {
                    _responce = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
                    _responce.request = "select code from otp_enter_code_attempts WHERE phone = 998"+_buyer;
                    Thread.Sleep(500);
                    _responce.GetOTP();
                    _otpCode = _responce.code;
                }
                
                //вставляем его в поле и жмем кнопку "Отправить"
                _test.SendKeys(_fieldNewBuyerVendor,1,_otpCode);
                _test.Click(_buttonOtpNewBuyerVendor);
                
            
                //вводим номер карты, дату и жмем на кнопку запроса ОТП
                _test.SendKeys(_fieldNewBuyerVendor,2,_card);
                _test.SendKeys(_fieldNewBuyerVendor,3,_date);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_buttonCheckCardVendor));
                _test.Click(_buttonCheckCardVendor);
            
                //берем отп из базы, он должен отличаться от предыдущего кода
                while (_responce.code==_otpCode)
                {
                    _responce.request = "select code from otp_enter_code_attempts WHERE phone = 998"+_buyer;
                    Thread.Sleep(500);
                    _responce.GetOTP();
                }
                _otpCode = _responce.code;
            
                //заполняем поле отп кодом и жмем кнопку "отправить"
                _test.SendKeys(_fieldNewBuyerVendor,4,_otpCode);
                _test.Click(_buttonCheckCardOTPVendor);

                //далее следует блок с заливкой фотографий
                for (int i = 0; i < 3; i++)
                {
                    _test.SendKeys(_fieldPhotoVendor,i, _photo);
                }
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_buttonSavePhotosVendor));
                _test.Click(_buttonSavePhotosVendor);
                
                //заполняем поля с доверителями
                _test.SendKeys(_fieldName1,"Брэд Питт");
                _test.SendKeys(_fieldName2,"Хлеб Пита");
                
                //если телефоны доверителей не должны совпадать
                if (!_equal)
                {
                    _test.SendKeys(_fieldGarantNumber1,"940937094");
                    _test.SendKeys(_fieldGarantNumber2,"940937093");
                    _test.SendKeys(_fieldGarantNumber1,"9");
                    _test.SendKeys(_fieldGarantNumber2,"9");
                }
                
                //если должны
                else
                {
                    _test.SendKeys(_fieldGarantNumber1,"9940937094");
                    _test.SendKeys(_fieldGarantNumber2,"9940937094");
                    _test.SendKeys(_fieldGarantNumber1,"9");
                    _test.SendKeys(_fieldGarantNumber2,"9");
                }
            
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable((_buttonSaveBuyerVendor)));
                _test.Click(_buttonSaveBuyerVendor);
                


            }
            catch (Exception e)
            {
            }
            ;

        }

        public Log FindContract(int contract)
        {
            Auth();
            //_test.SendKeys();
            return log;
        }

        public bool ActualExpected(string actual,string expected)
        {
            if (expected.Replace(" ","")==driver.FindElement(By.XPath(actual)).Text.Replace(" ",""))
            {
                Exit();
                return true;
            }
            Exit();
            return false;
        }

       

        

        private void Exit()
        {
            driver.Quit();
        }
    }
}